using System;
using myMath;

namespace PlotFDEM.MatrixContinuum
{

    public class MatrixContinuumElasticFiberModel : iMatrixModel
    {

        public int nf1;
        public int nf2;
        protected double E;
        protected double nu;
        protected double d;
        protected int j;
        protected double r;
        protected double b;
        protected double[,] D;


        public MatrixContinuumElasticFiberModel(Fiber f1, Fiber f2, double E, double nu, double d0)
        {
            //Input Parameters
            this.E = E;
            this.nu = nu;
            this.d = d0;
            this.nf1 = f1.fiberIndex;
            this.nf2 = f2.fiberIndex;
            this.b = f1.lLength[0];
            this.r = f1.lRadii[0];

            double Ep = E / ((1 - 2 * nu) * (1 + nu));
            D = new double[6, 6] {
                {Ep * (1 - nu), Ep * nu, Ep * nu, 0, 0, 0},
                {Ep * nu, Ep * (1 - nu), Ep * nu, 0, 0, 0},
                {Ep * nu, Ep * nu, Ep * (1 - nu), 0, 0, 0},
                {0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0, 0, 0},
                {0, 0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0, 0},
                {0, 0, 0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0}
            };
        }

        #region Public Methods
        public double[] CalculateDisplacement(double x, double y, double z, double[] q, int iteration)
        {
            double rmz = r * r - z * z;
            double srmz = Math.Sqrt(rmz);

            double[,] Nxyz = new double[3, 9]{{-((-d + srmz + y) / (d - 2 * srmz)), 0, 0, 0, (-srmz + y) / (d - 2 * srmz), 0, 0, 0, x / b },
                {0, -((-d + srmz + y) / (d - 2 * srmz)), 0, ((-d + srmz + y) * z) / (d - 2 * srmz), 0, (-srmz + y) / (d - 2 * srmz), 0, ((srmz - y) * z) / (d - 2 * srmz), 0 },
                {0, 0, -((-d + srmz + y) / (d - 2 * srmz)), -((rmz - d * srmz + srmz * y) / (d - 2 * srmz)), 0, 0, (-srmz + y) / (d - 2 * srmz), (rmz - srmz * y) / (d - 2 * srmz), 0 }};

            return MatrixMath.Multiply(Nxyz, q);
        }

        public double[] CalculateStrain(double x, double y, double z, double[] q, int iteration)
        {
            double z2 = z * z;
            double r2 = r * r;
            double d2 = d * d;

            double srmz = Math.Sqrt(r2 - z2);
            double c1 = d - 2 * srmz;
            double c12 = c1 * c1;

            double[,] Bxyz = new double[6, 9]{{ 0, 0, 0, 0, 0, 0, 0, 0, 1 / b},
                { 0, 1 / (-d + 2 * srmz), 0, z / c1, 0, 1 / c1, 0, z / (-d + 2 * srmz), 0},
                { 0, 0, -(((d - 2 * y) * z) / (c12 * srmz)), (z * (-d2 - 2 * r2 + d * (2 * srmz + y) + 2 * z2)) / (c12 * srmz),
                    0, 0, ((d - 2 * y) * z) / (c12 * srmz), (z * (2 * r2 + d * (-2 * srmz + y) - 2 * z2)) / (c12 * srmz), 0},
                { 0, -(((d - 2 * y) * z) / (c12 * srmz)), 1 / (-d + 2 * srmz), (-(d2 * srmz) - 2 * r2 * y + d * (2 * r2 + srmz * y - z2)) / (c12 * srmz),
                    0, ((d - 2 * y) * z) / (c12 * srmz), 1 / c1, (2 * r2 * y - d * (srmz * y + z2)) / (c12 * srmz), 0},
                { -(((d - 2 * y) * z) / (c12 * srmz)), 0, 0, 0, ((d - 2 * y) * z) / (c12 * srmz), 0, 0, 0, 0},
                { 1 / (-d + 2 * srmz), 0, 0, 0, 1 / c1, 0, 0, 0, 0}
            };

            foreach (double d in Bxyz)
            {
                if (Double.IsNaN(d))
                {
                    bool flag = true;
                }
            }

            return MatrixMath.Multiply(Bxyz, q);
        }

        public virtual double[] CalculateStress(double x, double y, double z, double[] q, int iteration)
        {
            double[] strain = this.CalculateStrain(x, y, z, q, iteration);

            return MatrixMath.Multiply(D, strain);
        }
        public virtual double CalculateDamage(double x, double y, double z, double[] q, int iteration)
        {
            return 0;
        }

        #endregion

        #region Private Methods 

        #endregion
    }

}
