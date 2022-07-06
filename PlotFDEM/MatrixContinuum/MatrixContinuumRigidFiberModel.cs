using System;
using myMath;
using System.Collections.Generic;

namespace PlotFDEM.MatrixContinuum
{

    public class MatrixContinuumRigidFiberModel: iMatrixModel
    {

        public int nf1;
        public int nf2;
        private double E;
        private double nu;
        private double d;
        private int j;
        private double r;
        private double b;
        List<double[,]> matrixQ;


        public MatrixContinuumRigidFiberModel(Fiber f1, Fiber f2, double E, double nu, double d0)
        {
            //Input Parameters
            this.E = E;
            this.nu = nu;
            this.d = d0;
            this.nf1 = f1.fiberIndex;
            this.nf2 = f2.fiberIndex;
            this.b = f1.lLength[0];
            matrixQ = new List<double[,]>();
        }
        public MatrixContinuumRigidFiberModel() { } //Dummy constructor to initiate empty object

        #region Public Methods
        public double[] CalculateDisplacement(double x, double y, double z, double[] q, int iteration)
        {
            double rmz = r * r - z * z;
            double srmz = Math.Sqrt(rmz);

            double[,] Nxyz = new double[3, 8] {
                { (-srmz + y)/(d - 2*srmz),0,0,x/b,0,0,0,0 },
                {
                    0,
                    (-srmz + y)/(d - 2*srmz),
                    0,
                    0,
                    ((-d + srmz + y)*z)/(d - 2*srmz),
                    (rmz - d*srmz + srmz*y)/(d - 2*srmz),
                    ((srmz - y)*z)/(d - 2*srmz),
                    (-rmz + srmz*y)/(d - 2*srmz)
            },

                {
                    0,
                    0,
                    (-srmz + y)/(d - 2*srmz),
                    0,
                    -((rmz - d*srmz + srmz*y)/(d - 2*srmz)),
                    ((-d + srmz + y)*z)/(d - 2*srmz),
                    (rmz - srmz*y)/(d - 2*srmz),
                    ((srmz - y)*z)/(d - 2*srmz)
                }
            };
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

            double[,] Bxyz = new double[6, 8] {
                { 0,0,0,1/b,0,0,0,0 },
                {0,1/c1,0,0,z/c1,srmz/c1,-(z/c1),srmz/c1 },
                {
                    0,0,((d - 2*y)*z)/(c12*srmz),0,(z*(-d2 - 2*r2 + d*(2*srmz + y) + 2*z2))/(c12*srmz),
                    -((d2 + r2*(2 + (2*y)/srmz) - (d*(3*r2 + srmz*y - 2*z2))/srmz - 2*z2)/(d2 + 4*r2 - 4*d*srmz - 4*z2)),
                    (z*(2*r2 + d*(-2*srmz + y) - 2*z2))/(c12*srmz),
                    (-2*r2*(srmz - y) + d*(r2 - srmz*y - 2*z2) + 2*srmz*z2)/(c12*srmz)
                },
                {
                    0,((d - 2*y)*z)/(c12*srmz),
                    1/c1,
                    0,
                    (-(d2*srmz) - 2*r2*y + d*(2*r2 + srmz*y - z2))/(c12*srmz),
                    -((d*(-d + srmz + y)*z)/(c12*srmz)),
                    (2*r2*y - d*(srmz*y + z2))/(c12*srmz),
                    (d*(srmz - y)*z)/(c12*srmz)
                },
                {
                    ((d - 2*y)*z)/(c12*srmz),
                    0,0,0,0,0,0,0
                },
                {
                    1/c1,0,0,0,0,0,0,0
                }
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

        public double[] CalculateStress(double x, double y, double z, double[] q, int iteration)
        {
            double Ep = E / ((1 - 2 * nu) * (1 + nu));

            double[,] D = new double[6, 6] {
                {Ep * (1 - nu), Ep * nu, Ep * nu, 0, 0, 0},
                {Ep * nu, Ep * (1 - nu), Ep * nu, 0, 0, 0},
                {Ep * nu, Ep * nu, Ep * (1 - nu), 0, 0, 0},
                {0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0, 0, 0},
                {0, 0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0, 0},
                {0, 0, 0, 0, 0, (Ep * (1 - 2 * nu)) / 2.0}
            };
            double[] strain = this.CalculateStrain(x, y, z, q, iteration);

            return MatrixMath.Multiply(D, strain);
        }
        public double CalculateDamage(double x, double y, double z, double[] q, int iteration)
        {
            return 0;
        }
        #endregion

        #region Private Methods 

        #endregion
    }

}
