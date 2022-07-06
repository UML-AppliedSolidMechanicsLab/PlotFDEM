using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using Animation;

namespace PlotFDEM
{
    public partial class PackPlot : Form
    {
        protected List<SimpleFiber> lFibers;
        protected SimpleBoundary boundary;
        public string fileName;
        protected StreamReader dataRead;
        protected float scaleFactor = 1f;
        protected float translateX = 0f;
        protected float translateY = 0f;
        protected float LowerLimit;
        protected float LeftLimit;
        protected float UpperLimit;
        protected float RightLimit;
        protected Color boundaryColor = Color.Red;

        public PackPlot()
        {
            //add an open dialogue box
            OpenFileDialog openFldr = new OpenFileDialog();
            openFldr.Title = "Select Pack File to Read From";
            openFldr.Filter = "CSV Files (*.csv*)|*.csv*";
            openFldr.FilterIndex = 2;
            openFldr.RestoreDirectory = true;

            // Show open file dialog box
            DialogResult result = openFldr.ShowDialog();

            // Process open file dialog box results
            if (result == DialogResult.OK)
            {

                //try {
                fileName = Path.GetFileNameWithoutExtension(openFldr.FileName);
                ReadFile(openFldr.FileName);

            }

            InitializeComponent();

            packingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.packingPanelPaint);
            packingPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.packingPanelMouseWheelScroll);
            packingPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.packingPanelMouseClick);
            packingPanel.MouseEnter += new EventHandler(PackingPanelMouseEnter);

            LowerLimit = boundary.Corners[0].Y;
            UpperLimit = boundary.Corners[2].Y;
            LeftLimit = boundary.Corners[0].X;
            RightLimit = boundary.Corners[2].X;

            PanelMethods.ZoomToFit(packingPanel, ref scaleFactor, ref translateX, ref translateY, LeftLimit, LowerLimit, RightLimit, UpperLimit);

            Show();
            this.Focus();
        }

        public virtual void ReadFile(string sFileName)
        {

            #region initiation stuff
            lFibers = new List<SimpleFiber>();

            dataRead = new StreamReader(sFileName);

            string tempLine = dataRead.ReadLine(); ; //Get through first line of labels
            string[] temp;
            int nEndFlag = 0;
            int counter = 0;
            char[] charsToTrim = { ',', '.', ' ' };

            #endregion

            while (nEndFlag == 0 && !dataRead.EndOfStream)
            {

                #region store line in temp
                tempLine = dataRead.ReadLine();
                tempLine = tempLine.Replace(" ", ""); //Get rid of empty spaces

                tempLine = tempLine.TrimEnd(charsToTrim);
                temp = tempLine.Split(',');
                #endregion

                if (temp.Length > 1)
                {
                    lFibers.Add(new SimpleFiber(counter, Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2])));
                    counter++;
                }
                else
                {
                    nEndFlag = 1;
                }
            }
            //Read the border if there is one
            try
            {
                tempLine = dataRead.ReadLine();
                tempLine = dataRead.ReadLine();
                tempLine = tempLine.Replace(" ", ""); //Get rid of empty spaces
                tempLine = tempLine.TrimEnd(charsToTrim);
                temp = tempLine.Split(',');

                if (temp.Length > 1)
                {
                    boundary = new SimpleBoundary(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]),
                    Convert.ToDouble(temp[3]), Convert.ToDouble(temp[4]), Convert.ToDouble(temp[5]));
                }
            }
            catch (Exception)
            {
                //create a ficticious boundary.....
                double miny, minz, maxy, maxz;
                miny = maxy = lFibers[0].Position[0];
                minz = maxz = lFibers[0].Position[1];
                foreach (SimpleFiber fiber in lFibers)
                {
                    miny = fiber.Position[0] < miny ? fiber.Position[0] : miny;
                    minz = fiber.Position[1] < minz ? fiber.Position[1] : minz;
                    maxy = fiber.Position[0] > maxy ? fiber.Position[0] : maxy;
                    maxz = fiber.Position[1] > maxz ? fiber.Position[1] : maxz;
                }
                boundary = new SimpleBoundary(0.0, miny, minz, 1.0, (maxy - miny), (maxz - minz));
                boundaryColor = Color.White;
            }
            
            

            dataRead.Close();
        }

        void packingPanelPaint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(packingPanel.Width, packingPanel.Height
                                    , System.Drawing.Imaging.PixelFormat.Format32bppRgb);
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

            Draw(graphic);

            //close the container
            graphic.EndContainer(transformContainer);

            //gscreen.InterpolationMode = InterpolationMode.HighQualityBicubic;


            Graphics gscreen = e.Graphics;
            gscreen.DrawImage(bmp, new PointF(0f, 0f));
            graphic.Dispose();
            bmp.Dispose();
        }

        void packingPanelMouseWheelScroll(object sender, MouseEventArgs e)
        {

            float factor = 0.95F;

            //Zoom to the mouse location
            if (e.Delta > 0)
            {
                factor = 1.05F;
            }

            PanelMethods.ZoomToPoint((PointF)e.Location, factor, ref scaleFactor,
                                     ref translateX, ref translateY);
            packingPanel.Invalidate();
        }

        void packingPanelMouseClick(object sender, MouseEventArgs e)
        {

            PanelMethods.ZoomToFit(packingPanel, ref scaleFactor, ref translateX, ref translateY, LeftLimit, LowerLimit, RightLimit, UpperLimit);
            packingPanel.Invalidate();
        }

        void PackingPanelMouseEnter(object sender, EventArgs e)
        {
            packingPanel.Focus();
        }

        public virtual void Draw(Graphics graphic)
        {

            //Color the background
            //graphic.Clear( Color.FromArgb(230,47,214,63));

            //draw the fibers
            foreach (SimpleFiber fi in lFibers)
            {
                fi.Draw(graphic, Color.FromArgb(230, 80, 90, 90));//dark grey//240,240,240)); //Color.DodgerBlue); //Add transform???
            }
            if (boundary != null)
            {
                boundary.Draw(graphic, boundaryColor); //Add transform???
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy")
            {
                Bitmap bm = new Bitmap(packingPanel.Width, packingPanel.Height
                                   , System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                packingPanel.DrawToBitmap(bm, new Rectangle(0, 0, packingPanel.Width, packingPanel.Height));
                Graphics graphics = Graphics.FromImage(bm);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                Clipboard.SetImage(bm);
                graphics.Flush();

            }
        }
    }
    public class SimpleFiber
    {

        public double[] Position;
        double Radius;
        public int fiberIndex;


        public SimpleFiber(int fiberIndex, double x, double y, double r)
        {
            this.fiberIndex = fiberIndex;
            Position = new double[2] { x, y };
            Radius = r;
        }
        
        public void DrawFiberNumbers(Graphics graphic, Color color)
        {
            //Now, transform the axis from local instance to window axis
            Matrix myTransform = new Matrix(1f, 0f, 0f, -1f, 0f, 0f); //reflection

            //Now, make a container to apply the transfrom to the container only
            GraphicsContainer transformContainer = graphic.BeginContainer();
            graphic.Transform = myTransform;

            SolidBrush brush = new SolidBrush(color);
            Font font = new Font(new FontFamily("Arial"), (int)(Radius / 2.0));
            graphic.DrawString(fiberIndex.ToString(), font, brush, (float)(Position[0] - Radius / 4.0), -(float)(Position[1] + Radius / 4.0));
            brush.Dispose();

            //close the container because it's awesome.)
            graphic.EndContainer(transformContainer);
        }

        public void Draw(Graphics graphic, Color objectColor)
        {
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush fiberBrush = new SolidBrush(objectColor);
            graphic.FillEllipse(fiberBrush, (float)(Position[0] - Radius), (float)(Position[1] - Radius), (float)(2.0 * Radius), (float)(2.0 * Radius));
            fiberBrush.Dispose();
        }

    }

    public class SimpleBoundary
    {

        public PointF[] Corners;

        public SimpleBoundary(double x, double y, double z, double lx, double ly, double lz)
        {
            Corners = new PointF[4];
            Corners[0] = new PointF( (float)y, (float)z);
            Corners[1] = new PointF((float)(y + ly), (float)(z));
            Corners[2] = new PointF((float)(y + ly), (float)(z + lz));
            Corners[3] = new PointF((float)(y), (float)(z + lz));
        }

        public void Draw(Graphics graphic, Color objectColor)
        {
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.SmoothingMode = SmoothingMode.AntiAlias;

            Pen cellWallBrush = new Pen(objectColor, 0.3f);
            cellWallBrush.DashStyle = DashStyle.Dot;
            cellWallBrush.DashOffset = 40;

            graphic.DrawPolygon(cellWallBrush, Corners);
            cellWallBrush.Dispose();
        }
    }

}
