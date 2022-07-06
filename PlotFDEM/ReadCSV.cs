/*
 * Created by SharpDevelop.
 * User: Scott
 * Date: 5/28/2014
 * Time: 1:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Drawing.Drawing2D;
using System.Drawing;
using ZedGraph;
using System.Windows.Forms;
using myMath;
using PlotFDEM.MatrixContinuum;

namespace PlotFDEM
{
    /// <summary>
    /// Description of ReadCSV.
    /// </summary>
    public class ReadCSV
    {
        private StreamReader dataRead;
        private int nCount = 0;
        public double maxSizingForce = 0;
        public double maxContactForce = 0;

        public int maxIteration;
        public List<Fiber> lFibers;
        public List<Fiber[]> lProjFibers;
        public Boundary boundary;
        public List<Contact> lContacts;  //Rather than track which contacts are which, I'll just make a new contact for each one, and each iteration
        public List<Contact> lSizing;  //Rather than track which contacts are which, I'll just make a new contact for each one, and each iteration
        public List<MatrixContinuum.MatrixContinuum> lMatrix;
        public StressStrain results;
        public string fileName;
        iMatrixModel matrixModel;

        public ReadCSV()
        {

            //add an open dialogue box
            OpenFileDialog openFldr = new OpenFileDialog();
            openFldr.Title = "Select File to Read From";
            openFldr.Filter = "CSV Files (*.csv*)|*.csv*";
            openFldr.FilterIndex = 2;
            openFldr.RestoreDirectory = true;

            // Show open file dialog box
            DialogResult result = openFldr.ShowDialog();

            // Process open file dialog box results
            if (result == DialogResult.OK) {

                //try {
                fileName = Path.GetFileNameWithoutExtension(openFldr.FileName);
                ReadFile(openFldr.FileName);

            }
        }

        public void ReadFile(string sFileName) {

            #region initiation stuff
            lFibers = new List<Fiber>();
            boundary = new Boundary();
            lProjFibers = new List<Fiber[]>();
            lContacts = new List<Contact>();
            lSizing = new List<Contact>();
            results = new StressStrain();
            lMatrix = new List<MatrixContinuum.MatrixContinuum>();

            dataRead = new StreamReader(sFileName);

            int iteration = 0;
            int nWall = 0;
            int nFiber = 0;
            int nPFiber = 0;
            int nContact = 0;
            int nSizing = 0;
            matrixModel = new MatrixContinuumRigidFiberModel();

            string tempLine = "Initiate"; //temporary location of lines
            string[] temp;
            int nEndFlag = 0;
            string sSection = "None";

            #endregion

            while (nEndFlag == 0) {

                #region store line in temp
                tempLine = dataRead.ReadLine();
                tempLine = tempLine.Replace(" ", ""); //Get rid of empty spaces
                char[] charsToTrim = { ',', '.', ' ' };

                tempLine = tempLine.TrimEnd(charsToTrim);
                temp = tempLine.Split(',');
                #endregion

                #region If the line is a comment...
                if (temp[0].Contains("*")) { //

                    if (!temp[0].Contains("**")) { //gets rid of the comments

                        sSection = temp[0];

                        if (sSection == "*END") { //ends it if the section is "end"
                            nEndFlag = 1;
                        }
                    }
                }
                #endregion

                else { //For Data Members

                    switch (sSection) {
                        case "*GeneralOutput":
                            #region General Output
                            results.Add(new double[12]{Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]), Convert.ToDouble(temp[3]), Convert.ToDouble(temp[4]), Convert.ToDouble(temp[5]),
                                            Convert.ToDouble(temp[6]), Convert.ToDouble(temp[7]), Convert.ToDouble(temp[8]), Convert.ToDouble(temp[9]),
                                            Convert.ToDouble(temp[10]),Convert.ToDouble(temp[11]),Convert.ToDouble(temp[12])});
                            break;
                        #endregion

                        case "*Iteration":
                            #region Sets the iteration
                            iteration = Convert.ToInt32(temp[0]);
                            nWall = 0;
                            nFiber = 0;
                            nPFiber = 0;
                            nContact = 0;
                            break;
                        #endregion

                        case "*Fibers":
                            #region Add a fiber
                            int currIndex = Convert.ToInt32(temp[0]);
                            bool isProjected = Convert.ToBoolean(temp[1]);
                            double r = Convert.ToDouble(temp[6]);
                            double l = Convert.ToDouble(temp[7]);
                            double p1y = Convert.ToDouble(temp[3]);
                            double p1z = Convert.ToDouble(temp[4]);
                            double rot = Convert.ToDouble(temp[5]);
                            if (currIndex != -1) { //If the fiber is the original

                                if (iteration == 0) {
                                    lFibers.Add(new Fiber(lFibers.Count));
                                    lProjFibers.Add(new Fiber[3] { new Fiber(lFibers.Count - 1), new Fiber(lFibers.Count - 1), new Fiber(lFibers.Count - 1) });
                                }
                                lFibers[nFiber].Add(p1y, p1z, r, l, rot, isProjected, true);
                                lProjFibers[nFiber][0].Add(0, 0, r, l, rot, true, false);
                                lProjFibers[nFiber][1].Add(0, 0, r, l, rot, true, false);
                                lProjFibers[nFiber][2].Add(0, 0, r, l, rot, true, false);
                                nFiber++;
                                nPFiber = 0;
                            }
                            else { //Add projected fibers
                                lProjFibers[nFiber - 1][nPFiber].Add(p1y, p1z, r, l, rot, true, true, true);
                                nPFiber++;
                            }

                            break;
                        #endregion

                        case "*Boundaries":
                            #region Add to a wall object in the list

                            if (nWall > 1) {
                                double[] cent = new double[2] { Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]) };
                                double[] norm = new double[2] { Convert.ToDouble(temp[4]), Convert.ToDouble(temp[5]) };
                                boundary.Add((nWall - 2), cent, norm);
                            }
                            nWall++;
                            break;
                        #endregion

                        case "*Contacts":
                            #region Add contacts to the list
                            ReadContacts(iteration, ref nContact, ref nSizing, temp);
                            break;
                            #endregion

                    }
                }
                nCount++;
            }

            maxIteration = iteration;
            results.Done();
            dataRead.Close();
        }

        virtual protected void ReadContacts(int iteration, ref int nContact, ref int nSizing, string[] temp)
        {

            int fIndex1 = Convert.ToInt32(temp[0]);
            int pfIndex1 = Convert.ToInt32(temp[1]);
            int fIndex2 = Convert.ToInt32(temp[2]);
            int pfIndex2 = Convert.ToInt32(temp[3]);

            Fiber f1 = lFibers[fIndex1];
            Fiber f2 = lFibers[fIndex2];

            if (pfIndex1 != -1)
            {
                f1 = lProjFibers[fIndex1][pfIndex1];
            }
            if (pfIndex2 != -1)
            {
                f2 = lProjFibers[fIndex2][pfIndex2];
            }

            //Types which take in a coordinate (where the pair can change
            if (temp[4] == "Sizing" || temp[4] == "Matrix" || temp[4] == "Contact")
            {
                //Find the coordinate
                double P1y = f1.lPositions[iteration][0];
                double P1z = f1.lPositions[iteration][1];
                double P2y = f2.lPositions[iteration][0];
                double P2z = f2.lPositions[iteration][1];

                //Now differentiate between contact and sizing
                if (temp[4] == "Sizing" || temp[4] == "Matrix")
                {
                    if (lSizing.Count - 1 < nSizing)
                    {
                        lSizing.Add(new Contact());
                    }
                    lSizing[nSizing].Add(P1y, P1z, P2y, P2z, Convert.ToDouble(temp[5]), iteration, ref maxSizingForce);
                    //only add the "isbroken" thing if it is there.  If it is not there, assume it isn't broken.  This makes legacy files still work.
                    try
                    {
                        bool isBroken = Convert.ToBoolean(temp[6]);
                        lSizing[nSizing].isBroken = isBroken;
                    }
                    catch (Exception)
                    {

                    }
                    nSizing++;
                }
                //If it is contact
                else
                {
                    if (lContacts.Count - 1 < nContact)
                    {
                        lContacts.Add(new Contact());
                    }
                    lContacts[nContact].Add(P1y, P1z, P2y, P2z, Convert.ToDouble(temp[5]), iteration, ref maxContactForce);
                    nContact++;
                }
            }
            
            else if (temp[4] == "MatrixContinuum")
            {
                matrixModel = new MatrixContinuumRigidFiberModel(f1, f2, Convert.ToDouble(temp[6]), Convert.ToDouble(temp[7]), Convert.ToDouble(temp[5]));
                lMatrix.Add(new MatrixContinuum.MatrixContinuum( f1, f2, matrixModel));
                
            }
            else if (temp[4] == "MatrixContinuumElasticFibers")
            {
                matrixModel = new MatrixContinuumElasticFiberModel(f1, f2, Convert.ToDouble(temp[6]), Convert.ToDouble(temp[7]), Convert.ToDouble(temp[5]));
                lMatrix.Add(new MatrixContinuum.MatrixContinuum(f1, f2, matrixModel));

            }

            else if (temp[4] == "MatrixContinuumElasticFibers_Damage")
            {
                matrixModel = new MatrixContinuumElasticFiberDamageModel(f1, f2, Convert.ToDouble(temp[6]), Convert.ToDouble(temp[7]), Convert.ToDouble(temp[5]),
                    Convert.ToDouble(temp[8]), Convert.ToDouble(temp[9]), Convert.ToDouble(temp[10]), Convert.ToDouble(temp[11]));
                lMatrix.Add(new MatrixContinuum.MatrixContinuum(f1, f2, matrixModel));

            }

            //This is if it is matrix continuum, but not the first
            else if (temp[4] == "m")
            {
                foreach (MatrixContinuum.MatrixContinuum matrixContinuum in lMatrix)
                {
                    if (matrixContinuum.nf1 == fIndex1 && matrixContinuum.nf2 == fIndex2)
                    {
                        if (matrixModel is MatrixContinuumRigidFiberModel)
                        {
                            double t1 = Convert.ToDouble(temp[9]);
                            double t2 = Convert.ToDouble(temp[10]);
                            double[] q = new double[] {Convert.ToDouble(temp[5]), Convert.ToDouble(temp[6]) , Convert.ToDouble(temp[7]) , Convert.ToDouble(temp[8]),
                        Math.Sin(t1), 1.0 - Math.Cos(t1), Math.Sin(t2), 1.0 - Math.Cos(t2)};

                            double[] zIntBounds = new double[] { Convert.ToDouble(temp[11]), Convert.ToDouble(temp[12]), Convert.ToDouble(temp[13]), Convert.ToDouble(temp[14]) };
                            matrixContinuum.Add(q, zIntBounds, iteration);

                        }

                        else if (matrixModel is MatrixContinuumElasticFiberDamageModel)
                        {
                            double[] q = new double[] {Convert.ToDouble(temp[5]), Convert.ToDouble(temp[6]) , Convert.ToDouble(temp[7]) , Convert.ToDouble(temp[8]),
                                Convert.ToDouble(temp[9]),Convert.ToDouble(temp[10]), Convert.ToDouble(temp[11]), Convert.ToDouble(temp[12]),Convert.ToDouble(temp[13])};

                            double[] Damage = new double[temp.Length - 14];

                            for (int i = 0; i < Damage.Length; i++)
                            {
                                Damage[i] = Convert.ToDouble(temp[i + 14]);
                            }
                            matrixContinuum.Add(q, Damage, iteration, true);
                        }

                        else if (matrixModel is MatrixContinuumElasticFiberModel)
                        {
                            double[] q = new double[] {Convert.ToDouble(temp[5]), Convert.ToDouble(temp[6]) , Convert.ToDouble(temp[7]) , Convert.ToDouble(temp[8]),
                        Convert.ToDouble(temp[9]),Convert.ToDouble(temp[10]), Convert.ToDouble(temp[11]), Convert.ToDouble(temp[12]),Convert.ToDouble(temp[13])};

                            double[] zIntBounds = new double[] { Convert.ToDouble(temp[14]), Convert.ToDouble(temp[15]), Convert.ToDouble(temp[16]), Convert.ToDouble(temp[17]) };
                            matrixContinuum.Add(q, zIntBounds, iteration);
                        }

                        
                    }
                }
            }
        }
    }

    public class Fiber {

        public List<double[]> lPositions;
        private List<double> lRotations;
        public List<double> lRadii;
        private List<bool> lIsBorder;
        private List<bool> lIsVisible;
        public List<double> lLength;
        public int fiberIndex;

        private static double maxVelocity = 0;
        private static double sumVelocity = 0;
        private static double sum2Velocity = 0;
        private static int nVelocity = 0;
        private static double penThick = 0.5;

        public Fiber(int fiberIndex) {
            this.fiberIndex = fiberIndex;
            lPositions = new List<double[]>();
            lRotations = new List<double>();
            lRadii = new List<double>();
            lIsBorder = new List<bool>();
            lIsVisible = new List<bool>();
            lLength = new List<double>();
        }
        public void Add(double inY, double inZ, double inRadii, double inLength, double inRotation, bool inIsBorder, bool inIsVisible) {
            lPositions.Add(new double[2] { inY, inZ });
            lRotations.Add(inRotation);
            lRadii.Add(inRadii);
            lIsBorder.Add(inIsBorder);
            lIsVisible.Add(inIsVisible);
            lLength.Add(inLength);
        }
        public void Add(double inY, double inZ, double inRadii, double inLength, double inRotation, bool inIsBorder, bool inIsVisible, bool overwriteLastIndex) {
            lPositions[lPositions.Count - 1] = new double[2] { inY, inZ };
            lRotations[lPositions.Count - 1] = inRotation;
            lRadii[lPositions.Count - 1] = inRadii;
            lIsBorder[lPositions.Count - 1] = inIsBorder;
            lIsVisible[lPositions.Count - 1] = inIsVisible;
            lLength[lPositions.Count - 1] = inLength;
        }

        public void DrawVelocity(Graphics graphic, int nTimeStep)
        {
            if (nTimeStep > 0) {
                if (!lIsBorder[nTimeStep]) { //For now, don't draw borders because they can switch with proj.  To change add projections to the fiber
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    double[] Vel = new double[2];
                    double mag = 0;
                    findVel(nTimeStep, ref Vel, ref mag);
                    float pensize = (float)penThick;//(float)(penThick * mag / maxVelocity);
                                                    //Scale the length so that the average length is the radius
                    double[] VelNorm = VectorMath.ScalarMultiply(lRadii[0] / (sumVelocity / nVelocity), Vel);
                    double[] pt2 = VectorMath.Add(lPositions[nTimeStep], VelNorm);
                    //scale the color based on the max velocity
                    double ratio = mag / (sumVelocity / nVelocity + 2.0 * sum2Velocity / nVelocity);
                    ratio = (ratio > 1) ? 1 : ratio * 1.0;
                    Color myCol = Contact.ColorFromHSV(240 - 240 * (ratio), 1d, 1d);

                    Pen fiberBrush = new Pen(myCol, pensize);
                    fiberBrush.EndCap = LineCap.ArrowAnchor;

                    graphic.DrawLine(fiberBrush, (float)lPositions[nTimeStep][0], (float)lPositions[nTimeStep][1], (float)pt2[0], (float)pt2[1]);
                    fiberBrush.Dispose(); //TODO figure something to draw between projected fibers, this is just for 2-D
                }
            }

        }
        public void findMaxVel() {
            double[] Vel = new double[2];
            double mag = 0;
            for (int i = 1; i < lPositions.Count; i++) {
                if (!lIsBorder[i]) {
                    findVel(i, ref Vel, ref mag);
                    if (Math.Abs(mag) > maxVelocity) {
                        maxVelocity = Math.Abs(mag);
                    }
                    sumVelocity += Math.Abs(mag);
                    sumVelocity += mag * mag;
                    nVelocity++;
                }
            }
        }
        private void findVel(int i, ref double[] V, ref double v) {
            V = VectorMath.Subtract(lPositions[i], lPositions[i - 1]);
            v = VectorMath.Norm(V);
        }
        public void DrawVelFiber(Graphics graphic, int nTimeStep)
        {
            if (nTimeStep > 0) {
                if (!lIsBorder[nTimeStep]) { //For now, don't draw borders because they can switch with proj.  To change add projections to the fiber

                    double[] Vel = new double[2];
                    double mag = 0;
                    findVel(nTimeStep, ref Vel, ref mag);
                    //scale the color based on the max velocity
                    double ratio = mag / (sumVelocity / nVelocity + 2.0 * sum2Velocity / nVelocity);
                    ratio = (ratio > 1) ? 1 : ratio * 1.0;
                    Color myCol = Contact.ColorFromHSV(240 - 240 * (ratio), 1d, 1d);
                    Draw(graphic, nTimeStep, myCol);

                }
            }

        }

        public void DrawOrientation(Graphics graphic, int nTimeStep)
        {
            int i = nTimeStep;
            if (lIsVisible[i])
            {
                //put a little line to see whether it's rotated
                Pen mypen = new Pen(Color.White, (float)(lRadii [i] / 100));
				
				double [] rotUp = new double[2]{lRadii [i] / 2d * Math.Cos(lRotations[i]), lRadii [i] / 2d * Math.Sin(lRotations[i])};;
				
				graphic.DrawLine(mypen, (float)lPositions[i][0], (float)lPositions[i][1], (float)(lPositions[i][0]+ rotUp[0]), (float)(lPositions[i][1]+ rotUp[1]));
				mypen.Dispose();
            }

        }

        public void DrawFiberNumbers(Graphics graphic, int nTimeStep, Color color)
        {
            int i = nTimeStep;
            if (lIsVisible[i])
            {
                //Now, transform the axis from local instance to window axis
                Matrix myTransform = new Matrix(1f, 0f, 0f, -1f, 0f, 0f); //reflection

                //Now, make a container to apply the transfrom to the container only
                GraphicsContainer transformContainer = graphic.BeginContainer();
                graphic.Transform = myTransform;

                SolidBrush brush = new SolidBrush(color);
                Font font = new Font(new FontFamily("Arial"), (int)(lRadii[i] / 2.0));
                graphic.DrawString(fiberIndex.ToString(), font, brush, (float)(lPositions[i][0] - lRadii[i] / 4.0), -(float)(lPositions[i][1] + lRadii[i] / 4.0));
                brush.Dispose();

                //close the container because it's awesome.)
                graphic.EndContainer(transformContainer);
            }

        }

        public void Draw(Graphics graphic, int i, Color objectColor) {

            if (lIsVisible[i]) {
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.SmoothingMode = SmoothingMode.AntiAlias;

                SolidBrush fiberBrush = new SolidBrush(objectColor);
                graphic.FillEllipse(fiberBrush, (float)(lPositions[i][0] - lRadii[i]), (float)(lPositions[i][1] - lRadii[i]), (float)(2.0 * lRadii[i]), (float)(2.0 * lRadii[i]));
                fiberBrush.Dispose();

                //put a little line to see whether it's rotated
                /*Pen mypen = new Pen(Color.White, (float)(lRadii [i] / 100));
				
				double [] rotUp = new double[2]{lRadii [i] / 2d * Math.Cos(lRotations[i]), lRadii [i] / 2d * Math.Sin(lRotations[i])};;
				
				graphic.DrawLine(mypen, (float)lPositions[i][0], (float)lPositions[i][1], (float)(lPositions[i][0]+ rotUp[0]), (float)(lPositions[i][1]+ rotUp[1]));
				mypen.Dispose();*/
                /**/
            }
        }

    }

    public class Wall {

        public List<double[]> lCenters;
        public List<double[]> lNorms;

        public Wall() {
            lCenters = new List<double[]>();
            lNorms = new List<double[]>();
        }

        public void Add(double[] inCenter, double[] inNorm) {
            lCenters.Add(inCenter);
            lNorms.Add(inNorm);
        }

    }

    public class Boundary {

        private List<Wall> lWalls;
        private int nWalls = 4;


        public Boundary() {
            lWalls = new List<Wall>();
            for (int i = 0; i < nWalls; i++) {
                lWalls.Add(new Wall());
            }
        }

        public void Add(int nWall, double[] inCenter, double[] inNorm) {

            lWalls[nWall].Add(inCenter, inNorm);
        }

        public PointF[] FindCorners(int newIndex) {

            //ADD Corner Finding
            PointF c1 = FindCorner(lWalls[0].lCenters[newIndex], lWalls[2].lCenters[newIndex], lWalls[0].lNorms[newIndex], lWalls[2].lNorms[newIndex]);
            PointF c2 = FindCorner(lWalls[0].lCenters[newIndex], lWalls[3].lCenters[newIndex], lWalls[0].lNorms[newIndex], lWalls[3].lNorms[newIndex]);
            PointF c3 = FindCorner(lWalls[1].lCenters[newIndex], lWalls[3].lCenters[newIndex], lWalls[1].lNorms[newIndex], lWalls[3].lNorms[newIndex]);
            PointF c4 = FindCorner(lWalls[1].lCenters[newIndex], lWalls[2].lCenters[newIndex], lWalls[1].lNorms[newIndex], lWalls[2].lNorms[newIndex]);
            return new PointF[4] { c1, c2, c3, c4 };
        }

        public float[] FindExtremeCorners() {

            //ADD Corner Finding
            float top = 0f; float bottom = 0f; float left = 0f; float right = 0f;

            for (int i = 0; i < lWalls[0].lCenters.Count; i++) {
                PointF[] tempCorners = FindCorners(i);
                foreach (PointF pf in tempCorners) {
                    if (pf.X > right) { right = pf.X; }
                    else if (pf.X < left) { left = pf.X; }

                    if (pf.Y > top) { top = pf.Y; }
                    else if (pf.Y < bottom) { bottom = pf.Y; }
                }
            }

            return new float[4] { left, bottom, right, top };
        }

        private PointF FindCorner(double[] c1, double[] c2, double[] n1, double[] n2) {

            double[] b = new double[2] { VectorMath.Dot(n1, c1), VectorMath.Dot(n2, c2) };
            double[,] A = MatrixMath.StackVertical(n1, n2);
            double[] x = MatrixMath.LinSolve(A, b);

            return new PointF((float)x[0], (float)x[1]);
        }

        public void Draw(Graphics graphic, int nTimeStep, Color objectColor)
        {
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.SmoothingMode = SmoothingMode.AntiAlias;

            Pen cellWallBrush = new Pen(objectColor, 0.3f);
            cellWallBrush.DashStyle = DashStyle.Dot;
            cellWallBrush.DashOffset = 40;

            PointF[] points = FindCorners(nTimeStep);

            graphic.DrawPolygon(cellWallBrush, points);
            cellWallBrush.Dispose();
        }
    }

    public class Contact
    {

        private static double penThick = 2;

        private List<double[]> lP1;
        private List<double[]> lP2;
        private List<double> lForce;
        private List<int> lIndexes;

        public bool isBroken = false;

        public Contact()
        {
            lP1 = new List<double[]>();
            lP2 = new List<double[]>();
            lForce = new List<double>();
            lIndexes = new List<int>();
        }

        public void Add(double p1Y, double p1Z, double p2Y, double p2Z, double Force, int index, ref double maxForce)
        {
            lP1.Add(new double[2] { p1Y, p1Z });
            lP2.Add(new double[2] { p2Y, p2Z });
            lForce.Add(Force);
            lIndexes.Add(index);

            if (Math.Abs(Force) > maxForce)
            {
                maxForce = Math.Abs(Force);
            }
        }

        public void Draw(Graphics graphic, int nTimeStep, double maxForce)
        {
            if (lIndexes.Contains(nTimeStep))
            {

                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
               graphic.SmoothingMode = SmoothingMode.AntiAlias;

                int index = lIndexes.IndexOf(nTimeStep);

                float pensize = (float)(penThick);
                //float pensize = (float)penThick;
                //Color myCol = ColorFromHSV(240 - 240 * (lForce[index] / maxForce), 1d, 1d);
                Color myCol = Color.Black;
                if (!isBroken)
                {
                    myCol = ColorFromArgb_RYG(Math.Abs(lForce[index]) / maxForce);
                    pensize = (float)(penThick * (Math.Abs(lForce[index]) / maxForce));
                }

                //Color myCol = Color.PaleGoldenrod;
                Pen fiberBrush = new Pen(myCol, pensize);

                graphic.DrawLine(fiberBrush, (float)lP1[index][0], (float)lP1[index][1], (float)lP2[index][0], (float)lP2[index][1]);
                fiberBrush.Dispose(); //TODO figure something to draw between projected fibers, this is just for 2-D
            }
        }
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            byte v = Convert.ToByte(value);
            byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be from 0 to 1</param>
        /// <returns></returns>
        public static Color ColorFromArgb_RYG(double value)
        {
            List<double[]> colors = new List<double[]>();
            //Colors go from low to high
            colors.Add(new double[3] { 65.0, 105.0, 225.0 }); //Royal Blue
            colors.Add(new double[3] { 65.0, 105.0, 225.0 }); //Royal Blue
            colors.Add(new double[3] { 64.0, 224.0, 208.0 }); //Turqoise
                                                              //colors.Add(new double[3]{175.0, 255.0, 47.0}); //lawn Green
            colors.Add(new double[3] { 175.0, 255.0, 47.0 }); //lawn Green
            colors.Add(new double[3] { 255.0, 230.0, 0.0 }); //Yellow
            colors.Add(new double[3] { 255.0, 0.0, 0.0 }); //Red //Add twice because I goofed up, and there are no red otherwise
            colors.Add(new double[3] { 255.0, 0.0, 0.0 }); //Red
                                                           //colors.Add(new double[3]{255.0, 0.0, 0.0}); //Red //Add twice because I goofed up, and there are no red otherwise

            //Check for too high or too low
            if (value >= 1.0)
            {
                return Color.FromArgb(255, (int)colors[colors.Count - 1][0], (int)colors[colors.Count - 1][1], (int)colors[colors.Count - 1][2]);
            }
            if (value < -1.0)
            {
                return Color.FromArgb(255, (int)colors[0][0], (int)colors[0][1], (int)colors[0][2]);
            }
            //Convert value to 0 to 1 scale (if -1 to 1) or tensile/compression
            //double newVal =  (value + 1)*0.5;
            double newVal = value * 1.0;

            //Find index below
            double n = colors.Count - 1.0;
            int indexBelow = (int)Math.Floor(newVal * n);
            int indexAbove = indexBelow + 1;

            if (indexBelow < 0 || indexBelow > 3)
            {
                int j = 4; //debug
            }

            //interpolate rgb
            int r = (int)(LinearInterpolation(newVal, indexBelow / n, indexAbove / n, colors[indexBelow][0], colors[indexAbove][0]));
            int g = (int)(LinearInterpolation(newVal, indexBelow / n, indexAbove / n, colors[indexBelow][1], colors[indexAbove][1]));
            int b = (int)(LinearInterpolation(newVal, indexBelow / n, indexAbove / n, colors[indexBelow][2], colors[indexAbove][2]));

            return Color.FromArgb(255, r, g, b);
        }

        public static double LinearInterpolation(double x, double xBelow, double xAbove, double yBelow, double yAbove)
        {
            double y = ((yAbove - yBelow) / (xAbove - xBelow) * (x - xBelow) + yBelow);
            if (y <= 0 || y > 255)
            {
                int i = 4;//debug
            }
            return y;
        }

    }
   
    public class StressStrain
    {

        public string[] Labels = new string[13] { "Epsilon xx", "Epsilon yy", "Epsilon zz", "Epsilon xy", "Epsilon xz", "Epsilon yz", "Sigma xx", "Sigma yy", "Sigma zz", "Sigma xy", "Sigma xz", "Sigma yz", "" };
        private int nComponents = 12;
        public List<PointPairList[]> lPointList = new List<PointPairList[]>();
        private PointPairList[] tempPointList;
        private double min = 0;
        private double max = 0;

        public StressStrain()
        {
            tempPointList = new PointPairList[nComponents];
            for (int i = 0; i < nComponents; i++)
            {
                tempPointList[i] = new PointPairList();
            }
        }

        public void Add(double[] d)
        {
            //This was to have the plot move up gradually
            /*PointPairList [] tempList = new PointPairList[nComponents];
			
			for (int i = 0; i < nComponents; i++) {
				
				PointPairList temp = new PointPairList();
				int it = 0;
				
				if (lPointList.Count != 0) {
					it = lPointList.Count;
					temp = lPointList[lPointList.Count-1][i].Clone();
				}
				temp.Add(new PointPair(it, d[i]));
				tempList[i] = temp;
			}
			lPointList.Add(tempList);
			 */
            int iteration = tempPointList[0].Count;
            for (int i = 0; i < nComponents; i++)
            {

                tempPointList[i].Add(new PointPair(iteration, d[i]));
                //Get min and max for use later as a scrolling placemarker
                if (d[i] < min) { min = d[i]; }
                else if (d[i] > max) { max = d[i]; }
            }

        }
        public void Done()
        {

            for (int i = 0; i < tempPointList[0].Count; i++)
            {

                PointPairList[] temp = new PointPairList[nComponents+1];

                tempPointList.CopyTo(temp, 0);

                //Add a line for the placeholder: max and min for the vertical line
                temp[nComponents] = new PointPairList();
                temp[nComponents].Add(new PointPair(i, min));
                temp[nComponents].Add(new PointPair(i, max));

                lPointList.Add(temp);
            }
        }
    }

}
