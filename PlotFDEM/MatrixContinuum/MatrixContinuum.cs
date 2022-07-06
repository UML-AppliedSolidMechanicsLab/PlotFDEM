using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing; 
using myMath;
using PlotFDEM.MatrixContinuum.ContourPlot;

namespace PlotFDEM.MatrixContinuum
{

    public class MatrixContinuum
    {
        public int nf1;
        public int nf2;
        private Fiber f1;
        private Fiber f2;
        private int j;
        private double r;
        public double b;

        private iMatrixModel matrixModel;

        public List<int> lIteration; //This is a list of the iteration matching with the q, z, and d12
        public List<double[]> lq; //This is the list of local displacement vectors.  
        private List<double[]> lglobalQ; //This is the list of local displacement vectors.  
        private List<double[,]> lglobalQ_rotationTransform; //This is the transform that rotates the displacements back to the global coordinates. 
        private List<double[,]> lglobalS_rotationTransform; //This is the transform that rotates the stress/strain back to the global coordinates.  
        public List<double[]> lz;//This is the list of z locaations
        public List<double> ld12; //This is the list of 
        

        //These are for the transform from local to global
        private List<Matrix> lLocalToGlobalTransform;

        //These are the transformed points
        private List<PlotPoint[,]> topPoints;
        private List<PlotPoint[,]> bottomPoints;
        private List<GridSquare[,]> topGrids;
        private List<GridSquare[,]> bottomGrids;
        private List<PointF[]> lCrack; //first two are the top crack, 2nd 2 are middle, last 2 are the bottom  or it is just one....


        public MatrixContinuum(Fiber f1, Fiber f2, iMatrixModel inMatrixModel)
        {
            //Input Parameters
            this.f1 = f1;
            this.f2 = f2;
            this.nf1 = f1.fiberIndex;
            this.nf2 = f2.fiberIndex;
            this.b = f1.lLength[0];
            matrixModel = inMatrixModel;

            //Saving Results
            lIteration = new List<int>();
            lq = new List<double[]>();
            lglobalQ = new List<double[]>();
            lz = new List<double[]>();
            ld12 = new List<double>();
            lLocalToGlobalTransform = new List<Matrix>();
            lglobalQ_rotationTransform = new List<double[,]>();
            lglobalS_rotationTransform = new List<double[,]>();

            //Points To Plot
            topPoints = new List<PlotPoint[,]>();
            bottomPoints = new List<PlotPoint[,]>();
            topGrids = new List<GridSquare[,]>();
            bottomGrids = new List<GridSquare[,]>();
            lCrack = new List<PointF[]>();
        }

        #region Public Methods
        /// <summary>
        /// Add a matrix point
        /// </summary>
        /// <param name="q">[u2, v2, w2, ug, sin(theta1), 1-cos(theta1), sin(theta2), 1-cos(theta2)]</param>
        /// <param name="zIntegrationLimitsTopToBottom">[ztop1, ztop2, zbot1, zbot2]</param>
        /// <param name="index"></param>
        public virtual void Add(double[] q, double[] zIntegrationLimitsTopToBottom, int index)

        {
            //Create local to globalTransform
            double[] xf1 = f1.lPositions[index];
            double[] x12 = VectorMath.Subtract(f2.lPositions[index], xf1);
            double rotation = Math.Atan2(x12[1], x12[0]);
            float rotDeg = (float)(rotation * 180 / Math.PI);

            //Don't think I need this anymore because I used the ATan2 function...
            //if it's in the 2nd or 3rd quadrant, then add 180
            //rotDeg = (x12[0] < 0 ) ? 180.0f + rotDeg : rotDeg;


            Matrix transform = new Matrix();
            transform.Translate((float)(xf1[0]), (float)(xf1[1]));
            transform.Rotate(rotDeg);
            lLocalToGlobalTransform.Add(transform);

            double c = Math.Cos(rotation);
            double s = Math.Sin(rotation);
            double s2 = s * s;
            double c2 = c * c;
            double sc = s * c;

            //Add just a rotation for the stress/strain
            double[,] tensRotTransform = new double[,] {{1.0, 0.0, 0.0, 0.0, 0.0, 0.0} ,
                {0.0, c2, s2, -2.0 * sc, 0.0, 0.0 },
                 {0.0, s2, c2, 2.0 * sc, 0.0, 0.0 },
                 {0.0, sc, -sc, c2-s2, 0.0, 0.0 },
                { 0.0, 0.0, 0.0, 0.0, c, s },
            { 0.0, 0.0, 0.0, 0.0, s, c }};
            lglobalS_rotationTransform.Add(tensRotTransform);

            //Add just a rotation for the displacement
            double[,] rotTransform = new double[,] {{1.0, 0.0, 0.0} ,
                {0.0, c, -1 * s  }, 
                {0.0, s, c }};
            lglobalQ_rotationTransform.Add(rotTransform);

            //Now add the displacement of fiber 1 since displacements are relative to fiber 1
            double[] xf1Origin = f1.lPositions[0];
            double[] diff = VectorMath.Subtract(xf1, xf1Origin);
            lglobalQ.Add(new double[] { 0, diff[0], diff[1] });

            //Save Results
            double di = VectorMath.Norm(x12);
            ld12.Add(di);
            lq.Add(q);
            lz.Add(zIntegrationLimitsTopToBottom);
            lIteration.Add(index);

            //Size Points to Plot as things are added
            topPoints.Add(new PlotPoint[1, 1]);
            bottomPoints.Add(new PlotPoint[1, 1]);
            topGrids.Add(new GridSquare[1, 1]);
            bottomGrids.Add(new GridSquare[1, 1]);

            //Find the crack ends....
            PointF[] tempCrackEnds = new PointF[6] {new PointF((float)(di/2.0), (float)lz[0][0]), new PointF((float)(di/2.0), (float)zIntegrationLimitsTopToBottom[0]),
            new PointF((float)(di/2.0), (float)zIntegrationLimitsTopToBottom[1]), new PointF((float)(di/2.0), (float)zIntegrationLimitsTopToBottom[2]),
            new PointF((float)(di/2.0), (float)zIntegrationLimitsTopToBottom[3]), new PointF((float)(di/2.0), (float)lz[0][3])};
            //Transform the crack ends
            transform.TransformPoints(tempCrackEnds);
            lCrack.Add(tempCrackEnds);
        }

        /// <summary>
        /// Add a matrix point
        /// </summary>
        /// <param name="q">[u2, v2, w2, ug, sin(theta1), 1-cos(theta1), sin(theta2), 1-cos(theta2)]</param>
        /// <param name="damage">This is the damage at the integration points.  Only for a damage model</param>
        /// <param name="index"></param>
        public virtual void Add(double[] q, double[] damage, int index, bool hasDamage)
        {
            try
            {
                MatrixContinuumElasticFiberDamageModel mat = (MatrixContinuumElasticFiberDamageModel)matrixModel;
                mat.AddDamage(damage);
                this.Add(q, mat.zBounds, index);
            }
            catch (Exception ex)
            {

                throw new Exception("Material must be MatrixContinuumElasticFiberDamage" + ex.Message);
            }
            
        }
        public void UpdatePointsOnly(int nGridPts)
        {
            for (int currIndex = 0; currIndex < lIteration.Count; currIndex++)
            {
                r = f1.lRadii[currIndex];

                //Populate Z and y
                double[] zTop = new double[nGridPts + 1];
                double[] zBottom = new double[nGridPts + 1];
                double[][] yTop = new double[nGridPts + 1][];
                double[][] yBottom = new double[nGridPts + 1][];
                for (int i = 0; i < nGridPts + 1; i++)
                {
                    //Populate z
                    zTop[i] = lz[currIndex][0] + (lz[currIndex][1] - lz[currIndex][0]) / nGridPts * i;
                    zBottom[i] = lz[currIndex][2] + (lz[currIndex][3] - lz[currIndex][2]) / nGridPts * i;
                    //Populate y
                    yTop[i] = CreateEquallySpacedArrayOfY(zTop[i], nGridPts);
                    yBottom[i] = CreateEquallySpacedArrayOfY(zBottom[i], nGridPts);
                }

                //Now save them into a plot point with empty results (empty elevation)
                topPoints[currIndex] = new PlotPoint[nGridPts + 1, nGridPts + 1];
                bottomPoints[currIndex] = new PlotPoint[nGridPts + 1, nGridPts + 1];
                for (int i = 0; i < nGridPts + 1; i++)
                {
                    for (int j = 0; j < nGridPts + 1; j++)
                    {
                        topPoints[currIndex][i, j] = new PlotPoint(new PointF((float)yTop[i][j], (float)zTop[i]), 0);
                        bottomPoints[currIndex][i, j] = new PlotPoint(new PointF((float)yBottom[i][j], (float)zBottom[i]), 0);
                    }

                }
            }
        }

        public void UpdateResultsOnly(int nPlotType, int nPlotComponent)
        {
            for (int currIndex = 0; currIndex < lIteration.Count; currIndex++)
            {
                int nGridPts = topPoints[currIndex].GetLength(0) - 1;

                for (int i = 0; i < nGridPts + 1 ; i++)
                {
                    for (int j = 0; j < nGridPts + 1; j++)
                    {
                        double topResult = DecideOnOutput(nPlotType, nPlotComponent, b, (double)topPoints[currIndex][i, j].point.X,
                            (double)topPoints[currIndex][i, j].point.Y, lq[currIndex], currIndex);
                        double bottomResult = DecideOnOutput(nPlotType, nPlotComponent, b, (double)bottomPoints[currIndex][i, j].point.X,
                            (double)bottomPoints[currIndex][i, j].point.Y, lq[currIndex], currIndex);

                        topPoints[currIndex][i, j].z = topResult;
                        bottomPoints[currIndex][i, j].z = bottomResult;
                    }

                }
            }
        }

        public void UpdateContoursOnly(double[] levels)
        {
            for (int currIndex = 0; currIndex < lIteration.Count; currIndex++)
            {
                //Initiate the grids
                int n = topPoints[currIndex].GetLength(0);
                topGrids[currIndex] = new GridSquare[n - 1, n - 1];
                bottomGrids[currIndex] = new GridSquare[n - 1, n - 1];

                //Now create the grids
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        topGrids[currIndex][i, j] = new GridSquare(topPoints[currIndex][i, j], topPoints[currIndex][i, j + 1], topPoints[currIndex][i + 1, j + 1], topPoints[currIndex][i + 1, j], levels);
                        bottomGrids[currIndex][i, j] = new GridSquare(bottomPoints[currIndex][i, j], bottomPoints[currIndex][i, j + 1], bottomPoints[currIndex][i + 1, j + 1], bottomPoints[currIndex][i + 1, j], levels);
                    }
                }
            }
        }

        public void Draw(int nTimeStep, Graphics panelGraphic, Color[] levelColors)
        {
            if (lIteration.Contains(nTimeStep))
            {

                j = lIteration.IndexOf(nTimeStep);

                foreach (GridSquare gs in topGrids[j])
                {
                    gs.ColorGrid(panelGraphic, levelColors, lLocalToGlobalTransform[j]);
                }
                foreach (GridSquare gs in bottomGrids[j])
                {
                    gs.ColorGrid(panelGraphic, levelColors, lLocalToGlobalTransform[j]);
                }
            }
        }

        public void DrawConnections(int nTimeStep, Graphics panelGraphic)
        {
            
            if (lIteration.Contains(nTimeStep))
            {
                float pensize = 1;
                Color myCol = Color.Black;
                Pen fiberBrush = new Pen(myCol, pensize);
                panelGraphic.DrawLine(fiberBrush, (float)f1.lPositions[nTimeStep][0], (float)f1.lPositions[nTimeStep][1], 
                    (float)f2.lPositions[nTimeStep][0], (float)f2.lPositions[nTimeStep][1]);
                fiberBrush.Dispose(); //TODO figure something to draw between projected fibers, this is just for 2-D
            }
        }

        public void DrawCrack(int nTimeStep, Graphics panelGraphic)
        {
            float pensize = 1;
            Color myCol = Color.Red;
            Pen fiberBrush = new Pen(myCol, pensize);
            PointF[] CrackPoints = new PointF[0];

            //IF crack is on the matrix
            if (lIteration.Contains(nTimeStep))
            {
                int index = lIteration.IndexOf(nTimeStep);
                CrackPoints = lCrack[index];
            }
            else if(nTimeStep != 0)
            {
                //Create local to globalTransform
                double[] xf1 = f1.lPositions[nTimeStep];
                double[] x12 = VectorMath.Subtract(f2.lPositions[nTimeStep], xf1);
                double di = VectorMath.Norm(x12);
                double rotation = Math.Atan2(x12[1], x12[0]);
                float rotDeg = (float)(rotation * 180 / Math.PI);

                Matrix transform = new Matrix();
                transform.Translate((float)(xf1[0]), (float)(xf1[1]));
                transform.Rotate(rotDeg);

                CrackPoints = new PointF[2] {new PointF((float)(di/2.0), (float)lz[0][0]), new PointF((float)(di/2.0), (float)lz[0][3])};
                //Transform the crack ends
                transform.TransformPoints(CrackPoints);
            }
            for (int i = 0; i < CrackPoints.Length / 2; i++)
            {
                panelGraphic.DrawLine(fiberBrush, CrackPoints[2 * i], CrackPoints[2 * i + 1]);
            }
           fiberBrush.Dispose(); //TODO figure something to draw between projected fibers, this is just for 2-D
        }
       
        public void DrawIsoLines(int nTimeStep, Graphics panelGraphic, Pen pen)
        {
            if (lIteration.Contains(nTimeStep))
            {

                j = lIteration.IndexOf(nTimeStep);

                foreach (GridSquare gs in topGrids[j])
                {
                    gs.PaintIsoLines(panelGraphic, lLocalToGlobalTransform[j], pen);
                }
                foreach (GridSquare gs in bottomGrids[j])
                {
                    gs.PaintIsoLines(panelGraphic, lLocalToGlobalTransform[j], pen);
                }
            }
        }

        public void FindMaxAndMinResults(ref double max, ref double min)
        {
            max = topPoints[0][0,0].z;
            min = max;

            for (int i = 0; i < topPoints.Count; i++)
            {
                FindMaxAndMinResults(i, ref max, ref min);
            }
        }
        public void FindMaxAndMinResults(int index, ref double max, ref double min)
        {
            foreach (PlotPoint plotPoint in topPoints[index])
            {
                if (plotPoint.z > max)
                {
                    max = plotPoint.z;
                }
                if (plotPoint.z < min)
                {
                    min = plotPoint.z;
                }
            }
        }
        #endregion

        #region Private Methods needed to draw

        public double DecideOnOutput(int nPlotType, int nPlotComponent, double x, double y, double z, double [] q, int iteration)
        {
            switch (nPlotType)
            {
                case 0: //local stress
                    double[] tempStress = matrixModel.CalculateStress(x, y, z, q, iteration);
                    switch (nPlotComponent)
                    {
                        case 0:
                            return VonMises(tempStress);
                        case 1:
                            return MaxPrinciple(tempStress);
                        default:
                            return tempStress[nPlotComponent - 2]; 
                    }
                    
                case 1: //global stress
                    double[] tempGStress = matrixModel.CalculateStress(x, y, z, q, iteration);
                    tempGStress = myMath.MatrixMath.Multiply(lglobalS_rotationTransform[iteration], tempGStress);
                    switch (nPlotComponent)
                    {
                        case 0:
                            return VonMises(tempGStress);
                        case 1:
                            return MaxPrinciple(tempGStress);
                        default:
                            return tempGStress[nPlotComponent - 2];
                    }

                case 2: //local strain
                    double[] tempstrain = matrixModel.CalculateStrain(x, y, z, q, iteration);
                    switch (nPlotComponent)
                    {
                        case 0:
                            tempstrain = EngineeringToTensorialShear(tempstrain);
                            return VonMises(tempstrain);
                        case 1:
                            tempstrain = EngineeringToTensorialShear(tempstrain);
                            return MaxPrinciple(tempstrain);
                        default:
                            return tempstrain[nPlotComponent - 2];
                    }

                case 3: //global strain
                    double[] tempgstrain = matrixModel.CalculateStrain(x, y, z, q, iteration);
                    tempgstrain = myMath.MatrixMath.Multiply(lglobalS_rotationTransform[iteration], tempgstrain);
                    switch (nPlotComponent)
                    {
                        case 0:
                            tempgstrain = EngineeringToTensorialShear(tempgstrain);
                            return VonMises(tempgstrain);
                        case 1:
                            tempgstrain = EngineeringToTensorialShear(tempgstrain);
                            return MaxPrinciple(tempgstrain);
                        default:
                            return tempgstrain[nPlotComponent - 2];
                    }

                case 4: //local displacement
                    double[] tempU = matrixModel.CalculateDisplacement(x, y, z, q, iteration);
                    if (nPlotComponent == 0) //If 3, then return the magnitude
                    {
                        return VectorMath.Norm(tempU);
                    }
                    return tempU[nPlotComponent - 1];

                case 5: //global displacement
                    double[] tempU2 = matrixModel.CalculateDisplacement(x, y, z, q, iteration);
                    tempU2 = myMath.MatrixMath.Multiply(lglobalQ_rotationTransform[iteration], tempU2);

                    //Now add the global displcaement of the fiber
                    tempU2 = VectorMath.Add(tempU2, lglobalQ[iteration]);

                    if (nPlotComponent == 0) //If 3, then return the magnitude
                    {
                        return VectorMath.Norm(tempU2);
                    }
                    return tempU2[nPlotComponent - 1];

                case 6: //Damage
                    double D = matrixModel.CalculateDamage(x, y, z, q, iteration);
                    
                    return D;
            }
            return 0;
        }

        private double[] CreateEquallySpacedArrayOfY(double z, int nPoints)
        {
            double[] y = new double[nPoints + 1];
            double yl = CalculateYAtLeftFiber(z);
            double yr = CalculateYAtRightFiber(z);

            for (int i = 0; i < nPoints + 1; i++)
            {
                y[i] = yl + i * (yr - yl) / nPoints;
            }
            return y;
        }

        public double CalculateYAtLeftFiber(double z)
        {
            return Math.Sqrt(r * r - z * z); ;
        }

        public double CalculateYAtRightFiber(double z)
        {
            return (ld12[j] - Math.Sqrt(r * r - z * z));
        }

        private double VonMises(double [] a)
        {
            double vm = Math.Sqrt(0.5 * (Math.Pow((a[0] - a[1]),2) + Math.Pow((a[1] - a[2]), 2) 
                + Math.Pow((a[2] - a[0]), 2) + 6.0 * (a[3]*a[3] + a[4] * a[4] + a[5] * a[5])));
            return vm;
        }

        private double MaxPrinciple(double[] s)
        {
            //put strain in tensor form: Eps_xx, Eps_yy, Eps_zz, Gamma_YZ, Gamma_XZ, Gamma_XY:
            double[,] sTensor = new double[3, 3] { { s[0], s[5], s[4] }, { s[5], s[1], s[3] }, { s[4], s[3], s[2] } };

            double[] sPrinciple = MatrixMath.EigenvaluesOf3by3SymmetricMatrix(sTensor);

            return sPrinciple[0];
        }

        private double [] EngineeringToTensorialShear(double [] eps)
        {
            for (int i = 3; i < eps.Length; i++)
            {
                eps[i] = 0.5 * eps[i];
            }
            return eps;
        }
        
        #endregion
    }

    public interface iMatrixModel
    {
        double[] CalculateDisplacement(double x, double y, double z, double[] q, int iteration);
        double[] CalculateStrain(double x, double y, double z, double[] q, int iteration);
        double[] CalculateStress(double x, double y, double z, double[] q, int iteration);
        double CalculateDamage(double x, double y, double z, double[] q, int iteration);

    }
}
