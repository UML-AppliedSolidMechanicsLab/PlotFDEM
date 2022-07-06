/*
 * Created by SharpDevelop.
 * User: Scott
 * Date: 9/1/2011
 * Time: 8:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Animation;


namespace PlotFDEM.MatrixContinuum.ContourPlot
{
	/// <summary>
	/// Description of ContourPlotMaker.
	/// </summary>
	public class ContourPlotMaker : Animation.IDrawGraphic
    {
		#region Private Members
		protected int nLevels; //Levels are how many isoregions there will be
		protected Color highColor;
		protected Color lowColor;
        protected Color fiberColor;
        protected Color projFiberColor;
        protected Color boundaryColor;
        protected Color [] colorScheme;
		protected double highRange;
		protected double lowRange;
		protected int nGridPts; //number of grid points in the x direction
		protected int nPlotType;
		protected int nPlotComponent;
        protected AnimatedPlot myAnimation;
        protected int matrixTransparency;
        protected int[] ascendingOrderOfMatrixIndices;

        //Status indicators
        protected bool bChangeLevels;
		protected bool bChangeColor;
		protected bool bChangeGrid;
		protected bool bChangeRange;
		protected bool bChangePlotType;
		protected bool bHasLines;
        private bool bAutomaticRange;
        private bool bShowContact;
        private bool bShowConnection;
        private bool bShowCrack;
        private bool bShowRotation;
        private bool bShowFiberNumbers;
        private bool bShowProjections;

        protected double maxRange;
		protected double minRange;

        //Objects to plot
        private List<Fiber> lFibers;
        private List<Fiber[]> lProjFibers;
        private Boundary boundary;
        public List<MatrixContinuum> lMatrix;
        private List<Contact> lContacts;
        private StressStrain results;

        public double [] levels;
		public Color [] levelColors;
		protected float IsolinePenThickness;
        public double maxContactForce;

        #endregion

        #region Public Members
        
        public int NLevels {
			get { return nLevels; }
			set {if (nLevels != value) {
					nLevels = value;
					bChangeLevels = true;}}
		}
		public Color HighColor {
			get { return highColor; }
			set { if (highColor != value) {
					highColor = value;
					bChangeColor = true;}}
		}
		public Color LowColor {
			get { return lowColor; }
			set { if (lowColor != value) {
					lowColor = value;
					bChangeColor = true;
				}}
		}
        public Color FiberColor
        {
            get { return fiberColor; }
            set
            {
                if (fiberColor != value)
                {
                    fiberColor = value;
                    bChangeColor = true;
                }
            }
        }
        public Color ProjFiberColor
        {
            get { return projFiberColor; }
            set
            {
                if (projFiberColor != value)
                {
                    projFiberColor = value;
                    bChangeColor = true;
                }
            }
        }
        public Color BoundaryColor
        {
            get { return boundaryColor; }
            set
            {
                if (boundaryColor != value)
                {
                    boundaryColor = value;
                    bChangeColor = true;
                }
            }
        }
        public Color[] ColorScheme {
			get { return colorScheme; }
			set { if(colorScheme != value){
					colorScheme = value;
					bChangeColor = true;
				}}
		}
		public double HighRange {
			get { return highRange; }
			set { if (highRange != value) {
					highRange = value;
					bChangeRange = true;}
			}
		}
		public double LowRange {
			get { return lowRange; }
			set { if (lowRange != value) {
					lowRange = value;
					bChangeRange = true;}
			}
		}
		public int NGridPts {
			get { return nGridPts; }
			set { if (nGridPts != value) {
					nGridPts = value;
					bChangeGrid = true;
				}}
		}
		public bool PlotIsolines {
			get { return bHasLines; }
			set { bHasLines = value; }
		}
		public int NPlotComponent{
			get { return nPlotComponent; }
			set { nPlotComponent = value;
				bChangePlotType = true;}
		}
		public int NPlotType{
			get { return nPlotType; }
			set { nPlotType = value;
				bChangePlotType = true;}
		}
        public bool AutomaticRange
        {
            get { return bAutomaticRange; }
            set { bAutomaticRange = value;
                bChangeRange = true;
            }
        }
        public bool ShowConnections
        {
            get { return bShowConnection; }
            set { bShowConnection = value;}
        }
        public bool ShowRotation
        {
            get { return bShowRotation; }
            set { bShowRotation = value; }
        }
        public bool ShowCrack
        {
            get { return bShowCrack; }
            set { bShowCrack = value; }
        }
        public bool ShowFiberNumbers
        {
            get { return bShowFiberNumbers; }
            set { bShowFiberNumbers = value; }
        }
        
        public bool ShowProjectionOfEverything
        {
            get { return bShowProjections; }
            set { bShowProjections = value; ; }
        }
        public int MatrixTransparency
        {
            get { return matrixTransparency; }
            set{matrixTransparency = value;
                myAnimation.Refresh();
            }
        }
        #endregion

        #region Constructors
        public ContourPlotMaker()
        {
            bAutomaticRange = true;
            bShowConnection = false;
            bShowRotation = false;
            bShowFiberNumbers = false;
            //Read CSV
            ReadCSV myData = new ReadCSV();

            //Now load the objects to plot
            lFibers = myData.lFibers;
            lContacts = myData.lContacts;
            boundary = myData.boundary;
            lProjFibers = myData.lProjFibers;
            results = myData.results;
            maxContactForce = myData.maxContactForce;
            lMatrix = myData.lMatrix;
            ascendingOrderOfMatrixIndices = Enumerable.Range(0, lMatrix.Count).ToArray();

            //Get the corners
            float[] corners = boundary.FindExtremeCorners();
            //Create the animation
            myAnimation = new AnimatedPlot(this, results.lPointList, results.Labels, myData.fileName, "Iterations", "Stress/Strain",
                                                                            corners[0], corners[1], corners[2], corners[3]);

            InitializeContourPlot();

        }

        private void InitializeContourPlot()
        {
            //Start out with initial numbers and generate a plot
            nPlotType = 0;
            nPlotComponent = 0;
            nGridPts = 1;
            HighColor = Color.Black;
            LowColor = Color.Gray;
            ProjFiberColor = Color.FromArgb(230, 50, 60, 60);
            FiberColor = Color.FromArgb(230, 80, 90, 90);
            BoundaryColor = Color.White;
            colorScheme = new Color[5] { Color.Blue, Color.Aqua, Color.LimeGreen, Color.Yellow, Color.Red };
            NLevels = 20;
            bChangeGrid = bChangeLevels = bAutomaticRange = bChangeColor = bChangePlotType = bChangeRange = true;
            Update();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This draws everything onto the animation pane
        /// </summary>
        public bool IsClosed()
        {
            return myAnimation.IsDisposed;
        }
        public void Draw(Graphics graphic, int i)
        {
            DrawEverything(graphic, i);

            //Draw a projection of everything
            if (bShowProjections)
            {
                //Get current iteration
                int currentIteration = myAnimation.currentFrame;
                PointF[] corners = boundary.FindCorners(currentIteration);
                PointF lr = Subtract(corners[1], corners[0]);
                PointF ur = Subtract(corners[2], corners[0]);
                PointF ul = Subtract(corners[3], corners[0]);
                
                //Right
                DrawTranslated(graphic, i, lr.X, lr.Y);
                //Left
                DrawTranslated(graphic, i, -1f * lr.X, -1f * lr.Y);
                //Top
                DrawTranslated(graphic, i, ul.X, ul.Y);
                //Bottom
                DrawTranslated(graphic, i, -1f * ul.X, -1f * ul.Y);
                //BottomLeft
                DrawTranslated(graphic, i, -1f * ur.X, -1f * ur.Y);
                //TopRight
                DrawTranslated(graphic, i, ur.X, ur.Y);
                //BottomRight
                DrawTranslated(graphic, i, lr.X - ul.X, lr.Y - ul.Y);
                //TopLeft
                DrawTranslated(graphic, i, ul.X - lr.X, ul.Y - lr.Y);
                
            }
        }

        private void DrawTranslated(Graphics graphic, int i, float translateX, float translateY)
        {
            //Not sure why I have to do this, but somehow it doesn't really add on the previous offset...
            
            GraphicsContainer graphicsContainer = graphic.BeginContainer();
            graphic.TranslateTransform(translateX, translateY);
            DrawEverything(graphic, i);
            graphic.EndContainer(graphicsContainer);
        }
        
        private void DrawEverything(Graphics graphic, int i)
        {
            Pen isolinePen = new Pen(Color.Black, IsolinePenThickness);

            //Set the transparency!
            Color[] tempLevels = new Color[levelColors.Length];
            for (int j = 0; j < levelColors.Length; j++)
            {
                int alpha = levelColors[j].A - matrixTransparency;
                alpha = (alpha < 0) ? 0 : alpha;
                tempLevels[j] = Color.FromArgb(alpha, levelColors[j]);
            }

            //Plot the matrix  
            for (int j = 0; j < lMatrix.Count; j++)
            {
                lMatrix[ascendingOrderOfMatrixIndices[j]].Draw(i, graphic, tempLevels);
                if (bHasLines)
                {
                    lMatrix[ascendingOrderOfMatrixIndices[j]].DrawIsoLines(i, graphic, isolinePen);
                }
            }
            isolinePen.Dispose();

            if (bShowContact)
            {
                foreach (Contact sp in lContacts)
                {
                    //sp.ContactScaleFactor = (Max(homogenizedStress) / lFibers.Count * cBoundary.ODimensions[1] * cBoundary.ODimensions[2]*200);//TODO Get some scaling method that makes more sense Math.Abs(homogenizedStress.Max());
                    sp.Draw(graphic, i, maxContactForce); //Add transform???
                }
            }

            //draw the fibers
            foreach (Fiber[] fi in lProjFibers)
            {
                foreach (Fiber f in fi)
                {
                    f.Draw(graphic, i, projFiberColor); ////230, 50, 60, 60));//DarkerGray//220,220,220));//50, 60, 60));//DarkerGray //Color.DodgerBlue); //Add transform???
                }
            }
            foreach (Fiber fi in lFibers)
            {
                fi.Draw(graphic, i, fiberColor); ////230, 80, 90, 90));//dark grey//240,240,240)); //Color.DodgerBlue); //Add transform???
            }
            if (bShowRotation)
            {
                foreach (Fiber fi in lFibers)
                {
                    fi.DrawOrientation(graphic, i);
                }
            }

            //draw the boundary
            boundary.Draw(graphic, i, boundaryColor); //Add transform???

            //Plot the connection between fibers
            if (bShowConnection)
            {
                foreach (MatrixContinuum m in lMatrix)
                {
                    m.DrawConnections(i, graphic);
                }
            }

            //Plot the Crack
            if (bShowCrack)
            {
                foreach (MatrixContinuum m in lMatrix)
                {
                    m.DrawCrack(i, graphic);
                }
            }

            //Draw the fiber numbers
            if (bShowFiberNumbers)
            {

                foreach (Fiber fi in lFibers)
                {
                    fi.DrawFiberNumbers(graphic, i, Color.Black);
                }
                foreach (Fiber[] pf in lProjFibers)
                {
                    foreach (Fiber pfi in pf)
                    {
                        pfi.DrawFiberNumbers(graphic, i, Color.White);
                    }
                }

            }
        }
        public void Update()
        {
            //Do this one first in case the levels need to be updated
            if (bChangeGrid)
            {
                for (int i = 0; i < lMatrix.Count; i++)
                {
                    lMatrix[ascendingOrderOfMatrixIndices[i]].UpdatePointsOnly(nGridPts);
                }
            }

            //update the results if needed
            if (bChangeGrid || bChangePlotType)
            {

                for (int i = 0; i < lMatrix.Count; i++)
                {
                    lMatrix[ascendingOrderOfMatrixIndices[i]].UpdateResultsOnly(nPlotType, nPlotComponent);
                }
            }

            //Update the range if the range should be automated
            if ((bChangeRange || bChangePlotType) && bAutomaticRange)
            {
                ascendingOrderOfMatrixIndices = FindMaxMinAndOutputAscendingOrder(ref maxRange, ref minRange);
                highRange = maxRange;
                lowRange = minRange;
            }

            //Update create the levels
            if (bChangeLevels || bChangeRange)
            {
                CreateLevels();
            }

            //Create new colors if needed
            if (bChangeLevels || bChangeRange || bChangeColor)
            {
                CreateLevelColors();
            }

            //Now finish updating the results and contours
            if (bChangeGrid || bChangePlotType || bChangeLevels || bChangeRange)  
            {
                for (int i = 0; i < lMatrix.Count; i++)
                {
                    lMatrix[ascendingOrderOfMatrixIndices[i]].UpdateContoursOnly(levels);
                }
            }
			
			//Reset all of the change flags
			bChangeGrid = false;
			bChangeColor = false;
			bChangeLevels = false;
			bChangeRange = false;
			bChangePlotType = false;

            myAnimation.Activate();
            myAnimation.Refresh();
		}
		
		public void Paint(Graphics graphic){
			
            //Here, draw the legend

            //Old Code
			/*ColorGrids(myTransform, graphic);
			
			if (bHasLines) {
				DrawIsoLines(myTransform, graphic);
			}*/
		}
		
		/// Find the minimum and maximum of the data (z) to get the automatic color bounds
		public int[] FindMaxMinAndOutputAscendingOrder(ref double max, ref double min)
        {
            //Create an array of ascending integers to show how the indices are changed
            int[] ascendingIndices = Enumerable.Range(0, lMatrix.Count).ToArray();
            double[] maxAbsoluteValue = new double[lMatrix.Count];
            double[] firstMaxAbsoluteValue = new double[lMatrix.Count];

            //This is just to get the list started, so that the min isn't always 0
            lMatrix[0].FindMaxAndMinResults(ref max, ref min);
            double tempMax = 0;
            double tempMin = 0;

            for (int i = 0; i < lMatrix.Count; i++)
            {
                tempMax = tempMin = 0.0;

                lMatrix[i].FindMaxAndMinResults(ref tempMax, ref tempMin);
                if (tempMax > max)
                {
                    max = tempMax;
                }
                if (tempMin < min)
                {
                    min = tempMin;
                }

                maxAbsoluteValue[i] = Math.Max(tempMax, Math.Abs(tempMin));

                //now find the max of the first iteration only
                tempMax = tempMin = 0.0;
                lMatrix[i].FindMaxAndMinResults(1, ref tempMax, ref tempMin);
                firstMaxAbsoluteValue[i] = Math.Max(tempMax, Math.Abs(tempMin));
            }
            //Now sort the max values in ascending order for the first iteration 
            
            Array.Sort(firstMaxAbsoluteValue, ascendingIndices);

            return ascendingIndices;
		}
		
		#endregion
		
		#region Private Methods
		
		private void CreateLevels(){
			//Assign the levels of the contour lines / isoregions
			levels = new double[nLevels + 1];
			double dLevels = (highRange - LowRange) / nLevels;
			for (int i = 0; i < nLevels + 1; i++) {
				levels[i] = LowRange + i * dLevels;
			}
		}
		
		private void CreateLevelColors(){
			//Split up the colors into R, G, and B values, then find ranges between the two
			levelColors = new Color[nLevels+2];
			//This makes it black outside of the levels
			levelColors[0] = lowColor;
			levelColors[nLevels+1] = highColor;
			int nColors = colorScheme.Length;
			//double dR = (double)((highColor.R - lowColor.R) / nLevels);
			//double dG = (double)((highColor.G - lowColor.G) / nLevels);
			//double dB = (double)((highColor.B - lowColor.B) / nLevels);
			double colorsPerLevel = (nColors-1.0) / (nLevels-1);
			int R, G, B, A, c0;
			double remainder;
			for (int i = 1; i < nLevels; i++) {
				double cN = (i-1.0)*colorsPerLevel;
				c0 = (int)(Math.Floor(cN));
				remainder = cN - c0;
				
				R = (int)(colorScheme[c0].R + remainder * (colorScheme[c0 + 1].R - colorScheme[c0].R));
				G = (int)(colorScheme[c0].G + remainder * (colorScheme[c0 + 1].G - colorScheme[c0].G));
				B = (int)(colorScheme[c0].B + remainder * (colorScheme[c0 + 1].B - colorScheme[c0].B));
                A = (int)(colorScheme[c0].A + remainder * (colorScheme[c0 + 1].A - colorScheme[c0].A));
                levelColors[i] = Color.FromArgb(A,R,G,B);
			}
            //Now load the top one
            levelColors[nLevels] = colorScheme[colorScheme.Length - 1];
        }

        public static PointF Subtract(PointF a, PointF b)
        {
            PointF result = new PointF(a.X - b.X, a.Y - b.Y);
            return result;
        }
        #endregion
    }
}
