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
    /// <summary>
    /// Vector classes are still a mess - most of the functions that worked in 2D still need to be
    /// converted over to the 3D versions, if they're still applicable.  I'll clean this up later...
    /// sorry...
    /// </summary>
    class Vector3D
    {
        double x;
        double y;
        double z;
        public Vector3D() { x = 0; y = 0; z = 0; }                                              //
        public Vector3D(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }   //
        public Vector3D(Vector3D vec) { x = vec.X; y = vec.Y; z = vec.Z; }                      //
        public double Y { get { return y; } set { y = value; } }                                //
        public double X { get { return x; } set { x = value; } }                                //
        public double Z { get { return z; } set { z = value; } }                                //
        public double getMagnitude() { return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)); }    //
        public double getMagnitude2() { return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2); }              //
        //public double getDirection()
        //{
        //    if (x == 0)
        //        if (y < 0)
        //            return -Math.PI / 2;
        //        else if (y == 0)
        //            return 0;
        //        else
        //            return Math.PI / 2;
        //    return x < 0 ? Math.Atan(y / x) + Math.PI : Math.Atan(y / x);    
        //}
        public Vector3D getVector() { return new Vector3D(x, y, z); }                           //
        //public void setVector(double mag, double dir) { x = mag * Math.Cos(dir); y = mag * Math.Sin(dir); }
        public void setVector(Vector3D vec) { x = vec.X; y = vec.Y; z = vec.Z; }                //
        //public void addVector(double mag, double dir) { x += mag * Math.Cos(dir); y += mag * Math.Sin(dir); }
        public void addVector(Vector3D vec) { x += vec.X; y += vec.Y; z += vec.Z; }             //
        //public void subtractVector(double mag, double dir) { x -= mag * Math.Cos(dir); y -= mag * Math.Sin(dir); }
        public void subtractVector(Vector3D vec) { x -= vec.X; y -= vec.Y; z -= vec.Z; }        //
        public double getMagnitudeFrom(double x, double y, double z) { return Math.Sqrt(Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2) + Math.Pow(this.z - z, 2)); }         //
        public double getMagnitude2From(double x, double y, double z) { return Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2) + Math.Pow(this.z - z, 2); }                   //
        //public double getDirectionFrom(double x, double y) 
        //{
        //    if ((this.x - x) == 0)
        //        if ((this.y - y) < 0)
        //            return -Math.PI / 2;
        //        else if ((this.y - y) == 0)
        //            return 0;
        //        else
        //            return Math.PI / 2;
        //    return (this.x - x) < 0 ? Math.Atan((this.y - y) / (this.x - x)) + Math.PI : Math.Atan((this.y - y) / (this.x - x));
        //}
        public double getMagnitudeFrom(Vector3D vec) { return getMagnitudeFrom(vec.X, vec.Y, vec.Z); }                                      //
        public double getMagnitude2From(Vector3D vec) { return getMagnitude2From(vec.X, vec.Y, vec.Z); }
        //public double getDirectionFrom(Vector3D vec) { return getDirectionFrom(vec.X, vec.Y); }
        public double getMagnitudeTo(double x, double y, double z) { return Math.Sqrt(Math.Pow(x - this.x, 2) + Math.Pow(y - this.y, 2) + Math.Pow(z - this.z, 2)); }           //
        public double getMagnitude2To(double x, double y, double z) { return Math.Pow(x - this.x, 2) + Math.Pow(y - this.y, 2) + Math.Pow(z - this.z, 2); }                     //
        //public double getDirectionTo(double x, double y)
        //{
        //    if ((x - this.x) == 0)
        //        if ((y - this.y) < 0)
        //            return -Math.PI / 2;
        //        else if ((y - this.y) == 0)
        //            return 0;
        //        else
        //            return Math.PI / 2;
        //    return (x - this.x) < 0 ? Math.Atan((y - this.y) / (x - this.x)) + Math.PI : Math.Atan((y - this.y) / (x - this.x));
        //}
        public double getMagnitudeTo(Vector3D vec) { return getMagnitudeTo(vec.X, vec.Y, vec.Z); }
        public double getMagnitude2To(Vector3D vec) { return getMagnitude2To(vec.X, vec.Y, vec.Z); }
        //public double getDirectionTo(Vector3D vec) { return getDirectionTo(vec.X, vec.Y); }
        static public Vector3D operator + (Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);                    // ???? W
        }                           //
        static public Vector3D operator - (Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);                    // ???? W
        }
        static public Vector3D operator - (Vector3D lhs, Vector4D rhs)
        {
            return new Vector3D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);                    // ???? W
        }
        public static Vector3D operator -(Vector3D vec)
        { return new Vector3D(-vec.X, -vec.Y, -vec.Z); }

        public static Vector3D cross (Vector3D A, Vector4D B)
        {
            return new Vector3D
                (A.Y * B.Z - A.Z * B.Y,
                 A.Z * B.X - A.X * B.Z,
                 A.X * B.Y - A.Y * B.X);
        }

        public void normalizeOldOld()
        {
            double divisor = X * X + Y * Y + Z * Z;

                X *= X;
                X /= divisor;

                Y *= Y;
                Y /= divisor;

                Z *= Z;
                Z /= divisor;
        }

        public void normalizeOld()
        {
            double divisor = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (divisor != 0)
            {
                X /= divisor;
                Y /= divisor;
                Z /= divisor;
            }
        }

        public void normalize()
        {
            float invSqrt = Utilities.InverseSquareRoot((float)(X * X + Y * Y + Z * Z));
            X *= invSqrt;
            Y *= invSqrt;
            Z *= invSqrt;
        }

        static public double dot(Vector3D lhs, Vector3D rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }
    }
}
