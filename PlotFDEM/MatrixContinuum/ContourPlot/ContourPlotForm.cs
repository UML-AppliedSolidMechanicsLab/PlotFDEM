/*
 * Created by SharpDevelop.
 * User: Scott
 * Date: 9/1/2011
 * Time: 2:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace PlotFDEM.MatrixContinuum.ContourPlot
{
	/// <summary>
	/// Description of ContourPlotForm.
	/// </summary>
	public partial class ContourPlotForm : Form
	{
		public ContourPlotMaker myPlot;
		protected DoubleBufferedPanel pMainFrame;
		protected Bitmap contourBitmap;
		protected Bitmap colorScaleBitmap;
		protected float scaleFactor = 1f;
		protected float translateX = 0f;
		protected float translateY = 0f;
		protected int i = 0;
		protected List <Color []> lColorSchemes = new List<Color[]>();
		protected List <string> lColorSchemeNames = new List<string>();
		protected string [] sPlotType = new string[] {"Local Stress", "Global Stress", "Local Strain", "Global Strain", "Local Displacement", "Global Displacement", "Damage" };
		protected string [][] sPlotComponent = new string[][]{new string[] {"vm", "max Principal", "xx", "yy", "zz", "yz", "xz", "xy"}, new string[] {"vm", "max Principal", "xx", "yy", "zz", "yz", "xz", "xy"},
			new string[] {"vm", "max Principal", "xx", "yy", "zz", "yz", "xz", "xy"}, new string[] {"vm", "max Principal", "xx", "yy", "zz", "yz", "xz", "xy"},
			new string[] {"mag", "u", "v", "w"}, new string[] {"mag", "u", "v", "w"}, new string[] {"D"}};

		protected string[] sFiberPairs;


		#region Constructors

		public ContourPlotForm()
		{
			InitializeComponent();

            //create plotmaker
            myPlot = new ContourPlotMaker();
			cbPlotType.Items.Clear();
			cbPlotType.Items.AddRange(sPlotType);
			cbPlotType.SelectedIndex = 0; //This should call the event
			updateColorScaleBitmap();

			Initialize();

			UpdateContourBitmap();
		}
		
		protected void Initialize(){
			
			#region Initiate Buttons
			//Color Buttons
			bHighColor.BackColor = myPlot.HighColor;
			bLowColor.BackColor = myPlot.LowColor;
			bFiberColor.BackColor = myPlot.FiberColor;
			bProjFiberColor.BackColor = myPlot.ProjFiberColor;
			bBoundaryColor.BackColor = myPlot.BoundaryColor;
			//Fill in Text Boxes
			tbHighRange.Text = myPlot.HighRange.ToString();
			tbLowRange.Text = myPlot.LowRange.ToString();
			
			nudGridPtsX.Value = myPlot.NGridPts;
			nudIsoLines.Value = myPlot.NLevels;
			cbIsoLines.Checked = myPlot.PlotIsolines;
            cbAutomaticRange.Checked = myPlot.AutomaticRange;
			cbConnections.Checked = myPlot.ShowConnections;
			cbShowCrack.Checked = myPlot.ShowCrack;
			cbFiberNumbers.Checked = myPlot.ShowFiberNumbers;

			tbMatrixTransparancy.Value = 0;
			#endregion

			#region Now add the picture box

			this.pMainFrame = new DoubleBufferedPanel();
			
			// pbMainFrame
			// 
			this.pMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMainFrame.BackColor = System.Drawing.Color.White;
			this.pMainFrame.Name = "pbMainFrame";
			
			this.pMainFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.pMainFramePaint);
			this.pMainFrame.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PMainFrameMouseWheelScroll);
			this.pMainFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PMainFrameMouseClick);
			this.pMainFrame.MouseEnter += new EventHandler(PMainFrameMouseEnter);
			
			#endregion

			//Center it
			//PreProcessor.Draw.PanelMethods.ZoomToFit(pMainFrame, ref scaleFactor, ref translateX, ref translateY, InitialAssembly);

			//Put all of the default color schemes in there
			lColorSchemes.Add(new Color[] { Color.Blue, Color.Aqua, Color.LimeGreen, Color.Yellow, Color.Red });
			lColorSchemeNames.Add("BtoGtoYtoR");
			lColorSchemes.Add(new Color[] { Color.Red, Color.Yellow, Color.LimeGreen, Color.Aqua, Color.Blue });
			lColorSchemeNames.Add("RtoYtoGtoB");
			lColorSchemes.Add(new Color[]{Color.Red, Color.White});
			lColorSchemeNames.Add("RedToWhite");
			lColorSchemes.Add(new Color[]{Color.White, Color.Red});
			lColorSchemeNames.Add("WhiteToRed");
			lColorSchemes.Add(new Color[] { Color.Black, Color.White });
			lColorSchemeNames.Add("BlackToWhite");
			lColorSchemes.Add(new Color[] { Color.White, Color.Black });
			lColorSchemeNames.Add("WhiteToBlack");
			lColorSchemes.Add(new Color[]{Color.RoyalBlue, Color.Gray});
			lColorSchemeNames.Add("BtoG");
			lColorSchemes.Add(new Color[]{Color.FromArgb(0, 100, 255), Color.White});
			lColorSchemeNames.Add("BlueToWhite");
			lColorSchemes.Add(new Color[]{Color.White, Color.FromArgb(0, 100, 255)});
			lColorSchemeNames.Add("WhiteToBlue");
			lColorSchemes.Add(new Color[]{Color.Red, Color.Yellow, Color.LimeGreen, Color.Aqua, Color.Blue, Color.Blue, Color.Magenta});
			lColorSchemeNames.Add("CrazyHypersizer");
			lColorSchemes.Add(new Color[]{Color.Magenta, Color.Blue, Color.Blue, Color.Aqua, Color.LimeGreen, Color.Yellow, Color.Red});
			lColorSchemeNames.Add("CrazyHypersizer Inverted");
            lColorSchemes.Add(new Color[] { Color.FromArgb(0, Color.White), Color.Blue });
            lColorSchemeNames.Add("Transparent Blue");
            lColorSchemes.Add(new Color[] { Color.Blue, Color.FromArgb(0, Color.Blue) });
            lColorSchemeNames.Add("Transparent Blue Inverted");
            lColorSchemes.Add(new Color[] { Color.FromArgb(0, Color.Red), Color.Red });
            lColorSchemeNames.Add("Transparent Red");
            lColorSchemes.Add(new Color[] { Color.Red, Color.FromArgb(0, Color.Red) });
            lColorSchemeNames.Add("Transparent Red Inverted");
            lColorSchemes.Add(new Color[] { Color.FromArgb(0, Color.Black), Color.Black });
            lColorSchemeNames.Add("Transparent Black");
            lColorSchemes.Add(new Color[] { Color.Black, Color.FromArgb(0, Color.Black) });
            lColorSchemeNames.Add("Transparent Black Inverted");
			lColorSchemes.Add(new Color[] { Color.Blue, Color.LimeGreen });
			lColorSchemeNames.Add("Blue To Green");
			lColorSchemes.Add(new Color[] { Color.LimeGreen, Color.Blue });
			lColorSchemeNames.Add("Green to Blue");
			lColorSchemes.Add(new Color[] { Color.FromArgb(0, 0, 255), Color.FromArgb(0, 92, 255),  Color.FromArgb(0, 185, 255), Color.FromArgb(0, 255, 231), Color.FromArgb(0, 255, 139),Color.FromArgb(0, 255, 46),
			   Color.FromArgb(46, 255, 0), Color.FromArgb(139, 255, 0),  Color.FromArgb(139, 255, 0), Color.FromArgb(231, 255, 0), Color.FromArgb(255, 185, 0), Color.FromArgb(255, 92, 0), Color.Red});
			lColorSchemeNames.Add("Abaqus");
			lColorSchemes.Add(new Color[] { Color.Red, Color.FromArgb(255, 92, 0), Color.FromArgb(255, 185, 0), Color.FromArgb(231, 255, 0), Color.FromArgb(139, 255, 0),
				Color.FromArgb(139, 255, 0), Color.FromArgb(46, 255, 0), Color.FromArgb(0, 255, 46), Color.FromArgb(0, 255, 139), Color.FromArgb(0, 255, 231),
			Color.FromArgb(0, 185, 255), Color.FromArgb(0, 92, 255), Color.FromArgb(0, 0, 255)});
			lColorSchemeNames.Add("AbaqusInverted");

			UpdateColorsList();
			cbColorSchemes.SelectedIndex = 0;

			PopulatePointQueryLists();

			Show();
			bool t = this.Focus();
		}
		
		#endregion
		
		#region pMainFrame methods
		void pMainFramePaint(object sender, PaintEventArgs e)
		{
			/*
			//This code may be unused......
			//try {	
				Graphics gscreen = e.Graphics;
				gscreen.InterpolationMode = InterpolationMode.HighQualityBicubic;

			//Now, transform the axis from local instance to window axis
			Matrix myTransform = new Matrix(1f, 0f, 0f, -1f, 0f, 0f); //reflection
			myTransform.Scale(scaleFactor, scaleFactor, MatrixOrder.Append);
			myTransform.Translate(translateX, translateY, MatrixOrder.Append);

			gscreen.DrawImage(contourBitmap, translateX, translateY, contourBitmap.Width * scaleFactor, contourBitmap.Height * scaleFactor);
	
				//bmp.Dispose();
				//gscreen.Dispose();
				
			//} catch (Exception ex) {
				
			//	MessageBox.Show(ex.ToString() + " " + ex.Message);
			//}
			*/
		}

		void PMainFrameMouseWheelScroll(object sender, MouseEventArgs e){
			
			float factor = 0.95F;
			
			//Zoom to the mouse location
			if (e.Delta > 0) {
				factor = 1.05F;
			}
			
			//PreProcessor.Draw.PanelMethods.ZoomToPoint((PointF)e.Location, factor, ref scaleFactor,
			//                                           ref translateX, ref translateY);
			pMainFrame.Invalidate();
		}
		
		void PMainFrameMouseClick(object sender, MouseEventArgs e){
			
			//PreProcessor.Draw.PanelMethods.ZoomToFit(pMainFrame, ref scaleFactor, ref translateX, ref translateY, InitialAssembly);
			pMainFrame.Invalidate();
		}
		
		void PMainFrameMouseEnter(object sender, EventArgs e){
			pMainFrame.Focus();
		}
		
		private void UpdateContourBitmap(){

			//create a new bitmap, then a graphic to paint with
			contourBitmap = new Bitmap(pMainFrame.Width, pMainFrame.Height,
			                        System.Drawing.Imaging.PixelFormat.Format32bppRgb);
			
			Graphics graphic = Graphics.FromImage(contourBitmap);
			graphic.Clear(System.Drawing.Color.White);
			
			
			
			//Draw the deformed configuration
			myPlot.Paint(graphic);
			
			//UpdateRanges
			tbHighRange.Text = Convert.ToString( myPlot.HighRange);
			tbLowRange.Text = Convert.ToString( myPlot.LowRange);
			
		}


		#endregion

		#region Button Methods
		void BLowColorClick(object sender, EventArgs e)
		{
			//add a color dialogue box
			
			ColorDialog myColor = new ColorDialog();
			
			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			myColor.Color = bLowColor.BackColor;
			
			if (myColor.ShowDialog() == DialogResult.OK)
			{
				bLowColor.BackColor = myColor.Color;
			}
		}
		
		void BHighColorClick(object sender, EventArgs e)
		{
			//add a color dialogue box
			
			ColorDialog myColor = new ColorDialog();
			
			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			myColor.Color = bHighColor.BackColor;
			
			if (myColor.ShowDialog() == DialogResult.OK)
			{
				bHighColor.BackColor = myColor.Color;
			}
		}
		
		void BUpdateClick(object sender, EventArgs e)
		{
			//try {
				UpdateAll();

			//} catch (Exception ex) {
			//	MessageBox.Show("Check your input!  " + ex.ToString());
			//}
			//Redraw only when update button is hit.
			UpdateContourBitmap();
			updateColorScaleBitmap();
			pMainFrame.Invalidate();
			spContourPlot.Panel2.Refresh();
		}

        protected void UpdateAll()
        {
            Color hc = bHighColor.BackColor;
            Color lc = bLowColor.BackColor;
			Color fc = bFiberColor.BackColor;
			Color pfc = bProjFiberColor.BackColor;
			Color bc = bBoundaryColor.BackColor;
			Color[] colorScheme = lColorSchemes[cbColorSchemes.SelectedIndex];

            int lev = (int)nudIsoLines.Value;
            int nxpths = (int)nudGridPtsX.Value;
            bool iso = cbIsoLines.Checked;

            //Update ranges before updating the data

            myPlot.HighColor = hc;
            myPlot.LowColor = lc;
			myPlot.FiberColor = fc;
			myPlot.ProjFiberColor = pfc;
			myPlot.BoundaryColor = bc;
            myPlot.ColorScheme = colorScheme;
            myPlot.PlotIsolines = iso;
			myPlot.ShowRotation = cbRotations.Checked;
			myPlot.ShowConnections = cbConnections.Checked;
			myPlot.ShowCrack = cbShowCrack.Checked;
			myPlot.ShowFiberNumbers = cbFiberNumbers.Checked;
            myPlot.NGridPts = nxpths;
            myPlot.NLevels = lev;
            myPlot.MatrixTransparency = tbMatrixTransparancy.Value;
			myPlot.ShowProjectionOfEverything = cbProjections.Checked;

            if (!cbAutomaticRange.Checked)
            {
                myPlot.HighRange = Convert.ToDouble(tbHighRange.Text);
                myPlot.LowRange = Convert.ToDouble(tbLowRange.Text);
            }

            myPlot.Update();

            if (cbAutomaticRange.Checked)
            {
                tbHighRange.Text = myPlot.HighRange.ToString();
                tbLowRange.Text = myPlot.LowRange.ToString();
            }

        }
		
		private void UpdateColorsList(){
			cbColorSchemes.Items.Clear();
			foreach (string sname in lColorSchemeNames) {
				cbColorSchemes.Items.Add(sname);
			}
			cbColorSchemes.Items.Add("Add new Scheme");
		}
		
		void CbColorSchemesSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbColorSchemes.SelectedIndex == (cbColorSchemes.Items.Count - 1)) {
				//Add one
				ColorSchemeMakerForm myCSMaker = new ColorSchemeMakerForm();
				if (myCSMaker.Success) {
					lColorSchemes.Add(myCSMaker.LColors.ToArray());
					lColorSchemeNames.Add(myCSMaker.SchemeName);
					UpdateColorsList();
				}
			}
		}
		
		void CbPlotTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			cbPlotComponent.Items.Clear();
			cbPlotComponent.Items.AddRange(sPlotComponent[cbPlotType.SelectedIndex]);
			cbPlotComponent.SelectedIndex = 0;

            myPlot.NPlotType = cbPlotType.SelectedIndex;
		}
		
		void CbPlotComponentSelectedIndexChanged(object sender, EventArgs e)
		{
            myPlot.NPlotComponent = cbPlotComponent.SelectedIndex;
		}

        private void cbAutomaticRange_CheckedChanged(object sender, EventArgs e)
        {
            myPlot.AutomaticRange = cbAutomaticRange.Checked;
        }

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			myPlot.MatrixTransparency = tbMatrixTransparancy.Value;
		}

		private void bFiberColor_Click(object sender, EventArgs e)
		{
			//add a color dialogue box

			ColorDialog myColor = new ColorDialog();

			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			myColor.Color = bFiberColor.BackColor;

			if (myColor.ShowDialog() == DialogResult.OK)
			{
				bFiberColor.BackColor = myColor.Color;
			}
		}

		private void bProjFiberColor_Click(object sender, EventArgs e)
		{
			//add a color dialogue box

			ColorDialog myColor = new ColorDialog();

			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			myColor.Color = bProjFiberColor.BackColor;

			if (myColor.ShowDialog() == DialogResult.OK)
			{
				bProjFiberColor.BackColor = myColor.Color;
			}
		}

		private void bBoundaryColor_Click(object sender, EventArgs e)
		{
			//add a color dialogue box

			ColorDialog myColor = new ColorDialog();

			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			myColor.Color = bBoundaryColor.BackColor;

			if (myColor.ShowDialog() == DialogResult.OK)
			{
				bBoundaryColor.BackColor = myColor.Color;
			}
		}

		private void bQueryPoint_Click(object sender, EventArgs e)
		{
			int nMatrix = cbFiberQueryPair.SelectedIndex;
			int nZ = cbZPointQuery.SelectedIndex;
			int nX = cbXPointQuery.SelectedIndex;
			int nY = cbYPointQuery.SelectedIndex;

			//[ztop1, ztop2, zbot1, zbot2]
			double zLoc = myPlot.lMatrix[nMatrix].lz[0][nZ];
			double yLoc = 0;
            switch (nY)
            {
				case 0:
					yLoc = myPlot.lMatrix[nMatrix].CalculateYAtLeftFiber(zLoc);
					break;

				case 1:
					yLoc = myPlot.lMatrix[nMatrix].ld12[0] / 2.0;
					break;
				case 2:
					yLoc = myPlot.lMatrix[nMatrix].CalculateYAtRightFiber(zLoc);
					break;
			}
			double xLoc = myPlot.lMatrix[nMatrix].b / 2.0 * nX;

			string sLocation = "x = " + xLoc + ", y = " + yLoc + ", z = " + zLoc;
			string sTitle = "Results between fibers " + cbFiberQueryPair.Text + " at " + sLocation;

			//make arrays for x and y:
			double[] nLS = new double[myPlot.lMatrix[nMatrix].lIteration.Count];
			double[] Result = new double[myPlot.lMatrix[nMatrix].lIteration.Count];

            for (int i = 0; i < nLS.Length; i++)
            {
				nLS[i] = myPlot.lMatrix[nMatrix].lIteration[i];
				Result[i] = myPlot.lMatrix[nMatrix].DecideOnOutput(cbPlotType.SelectedIndex, cbPlotComponent.SelectedIndex,
					xLoc, yLoc, zLoc, myPlot.lMatrix[nMatrix].lq[i], i);

			}


			SinglePlot.SinglePlotForm myPointQueryPlot = new SinglePlot.SinglePlotForm(sTitle, "LoadStep", (cbPlotType.Text + " " + cbPlotComponent.Text),
				sLocation, nLS, Result);

			myPointQueryPlot.Plot();
			

		}

		private void scContourPlot_Panel2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(colorScaleBitmap, 0f, 0f, colorScaleBitmap.Width, colorScaleBitmap.Height);

		}

		private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Copy")
			{
				//Graphics graphics = Graphics.FromImage(colorScaleBitmap);
				//graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				//graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				Clipboard.SetImage(colorScaleBitmap);
				//graphics.Flush();

			}
		}

		private void updateColorScaleBitmap()
        {
			colorScaleBitmap = new Bitmap(spContourPlot.Panel2.Width, spContourPlot.Panel2.Height, 
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			using (Graphics graphics = Graphics.FromImage(colorScaleBitmap))
			{
				graphics.Clear(Color.White);
				//graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				// uncomment for higher quality output
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				//graph.SmoothingMode = SmoothingMode.AntiAlias;

				//All these things can be set to change the look of the scale....
				float spacing = 0;
				float rectangleWidth = 30;
				float rectangleHeight = 20;
				float margin = 10;
				float fontSize = 10;

				SolidBrush rectBrush;
				SolidBrush fontBrush = new SolidBrush(Color.Black);
				Font font = new Font(new FontFamily("Arial"), fontSize);

				for (int i = 0; i < myPlot.levelColors.Length; i++)
				{
					rectBrush = new SolidBrush(myPlot.levelColors[myPlot.levelColors.Length - 1 - i]);
					graphics.FillRectangle(rectBrush, margin, margin + i * (rectangleHeight + spacing), rectangleWidth, rectangleHeight);

					if (i < myPlot.levels.Length)
					{
						graphics.DrawString(myPlot.levels[myPlot.levels.Length - 1 - i].ToString(), font, fontBrush, 1.5f * margin + rectangleWidth, margin + i * (rectangleHeight + spacing) + rectangleHeight - (float)(fontSize / 1.5));
					}
				}
			}
			
		}
		#endregion

		#region private methods
		/*
        public static void ZoomToFit(Panel panel, ref float ScaleFactor,
                                     ref float TranslateX, ref float TranslateY,
                                     Assembly.Assembly assembly)
        {
            float UpperLimit = 0;
            float LowerLimit = 0;
            float RightLimit = 0;
            float LeftLimit = 0;

            foreach (Instance inst in assembly.lInstances)
            {
                PointF[] pf = inst.rInstance;

                foreach (PointF pt in pf)
                {
                    if (UpperLimit < pt.Y)
                    {
                        UpperLimit = pt.Y;
                    }
                    if (LowerLimit > pt.Y)
                    {
                        LowerLimit = pt.Y;
                    }
                    if (RightLimit < pt.X)
                    {
                        RightLimit = pt.X;
                    }
                    if (LeftLimit > pt.X)
                    {
                        LeftLimit = pt.X;
                    }
                }



            }

            float totalHeight = Math.Abs(UpperLimit - LowerLimit);
            float totalWidth = Math.Abs(RightLimit - LeftLimit);

            float fJointRatio = totalHeight / totalWidth;
            float fPanelRatio = (float)panel.Height / (float)panel.Width;


            if (fJointRatio < fPanelRatio)
            {
                //Base the ScaleFactor on the width
                ScaleFactor = (float)(panel.Width / ((11.0 / 10.0) * totalWidth));

                TranslateX = (float)(panel.Width / 20.0 - LeftLimit * ScaleFactor);
                TranslateY = (float)((panel.Height - ScaleFactor * totalHeight) / 2 + UpperLimit * ScaleFactor);
            }
            else
            {
                //Base the ScaleFactor on the height
                ScaleFactor = (float)(panel.Height / ((11.0 / 10.0) * totalHeight));
                TranslateY = (float)(panel.Height / 20.0 + ScaleFactor * UpperLimit);
                TranslateX = (float)((panel.Width - ScaleFactor * totalWidth) / 2 - LeftLimit * ScaleFactor);
            }
        }
        */
		
		protected void PopulatePointQueryLists()
        {
			sFiberPairs = new string[myPlot.lMatrix.Count];
            for (int i = 0; i < myPlot.lMatrix.Count; i++)
            {
				sFiberPairs[i] = myPlot.lMatrix[i].nf1 + " / " + myPlot.lMatrix[i].nf2;
				cbFiberQueryPair.Items.Add(sFiberPairs[i]);
            }
			
			cbFiberQueryPair.SelectedIndex = 0;

			//Default in the center
			cbXPointQuery.SelectedIndex = 1;
			cbYPointQuery.SelectedIndex = 1;
			cbZPointQuery.SelectedIndex = 1;
		}

		
        #endregion

        
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
}
