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
    class FreeCamera : Camera
    {
        #region Fields

        Vector4D[] axes;

        #endregion

        #region Properties

        public override Vector3D zAxis { get { return new Vector3D(axes[2].X, axes[2].Y, axes[2].Z); } }
        public override Vector3D yAxis { get { return new Vector3D(axes[1].X, axes[1].Y, axes[1].Z); } }
        public override Vector3D xAxis { get { return new Vector3D(axes[0].X, axes[0].Y, axes[0].Z); } }

        #endregion

        #region Constructors

        public FreeCamera() : this(new Vector4D(0, 0, 0, 1)) { }
        public FreeCamera(Vector4D position) : base(position)
        {
            axes = GameShapes.CameraAxes();
        }

        #endregion

        #region View Matrix Creation

        public override void createView()
        {
            view = StaticMatrices.ViewMatrix(center, axes);
        }

        #endregion

        #region Movement Controls

        public override void reset()
        {
            axes = GameShapes.CameraAxes();
        }

        public override void forward(float amount)
        {
            center += (axes[2] * amount);
        }

        public override void strafe(float amount)
        {
            throw new NotImplementedException();
        }

        public override void elevate(float amount)
        {
            throw new NotImplementedException();
        }

        public override void roll(float amount)
        {
            for (int i = 0; i < 2; i++)
                axes[i].LeftMultiply(StaticMatrices.RotationMatrixArbitraryAxis(axes[2], amount * -.01));
        }

        public override void pitch(float amount)
        {
            for (int i = 1; i < 3; i++)
                axes[i].LeftMultiply(StaticMatrices.RotationMatrixArbitraryAxis(axes[0], amount));
        }

        public override void yaw(float amount)
        {
            for (int i = 0; i < 3; i += 2)
                axes[i].LeftMultiply(StaticMatrices.RotationMatrixArbitraryAxis(axes[1], -amount));
        }

        #endregion
    }
}
