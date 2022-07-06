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
    public class ConnectionPlot : PackPlot
    {
        private List<SimpleConnection> lConnections;

        public ConnectionPlot()
        {

        }

        public override void ReadFile(string sFileName)
        {

            #region initiation stuff
            lConnections = new List<SimpleConnection>();

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
                    lConnections.Add(new SimpleConnection(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]), Convert.ToDouble(temp[3])));
                    counter++;
                }
                else
                {
                    nEndFlag = 1;
                }
            }

            //Now make a border
            double minx, miny, maxx, maxy;
            minx = maxx = lConnections[0].Pt1.X;
            miny = maxy = lConnections[0].Pt1.Y;
            foreach (SimpleConnection con in lConnections)
            {
                minx = con.Pt1.X < minx ? con.Pt1.X : minx;
                miny = con.Pt1.Y < miny ? con.Pt1.Y : miny;
                maxx = con.Pt1.X > maxx ? con.Pt1.X : maxx;
                maxy = con.Pt1.Y > maxy ? con.Pt1.Y : maxy;
            }
            boundary = new SimpleBoundary(0.0, minx, miny, 1.0, (maxx - minx), (maxy - miny));
            boundaryColor = Color.White;

            dataRead.Close();
        }
        public override void Draw(Graphics graphic)
        {

            //Color the background
            //graphic.Clear( Color.FromArgb(230,47,214,63));

            //draw the fibers
            foreach (SimpleConnection con in lConnections)
            {
                con.Draw(graphic, Color.FromArgb(230, 80, 90, 90));//dark grey//240,240,240)); //Color.DodgerBlue); //Add transform???
            }
            if (boundary != null)
            {
                boundary.Draw(graphic, boundaryColor); //Add transform???
            }
        }
    }


    public class SimpleConnection
    {

        public PointF Pt1;
        public PointF Pt2;

        public SimpleConnection(double x1, double y1, double x2, double y2)
        {
            Pt1 = new PointF((float)x1, (float)y1);
            Pt2 = new PointF((float)x2, (float)y2);
        }

        public void Draw(Graphics graphic, Color objectColor)
        {
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.SmoothingMode = SmoothingMode.AntiAlias;

            Pen cellWallBrush = new Pen(objectColor, 0.3f);

            graphic.DrawLine(cellWallBrush, Pt1, Pt2);
            cellWallBrush.Dispose();
        }
    }
}
