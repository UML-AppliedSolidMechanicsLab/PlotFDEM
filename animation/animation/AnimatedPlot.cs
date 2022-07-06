/*
 * Created by SharpDevelop.
 * User: sstaple
 * Date: 8/4/2010
 * Time: 1:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Drawing2D;
using AnimatedGif;

namespace Animation
{
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public partial class AnimatedPlot : Form
	{
		#region Private Members
		//For Plot
		private DoubleBufferedPanel pMainFrame;
		private Bitmap pMainFrameBitmap;
		public float scaleFactor = 1f;
		protected float translateX = 0f;
		protected float translateY = 0f;
		private string sTitle1;
		private string sXTitle1;
		private string sYTitle1;
		private double YMax1;
		private double YMin1;
		private double XMax1;
		private double XMin1;
		private double PerBorder = 0.05;
		private List<PointPairList []> lPointPairList;
		private string [] labels;
		private Color [] LineColor = new Color[20]{ Color.DodgerBlue, Color.Red, Color.GreenYellow, Color.DarkBlue, Color.Firebrick, Color.Blue, Color.SkyBlue, Color.LightCoral,
			Color.ForestGreen, Color.LimeGreen,Color.GreenYellow,Color.Purple,Color.DarkViolet,Color.Magenta,
			Color.Black,Color.DimGray,Color.Silver,Color.Teal,Color.LightSeaGreen,Color.Turquoise};
		private GraphPane myPane;
		private int nFramesToSkip = 1;
		private bool saveWithPlot;
		//General Members
		private bool isAnimating = false;
		private int nFramesPerSecond;
		private bool forward = true;
		public int currentFrame = 0;
		private bool isLoop = true;
		protected int FrameCount = 0;
		protected float LowerLimit;
		protected float LeftLimit;
		protected float UpperLimit;
		protected float RightLimit;
		private IDrawGraphic myGraphicDrawer;
		
		#endregion
		
		#region Constructor
		
		public AnimatedPlot(IDrawGraphic inDrawGraphic, List<PointPairList []> inputPointPairList, string [] plotLabels, string inputTitle, string inputXAxis, string inputYAxis,
		                    float xMin, float yMin, float xMax, float yMax)
		{
			labels = plotLabels;
			myGraphicDrawer = inDrawGraphic;
			LowerLimit = yMin;
			LeftLimit = xMin;
			UpperLimit = yMax;
			RightLimit =xMax;
			sTitle1 = inputTitle;
			sXTitle1 = inputXAxis;
			sYTitle1 = inputYAxis;
			lPointPairList = inputPointPairList;
			FrameCount = lPointPairList.Count;
			
			#region Now work on the plot
			//Make sure there is a point pair list
			if(lPointPairList.Count == 0) return;
			
			//Set the Axis
			DetermineAxisBounds();
			
			Initiate();
			
			//Set up the graph pane
			myPane = zedGraphControl1.GraphPane;
			myPane.Title.Text = sTitle1;
			myPane.XAxis.Title.Text = sXTitle1;
			myPane.YAxis.Title.Text = sYTitle1;
			
			// Fill the background of the chart rect and pane
			myPane.Chart.Fill = new Fill(Color.White);

			// Show the legend
			myPane.Legend.IsVisible = true;
			myPane.Legend.Border.IsVisible = false;
			myPane.Legend.Position = ZedGraph.LegendPos.InsideTopLeft;
			myPane.Legend.Fill.IsVisible = false;

			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			myPane.XAxis.MajorTic.IsOpposite = false;
			myPane.XAxis.MinorTic.IsOpposite = false;

			// Hide the axis grid lines
			myPane.YAxis.MajorGrid.IsVisible = false;
			myPane.XAxis.MajorGrid.IsVisible = false;
			myPane.YAxis.MajorTic.IsOutside = false;
			myPane.XAxis.MajorTic.IsOutside = false;
			myPane.YAxis.MinorTic.IsAllTics = false;
			myPane.XAxis.MinorTic.IsAllTics = false;
			#endregion
			
		}
		
		private void Initiate(){
			//Initialize the component and activate it
			InitializeComponent();
			
			#region Now add the picture box
			
			this.pMainFrame = new DoubleBufferedPanel();
			
			// pbMainFrame
			// 
			//this.pMainFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			//                                                                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			
			this.pMainFrame.BackColor = System.Drawing.Color.White;
			this.pMainFrame.Location = new System.Drawing.Point(0, 0);
			this.pMainFrame.Size = new System.Drawing.Size(383, 496);
			this.pMainFrame.Dock = DockStyle.Fill;
			this.pMainFrame.Name = "pbMainFrame";
			this.pMainFrame.TabIndex = 16;
			this.pMainFrame.TabStop = false;
			
			this.pMainFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.pMainFramePaint);
			this.pMainFrame.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PMainFrameMouseWheelScroll);
			this.pMainFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PMainFrameMouseClick);
			this.pMainFrame.MouseEnter += new EventHandler(PMainFrameMouseEnter);
			//this.Controls.Add(this.pMainFrame);
			this.splitContainer1.Panel1.Controls.Add(this.pMainFrame);
			
			PanelMethods.ZoomToFit(pMainFrame, ref scaleFactor, ref translateX, ref translateY, LeftLimit, LowerLimit, RightLimit, UpperLimit);
			
			#endregion

			Show();
			//Set the intervals of the clock
			tbProgress.Minimum = 0;
			tbProgress.Maximum = FrameCount-1;
			nFramesPerSecond = Convert.ToInt32(nudFPS.Value);
			UpdateInterval();
			//ShowDialog ();
			this.Focus();	
		}
		#endregion
		
		#region Form Events
		
		void Timer1Tick(object sender, EventArgs e)
		{
			UpdatePlotAndFrameNumber();
			Increment();
		}
		
		void BPlayClick(object sender, EventArgs e)
		{
			if (isAnimating != true) {
				
				StartAnimation();
			}
			
		}
		
		void BPauseClick(object sender, EventArgs e)
		{
			if (isAnimating) {
				
				StopAnimation();
			}
		}
		
		void BBackToFirstClick(object sender, EventArgs e)
		{
			currentFrame = 0;
			
			if (isAnimating == false) {
				UpdatePlotAndFrameNumber();
			}
		}
		
		void BBackFrameClick(object sender, EventArgs e)
		{
			if (currentFrame == 0) {
				
				currentFrame = FrameCount - (FrameCount % nFramesToSkip) - 1;
			}
			else{
				
				currentFrame = currentFrame  - nFramesToSkip;
			}
			
			if (isAnimating == false) {
				UpdatePlotAndFrameNumber();
			}
		}
		
		void BForwardFrameClick(object sender, EventArgs e)
		{
			if (currentFrame > FrameCount - 1 - nFramesToSkip) {
				currentFrame = 0;
			}
			else{
				
				currentFrame += nFramesToSkip;
			}
			
			if (isAnimating == false) {
				UpdatePlotAndFrameNumber();
			}
		}
		
		void BForwardToEndClick(object sender, EventArgs e)
		{
			currentFrame = FrameCount-1;
			
			if (isAnimating == false) {
				UpdatePlotAndFrameNumber();
			}
			if (isLoop == false){
				
				StopAnimation();
			}
		}
		
		void BLoopClick(object sender, EventArgs e)
		{
			if (isLoop) isLoop = false;
			else isLoop = true;
			
		}
		
		void BReverseClick(object sender, EventArgs e)
		{
			if (forward) forward = false;
			else forward = true;
		}
		
		void nudFPSClick(object sender, EventArgs e)
		{
			UpdateInterval();
		}
		
		void BSaveClick(object sender, EventArgs e)
		{
			StopAnimation();
			
			//first get a dialogue box that asks if you should also save the plot
			DialogResult dlg = MessageBox.Show("Save with Plot?", "Saving Options",  MessageBoxButtons.YesNo,  MessageBoxIcon.Question);
			if (dlg == DialogResult.No)
			{saveWithPlot = false;}
			else{saveWithPlot = true;  }
			
			List<Bitmap> lBitmaps = CreateBitmapList();
			
			#region add a save dialogue box
			// Create new SaveFileDialog object
			SaveFileDialog DialogSave = new SaveFileDialog();
			// Default file extension
			//DialogSave.DefaultExt = "avi";
			DialogSave.DefaultExt = "gif";
			// Available file extensions
			//DialogSave.Filter = " (*.avi)|*.avi|All files (*.*)|*.*";
			DialogSave.Filter = " (*.gif)|*.gif|All files (*.*)|*.*";
			// Adds a extension if the user does not
			DialogSave.AddExtension = true;
			// Restores the selected directory, next time
			DialogSave.RestoreDirectory = true;
			DialogSave.Title = "Where do you want to save the file?";
			#endregion
			
			#region Save it to a GIF
			if (DialogSave.ShowDialog() == DialogResult.OK)
			{
				try {
					//second argument is the ms between frames (1/nfps * 1000)
					
					using (var gif = new AnimatedGifCreator(DialogSave.FileName, (int)(1000.0 / nFramesPerSecond)))
					{
						foreach (Bitmap bmp in lBitmaps)
						{
							gif.AddFrame(bmp, delay: -1, quality: GifQuality.Bit8);
							bmp.Dispose();
						}
					}

					
				} catch (Exception ex) {
					MessageBox.Show("Error Saving:" + ex.ToString());
				}
			}
			#endregion
			
			currentFrame = 0;
		}

		void bPlotAllClick(object sender, EventArgs e)
		{
			StopAnimation();
			
			UpdateAxis();
			
			myPane.CurveList.Clear();
			
			for (int i = 0; i < FrameCount-1; i +=1 + nFramesToSkip) {
				for (int j = 0; j < lPointPairList[i].Length; j++) {
					
					LineItem curve = myPane.AddCurve( labels[j], lPointPairList[i][j], Color.Black);
					curve.Symbol.IsVisible = false;
					curve.Line.Width = 1f;
				}
			}
			zedGraphControl1.Invalidate();
			pMainFrame.Invalidate();
		}
		
		void BOptionsClick(object sender, EventArgs e)
		{
			StopAnimation();
		}
		
		void BExportClick(object sender, EventArgs e)
		{
			StopAnimation();
			
			//add a save dialogue box
			// Create new SaveFileDialog object
			SaveFileDialog DialogSave = new SaveFileDialog();
			// Default file extension
			DialogSave.DefaultExt = "csv";
			// Available file extensions
			DialogSave.Filter = "Comma Separated Value (*.csv)|*.csv|All files (*.*)|*.*";
			// Adds a extension if the user does not
			DialogSave.AddExtension = true;
			// Restores the selected directory, next time
			DialogSave.RestoreDirectory = true;
			DialogSave.Title = "Where do you want to save the file?";
			
			if (DialogSave.ShowDialog() == DialogResult.OK)
			{
				try {
					StreamWriter dataWrite = new StreamWriter(DialogSave.FileName);
					WriteOutputFile(dataWrite);
					dataWrite.Close();
					
				} catch (Exception ex) {
					
					MessageBox.Show("Error: Try Again!.  Error: " + ex.Message);
				}
			}
		}
		
		void TbProgressScroll(object sender, EventArgs e)
		{
			currentFrame = tbProgress.Value;
			
			if (isAnimating == false) {
				UpdatePlotAndFrameNumber();
			}
		}
		
		void NudSkipValueChanged(object sender, EventArgs e)
		{
			nFramesToSkip = (int)nudSkip.Value;
			tbProgress.SmallChange = nFramesToSkip;
			tbProgress.TickFrequency = nFramesToSkip;
		}
		
		#endregion
		
		#region pMainFrame methods
		void pMainFramePaint(object sender, PaintEventArgs e)
		{
			Bitmap bmp = new Bitmap(pMainFrame.Width, pMainFrame.Height
			                        ,System.Drawing.Imaging.PixelFormat.Format32bppRgb);
			Graphics graphic = Graphics.FromImage(bmp);
			graphic.Clear(System.Drawing.Color.White);
			
			//Graphics gscreen = e.Graphics;
			
			//Now, transform the axis from local instance to window axis
			Matrix myTransform = new Matrix(1f, 0f, 0f, -1f, 0f, 0f); //reflection
			myTransform.Scale(scaleFactor, scaleFactor, MatrixOrder.Append);
			myTransform.Translate(translateX, translateY, MatrixOrder.Append);
			
			//Now, make a container to apply the transfrom to the container only
			GraphicsContainer transformContainer = graphic.BeginContainer();
			graphic.Transform = myTransform;
			
			myGraphicDrawer.Draw(graphic, currentFrame);
			
			//close the container
			graphic.EndContainer(transformContainer);
			
			//gscreen.InterpolationMode = InterpolationMode.HighQualityBicubic;
			
			
			Graphics gscreen = e.Graphics;
			gscreen.DrawImage(bmp, new PointF(0f,0f));
			graphic.Dispose();
			bmp.Dispose();
		}
		
		void updatePMainFrameBitmap()
        {
			pMainFrameBitmap = new Bitmap(pMainFrame.Width, pMainFrame.Height
									, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
			Graphics graphic = Graphics.FromImage(pMainFrameBitmap);
			graphic.Clear(System.Drawing.Color.White);
		}

		void PMainFrameMouseWheelScroll(object sender, MouseEventArgs e){
			
			float factor = 0.95F;
			
			//Zoom to the mouse location
			if (e.Delta > 0) {
				factor = 1.05F;
			}
			
			PanelMethods.ZoomToPoint((PointF)e.Location, factor, ref scaleFactor,
			                         ref translateX, ref translateY);
			pMainFrame.Invalidate();
		}
		
		void PMainFrameMouseClick(object sender, MouseEventArgs e){
			
			PanelMethods.ZoomToFit(pMainFrame, ref scaleFactor, ref translateX, ref translateY, LeftLimit, LowerLimit, RightLimit, UpperLimit);
			pMainFrame.Invalidate();
		}
		
		void PMainFrameMouseEnter(object sender, EventArgs e){
			pMainFrame.Focus();
		}
		
		#endregion
		
		#region Private Methods
		private void StopAnimation(){
			
			timer1.Stop();
			timer1.Enabled = false;
			bPlay.Enabled = true;
			bPause.Enabled = false;
			isAnimating = false;
		}
		
		private void StartAnimation(){
			
			timer1.Start();
			timer1.Enabled = true;
			bPlay.Enabled = false;
			bPause.Enabled = true;
			isAnimating = true;
		}
		
		private void DetermineAxisBounds(){
			
			//Find the max/min X and Y values
			double maxX = 0.0;
			double maxY = 0.0;
			double minX = 0.0;
			double minY = 0.0;
			bool isFirst = true;
			
			foreach(PointPairList [] ppla in lPointPairList){
				
				foreach (PointPairList ppl in ppla) {
					
					foreach(PointPair pp in ppl){
						
						double x = pp.X;
						double y = pp.Y;
						
						if (isFirst) {
							maxX = x;
							minX = x;
							maxY = y;
							minY = y;
						}
						isFirst = false;
						
						if (y > maxY) maxY = y;
						if (y < minY) minY = y;
						if (x > maxX) maxX = x;
						if (x < minX) minX = x;
					}
				}
			}
			//Find the range
			
			double xRange = maxX - minX;
			double yRange = maxY - minY;
			
			//Set the axis as 5% of the range more/less than max/min
			
			XMax1 = Math.Ceiling(maxX + PerBorder * xRange);
			XMin1 = Math.Floor(minX); // - PerBorder * xRange;
			YMax1 =  Math.Ceiling(maxY + PerBorder * yRange);
			YMin1 = Math.Floor(minY - PerBorder * yRange);
			
		}
		
		private void UpdateAxis(){
			
			myPane.XAxis.Scale.Max = XMax1;
			myPane.XAxis.Scale.Min = XMin1;
			myPane.YAxis.Scale.Max = YMax1;
			myPane.YAxis.Scale.Min = YMin1;
			
			zedGraphControl1.AxisChange();
			
		}
		
		private void UpdatePlotAndFrameNumber(){
			UpdatePlot();
			lFrameNumber.Text = currentFrame.ToString();
			tbProgress.Value = currentFrame;
		}
		
		protected virtual void UpdatePlot(){
			
			//UpdateAxis();
			
			myPane.CurveList.Clear();
			
			for (int i = 0; i < lPointPairList[currentFrame].Length; i++) {
				
				if (i < 20) {
					LineItem curve = myPane.AddCurve( labels[i], lPointPairList[currentFrame][i], LineColor[i]);
					
					curve.Symbol.IsVisible = false;
					
					curve.Line.Width = 4f;
					
					curve.Line.IsAntiAlias = true;
				}
			}
			zedGraphControl1.Invalidate();
			pMainFrame.Invalidate();
		}
		
		protected virtual List<Bitmap> CreateBitmapList()
		{
			currentFrame = 0;
			List<Bitmap> lBitmaps = new List<Bitmap>();
			foreach (PointPairList[] ppl in lPointPairList) {
				UpdatePlotAndFrameNumber();
				
				Bitmap panelBMP = new Bitmap(pMainFrame.Width, pMainFrame.Height);
				pMainFrame.DrawToBitmap(panelBMP, new Rectangle(0, 0, pMainFrame.Width, pMainFrame.Height ));
				
				int w = pMainFrame.Width;
				Bitmap plotBMP = myPane.GetImage();
				
				if (saveWithPlot) {
					w += plotBMP.Width;
				}
				
				Bitmap tempBMP = new Bitmap(w, pMainFrame.Height);
				using (Graphics g = Graphics.FromImage(tempBMP))
				{
					g.Clear(System.Drawing.Color.White);
					g.DrawImage(panelBMP, 0, 0);
					if (saveWithPlot) {
						g.DrawImage(plotBMP, panelBMP.Width, (panelBMP.Height - plotBMP.Height) / 2f);
					}
				}
				lBitmaps.Add(tempBMP);
				currentFrame = currentFrame + 1;
			}
			currentFrame = 0;
			return lBitmaps;
		}
		
		private void UpdateInterval(){
			
			nFramesPerSecond = Convert.ToInt32(nudFPS.Value);
			
			timer1.Interval = Convert.ToInt32(1.0 / nFramesPerSecond * 1000);
		}
		
		private void WriteOutputFile(StreamWriter dataWrite)
		{
			dataWrite.WriteLine("Created: " + DateTime.Now);
			dataWrite.WriteLine("Title: " + sTitle1 + ", X axis: " + sXTitle1 + ", Y axis: " + sYTitle1);
			
			foreach(PointPairList [] ppla in lPointPairList){
				//Print titles
				dataWrite.Write("x, y, ");
			}
			
			dataWrite.WriteLine();
			
			#region Find the longest series
			int nMax = 0;
			
			foreach(PointPairList [] ppla in lPointPairList){
				foreach(PointPairList ppl in ppla){
					
					if (ppl.Count > nMax) {
						
						nMax = ppl.Count;
					}
				}
			}
			#endregion
			
			for (int i = 0; i < nMax; i++) {
				
				foreach(PointPairList [] ppla in lPointPairList){
					
					foreach(PointPairList ppl in ppla){
						
						if (i >= ppl.Count) {
							
							dataWrite.Write("NaN, NaN, ");
						}
						else{
							
							dataWrite.Write(ppl[i].X + ", " + ppl[i].Y + ", ");
						}
					}
				}
				
				dataWrite.WriteLine();
			}
			dataWrite.WriteLine();
		}
		
		private void Increment(){
			//Increment the frames
			if (forward) {
				currentFrame = currentFrame + 1 + nFramesToSkip;
				
				if (currentFrame > FrameCount - nFramesToSkip-1) {
					
					currentFrame = 0;
					
					if (isLoop != true) {
						
						StopAnimation();
					}
				}
			}
			else{
				currentFrame  -= 1 + nFramesToSkip;;
				
				if (currentFrame < nFramesToSkip ) {
					
					currentFrame = FrameCount - 1 - nFramesToSkip;
					
					if (isLoop != true) {
						
						StopAnimation();
					}
				}
			}
		}

		
        #endregion

        private void contextMenuStrip_Panel1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy")
            {
				Bitmap bm = new Bitmap(pMainFrame.Width, pMainFrame.Height
									, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

				pMainFrame.DrawToBitmap(bm, new Rectangle(0, 0, pMainFrame.Width, pMainFrame.Height));
				Graphics graphics = Graphics.FromImage(bm);
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				Clipboard.SetImage(bm);
				graphics.Flush();

            }
        }
    }

    /// <summary>
    /// Like a normal panel, but it is double buffered for smooth animation.
    /// Note: I believe that you have to define your own user defined paint method.
    /// Source: http://www.csharphelp.com/forum/topic/flickering-when-drawing-bitmaps-on-a-panel
    /// </summary>
    public class DoubleBufferedPanel : System.Windows.Forms.Panel
	{
		public DoubleBufferedPanel()
		{
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
		
	}
	public interface IDrawGraphic{
		
		void Draw(Graphics graphic, int i);

		bool IsClosed();
	}
}