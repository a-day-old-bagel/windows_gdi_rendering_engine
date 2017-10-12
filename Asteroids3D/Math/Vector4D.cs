/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    /// <summary>
    /// Vector class has recently been migrated from using four doubles to using an array
    /// of doubles, so some artifacts from the independent double era may remain...
    /// </summary>
    class Vector4D
    {
        #region Fields

        double[] entry;

        #endregion

        #region Properties

        public double Y { get { return entry[1]; } set { entry[1] = value; } }
        public double X { get { return entry[0]; } set { entry[0] = value; } }
        public double Z { get { return entry[2]; } set { entry[2] = value; } }
        public double W { get { return entry[3]; } set { entry[3] = value; } }

        #endregion

        #region Constructors

        public Vector4D()
        {
            entry = new double[4];
            for (int i = 0; i < 4; i++)
                entry[i] = 0;
        }

        public Vector4D(double x, double y, double z, double w)
        {
            entry = new double[4];
            this.entry[0] = x;
            this.entry[1] = y;
            this.entry[2] = z;
            this.entry[3] = w;
        }

        public Vector4D(Vector4D vec)
        {
            entry = new double[4];
            entry[0] = vec.X;
            entry[1] = vec.Y;
            entry[2] = vec.Z;
            entry[3] = vec.W;
        }

        #endregion

        #region Getters

        public double getMagnitude() { return Math.Sqrt(Math.Pow(entry[0], 2) + Math.Pow(entry[1], 2) + Math.Pow(entry[2], 2)); }    //
        public double getMagnitude2() { return Math.Pow(entry[0], 2) + Math.Pow(entry[1], 2) + Math.Pow(entry[2], 2); }
        public double getMagnitudeFrom(Vector3D vec) { return Math.Sqrt(Math.Pow(vec.X - entry[0], 2) + Math.Pow(vec.Y - entry[1], 2) + Math.Pow(vec.Z - entry[2], 2)); }

        public PointF getPointXY()
        {
            return new PointF((float)entry[0], (float)entry[1]);
        }

        public Vector4D getVector()
        {
            return new Vector4D(entry[0], entry[1], entry[2], entry[3]);
        }

        #endregion

        #region Vector Manipulators

        public void setVector(Vector4D vec)
        {
            entry[0] = vec.X;
            entry[1] = vec.Y;
            entry[2] = vec.Z;
            entry[3] = vec.W;
        }

        public void addVector(Vector4D vec)
        {
            entry[0] += vec.X;
            entry[1] += vec.Y;
            entry[2] += vec.Z;
            entry[3] += vec.W;
        }

        public void subtractVector(Vector4D vec)
        {
            entry[0] -= vec.X;
            entry[1] -= vec.Y;
            entry[2] -= vec.Z;
            entry[3] -= vec.W;
        }

        public void ScalarMultiply(double scalar)
        {
            entry[0] *= scalar;
            entry[1] *= scalar;
            entry[2] *= scalar;
        }

        public void LeftMultiply(Matrix4 matrix)
        {
            setVector(Matrix4.Multiply(matrix, this));
        }

        public void divideByW()
        {
            for (int i = 0; i < 3; i++)
                entry[i] /= entry[3];
        }

        public void moveOriginToCenterScreen()
        {
            entry[0] += Utilities.screenCenter.X;
            entry[1] += Utilities.screenCenter.Y;
        }

        public void normalize()
        {
            float invSqrt = Utilities.InverseSquareRoot((float)(X * X + Y * Y + Z * Z));
            for (int i = 0; i < 3; i++)
            {
                entry[i] *= invSqrt;
            }
        }

        public void normalizeOld()
        {
            double divisor = X * X + Y * Y + Z * Z;
            for (int i = 0; i < 3; i++)
            {
                entry[i] *= entry[i];
                entry[i] /= divisor;
            }
        }

        public void equals (Vector3D equiv3D)
        {
            entry[0] = equiv3D.X;
            entry[1] = equiv3D.Y;
            entry[2] = equiv3D.Z;
            entry[3] = 1;
        }

        public void zero()
        {
            entry[0] = 0;
            entry[1] = 0;
            entry[2] = 0;
            entry[3] = 1;
        }

        #endregion

        #region Overloads

        static public Vector4D Zero()
        {
            return new Vector4D(0, 0, 0, 1);
        }

        static public Vector4D operator + (Vector4D lhs, Vector4D rhs)
        {
            return new Vector4D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W * rhs.W);                    // ???? W
        }

        static public Vector4D operator - (Vector4D lhs, Vector4D rhs)
        {
            return new Vector4D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W * rhs.W);                    // ???? W
        }

        static public double dot(Vector4D lhs, Vector4D rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public double this[int row]
        {
            get { return entry[row]; }
            set { entry[row] = value; }
        }

        static public Vector4D operator * (Vector4D vec, double scalar)
        {
            return new Vector4D(vec.X * scalar, vec.Y * scalar, vec.Z * scalar, vec.W);
        }

        public static Vector4D cross(Vector4D A, Vector4D B)
        {
            return new Vector4D
                (A.Y * B.Z - A.Z * B.Y,
                 A.Z * B.X - A.X * B.Z,
                 A.X * B.Y - A.Y * B.X, 0);
        }

        public static bool frontFacing(Vector4D A, Vector4D B, out Vector3D normal)
        {
            normal = new Vector3D
                (A.Y * B.Z - A.Z * B.Y,
                 A.Z * B.X - A.X * B.Z,
                 A.X * B.Y - A.Y * B.X);
            if (normal.Z > 0) return true;
            else return false;
        }

        public static Vector3D cross3D(Vector4D A, Vector4D B)
        {
            return new Vector3D
                (A.Y * B.Z - A.Z * B.Y,
                 A.Z * B.X - A.X * B.Z,
                 A.X * B.Y - A.Y * B.X);
        }

        #endregion
    }
}
