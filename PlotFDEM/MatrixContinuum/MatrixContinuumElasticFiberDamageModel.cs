using System;
using myMath;
using System.Collections.Generic;

namespace PlotFDEM.MatrixContinuum
{

    public class MatrixContinuumElasticFiberDamageModel : MatrixContinuumElasticFiberModel
    {
        private List<double[]> damage;
        private double[] zPoints;
        public double[] zBounds;
        private double G;
        private bool setZValues = false;

        public MatrixContinuumElasticFiberDamageModel(Fiber f1, Fiber f2, double E, double G, double d0, double zt1, double zt2, double zb1, double zb2)
            :base(f1, f2, E, 1.0 - E/(2.0*G), d0)
        {
            zBounds = new double[4] {zt1, zt2, zb1, zb2};
            this.G = G;
            damage = new List<double[]>();

        }

        #region Public Methods

        public void AddDamage(double[] damageValues)

        {
            damage.Add(damageValues);

            //This assumes that the location of the integration points does not change, hence just doing this once
            if (!setZValues)
            {
                //Get the locations of the damage values
                int halfLength = damage[0].Length / 2;
                double[] zPtsTop = new double[halfLength];
                double[] zPtsBot = new double[halfLength];

                for (int i = 0; i < halfLength; i++)
                {
                    zPtsTop[i] = QuadraticZ(i, zBounds[1], zBounds[0], damage[0].Length / 2, true);
                    zPtsBot[i] = QuadraticZ(i, zBounds[3], zBounds[2], damage[0].Length / 2, false);
                }
                zPoints = myMath.VectorMath.Stack(zPtsBot, zPtsTop);

            }
        }
        public override double[] CalculateStress(double x, double y, double z, double[] q, int iteration)
        {
            double[] strain = this.CalculateStrain(x, y, z, q, iteration);

            double damage_i = CalculateDamage( x,  y,  z,  q,  iteration);
                       

            double E_i = E * (1.0 - damage_i);
            double G_i = E_i / (2.0 * (1.0 + nu));

            double[,] D = new double[,] { { E_i, 0, 0, 0, 0, 0 },
                {0, E_i, 0, 0, 0, 0},
                {0, 0, E_i, 0, 0, 0},
                {0, 0, 0, G_i, 0, 0},
                {0, 0, 0, 0, G_i, 0},
                {0, 0, 0, 0, 0, G_i}};

            return MatrixMath.Multiply(D, strain);
        }
        public override double CalculateDamage(double x, double y, double z, double[] q, int iteration)
        {
            //Find the z index that is between
            int i = Array.FindIndex(zPoints, k => z <= k);

            if (i == -1)
            {
                i = zPoints.Length - 1;
            }
            //i = i < 0 ? 0 : i; //I think that this is needed for fringe values
            double damage_i;

            //This takes care of points at the beginning, where the -1 returns an error.
            if (i == 0)
            {
                damage_i = damage[iteration][i];
            }
            else
            {
                damage_i = damage[iteration][i - 1] + (z - zPoints[i - 1]) * (damage[iteration][i] - damage[iteration][i - 1]) / (zPoints[i] - zPoints[i - 1]);
            }

            return damage_i;
        }

        #endregion

        #region Private Methods 
        ///This samples the z space with a quadratic.  It is only public so that it can be easily tested.... This is from QuadraticSampling.nb
        public static double QuadraticZ(int i, double zLower, double zUpper, int nIntPts, bool isTop)
        {
            //This is quadratic with the slope 0 at the upper bound
            //The higher the slope, the more biased the distribution
            double z;
            if (isTop)
            {
                z = zLower + 2.0 / nIntPts * (zUpper - zLower) * i - (zUpper - zLower) / Math.Pow(nIntPts, 2.0) * Math.Pow(i, 2.0);
            }
            else
            {
                z = zLower + (Math.Pow(i, 2.0) * (-zLower + zUpper)) / Math.Pow(nIntPts, 2.0);
            }
            return z;
        }
        #endregion
    }

}
