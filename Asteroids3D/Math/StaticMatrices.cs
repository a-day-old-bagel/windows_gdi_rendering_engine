/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 * StaticMatrices.cs
 * 
 * Methods to return various 3D affine transformation matrices
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    static class StaticMatrices
    {
        #region Model Transforms

        static public Matrix4 RotationMatrixArbitraryAxis(Vector4D unitAxis, double angle)
        {
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            double t = 1 - Math.Cos(angle);
            double x = unitAxis.X;
            double y = unitAxis.Y;
            double z = unitAxis.Z;
            double x2 = Math.Pow(x, 2);
            double y2 = Math.Pow(y, 2);
            double z2 = Math.Pow(z, 2);

            return new Matrix4
                (t*x2  + c    ,  t*x*y - s*z  ,  t*x*z + s*y  ,  0  ,
                 t*x*y + s*z  ,  t*y2  + c    ,  t*y*z - s*x  ,  0  ,
                 t*x*z - s*y  ,  t*y*z + s*x  ,  t*z2  + c    ,  0  ,
                 0            ,  0            ,  0            ,  1  );
        }

        static public Matrix4 RotationMatrixZYX(Vector3D rot) { return RotationMatrixZYX(rot.X, rot.Y, rot.Z); }
        static public Matrix4 RotationMatrixZYX(double X, double Y, double Z)
        {
            double cx = Math.Cos(X);
            double sx = Math.Sin(X);
            double cy = Math.Cos(Y);
            double sy = Math.Sin(Y);
            double cz = Math.Cos(Z);
            double sz = Math.Sin(Z);

            return new Matrix4
                (cy*cz              , -cy*sz             , sy        , 0 ,
                 cx*sz + (sx*sy*cz) , cx*cz - (sx*sy*sz) , -sx*cy    , 0 ,
                 sx*sz - (cx*sy*cz) , sx*cz + (cx*sy*sz) , cx*cy     , 0 ,
                 0                  , 0                  , 0         , 1 );
        }

        static public Matrix4 ScaleMatrix(Vector3D scl) { return ScaleMatrix(scl.X, scl.Y, scl.Z); }
        static public Matrix4 ScaleMatrix(double X, double Y, double Z)
        {
            return new Matrix4
                (X, 0, 0, 0,
                 0, Y, 0, 0,
                 0, 0, Z, 0,
                 0, 0, 0, 1);
        }
        static public Matrix4 UniformScaleMatrix(double U)
        {
            return new Matrix4
                (U, 0, 0, 0,
                 0, U, 0, 0,
                 0, 0, U, 0,
                 0, 0, 0, 1);
        }

        static public Matrix4 TranslationMatrix(Vector3D pos) { return TranslationMatrix(pos.X, pos.Y, pos.Z); }
        static public Matrix4 TranslationMatrix(double X, double Y, double Z)
        {
            return new Matrix4
                (1, 0, 0, X,
                 0, 1, 0, Y,
                 0, 0, 1, Z,
                 0, 0, 0, 1);
        }

        #endregion

        #region View Transforms

        static public Matrix4 ViewMatrix(Vector4D center, double pitch, double yaw)
        {
            double cosPitch = Math.Cos(pitch);
            double sinPitch = Math.Sin(pitch);
            double cosYaw = Math.Cos(yaw);
            double sinYaw = Math.Sin(yaw);

            Vector4D[] axes = new Vector4D[3];
            axes[0] = new Vector4D( cosYaw, 0, -sinYaw, 1 );
            axes[1] = new Vector4D( sinYaw * sinPitch, cosPitch, cosYaw * sinPitch, 1 );
            axes[2] = new Vector4D( sinYaw * cosPitch, -sinPitch, cosPitch * cosYaw, 1 );

            return ViewMatrix(center, axes);
        }
        static public Matrix4 ViewMatrix(Vector4D center, Vector4D[] cameraAxes)
        {
            return new Matrix4
                (cameraAxes[0].X, cameraAxes[0].Y, cameraAxes[0].Z, -Vector4D.dot(cameraAxes[0], center),
                 cameraAxes[1].X, cameraAxes[1].Y, cameraAxes[1].Z, -Vector4D.dot(cameraAxes[1], center),
                 cameraAxes[2].X, cameraAxes[2].Y, cameraAxes[2].Z, -Vector4D.dot(cameraAxes[2], center),
                 0, 0, 0, 1);
        }

        #endregion

        #region Projection Transforms

        static public Matrix4 FrustumMatrix(float FOV, float aspRat, float zNear, float zFar)
        {
            float tanFOV = (float)Math.Tan(FOV);
            float zDiff = zFar - zNear;

            return new Matrix4(                      // HELPFUL MATRIX VIEW
            /**|=================================================================================================/**/
            /**/  1 / tanFOV  ,/**/         0         ,/**/          0              ,/**/          0             /**/,
            /**|=================================================================================================/**/
            /**/       0      ,/**/  aspRat / tanFOV  ,/**/          0              ,/**/          0             /**/,
            /**|=================================================================================================/**/
            /**/       0      ,/**/         0         ,/**/ (zFar + zNear) / zDiff  ,/**/ -2*zFar*zNear / zDiff  /**/,
            /**|=================================================================================================/**/
            /**/       0      ,/**/         0         ,/**/          1              ,/**/          0             /**/);
            /**|=================================================================================================/**/
        }

        #endregion
    }
}
