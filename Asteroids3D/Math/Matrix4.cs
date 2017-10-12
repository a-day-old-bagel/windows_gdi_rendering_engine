/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    class Matrix4
    {
        #region Fields

        double[,] Mat;

        #endregion

        #region Constructors

        public Matrix4()
        {
            Mat = new double[4, 4];
            Mat[0, 0] = 1; Mat[0, 1] = 0; Mat[0, 2] = 0; Mat[0, 3] = 0;
            Mat[1, 0] = 0; Mat[1, 1] = 1; Mat[1, 2] = 0; Mat[1, 3] = 0;
            Mat[2, 0] = 0; Mat[2, 1] = 0; Mat[2, 2] = 1; Mat[2, 3] = 0;
            Mat[3, 0] = 0; Mat[3, 1] = 0; Mat[3, 2] = 0; Mat[3, 3] = 1;
        }

        public Matrix4(double aa, double ab, double ac, double ad,
                       double ba, double bb, double bc, double bd,
                       double ca, double cb, double cc, double cd,
                       double da, double db, double dc, double dd)
        {
            Mat = new double[4, 4];
            Mat[0, 0] = aa; Mat[0, 1] = ab; Mat[0, 2] = ac; Mat[0, 3] = ad;
            Mat[1, 0] = ba; Mat[1, 1] = bb; Mat[1, 2] = bc; Mat[1, 3] = bd;
            Mat[2, 0] = ca; Mat[2, 1] = cb; Mat[2, 2] = cc; Mat[2, 3] = cd;
            Mat[3, 0] = da; Mat[3, 1] = db; Mat[3, 2] = dc; Mat[3, 3] = dd;
        }

        #endregion

        #region Static Matrix Methods

        public static Matrix4 ZeroMatrix()
        {
            Matrix4 matrix = new Matrix4();
            for (int i = 0; i < 4; i++)
                matrix[i, i] = 0;
            return matrix;
        }

        public static Matrix4 IdentityMatrix()
        {
            return new Matrix4();
        }

        public static Matrix4 Multiply(Matrix4 lhs, Matrix4 rhs)
        {
            Matrix4 matrix = ZeroMatrix();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        matrix[i, j] += lhs[i, k] * rhs[k, j];
            return matrix;
        }

        public static Vector4D Multiply(Matrix4 lhs, Vector4D rhs)
        {
            Vector4D vector = new Vector4D();
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < 4; k++)
                    vector[i] += lhs[i, k] * rhs[k];
            return vector;
        }

        #endregion

        #region Overloads

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return Multiply(lhs, rhs);
        }

        public static Vector4D operator *(Matrix4 lhs, Vector4D rhs)
        {
            return Multiply(lhs, rhs);
        }

        public double this[int row, int col]
        {
            get { return Mat[row, col]; }
            set { Mat[row, col] = value; }
        }

        #endregion
    }
}
