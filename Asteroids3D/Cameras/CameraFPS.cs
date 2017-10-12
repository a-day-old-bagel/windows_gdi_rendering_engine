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
    class FPSCamera : Camera
    {
        #region Fields

        double _pitch;
        double _yaw;

        #endregion

        #region Properties

        /// <summary>
        /// Doing these calculations twice each frame right now.  May have to store axes in fpsCam as well (like free cam) to optimize.
        /// </summary>
        public override Vector3D zAxis { get { return new Vector3D(Math.Sin(_yaw) * Math.Cos(_pitch), -Math.Sin(_pitch), Math.Cos(_pitch) * Math.Cos(_yaw)); } }
        public override Vector3D yAxis { get { return new Vector3D(Math.Sin(_yaw) * Math.Sin(_pitch), Math.Cos(_pitch), Math.Cos(_yaw) * Math.Sin(_pitch)); } }
        public override Vector3D xAxis { get { return new Vector3D(Math.Cos(_yaw), 0, -Math.Sin(_yaw)); } }

        #endregion

        #region Constructors

        public FPSCamera() : this(new Vector4D(0, 0, 0, 1)) { }
        public FPSCamera(Vector4D position) : base(position)
        {       
            _pitch = 0;
            _yaw = 0;
        }

        #endregion

        #region View Matrix Creation

        public override void createView()
        {
            view = StaticMatrices.ViewMatrix(center, _pitch, _yaw);
        }

        #endregion

        #region Movement Controls

        public override void reset()
        {
            _pitch = 0;
            _yaw = 0;
        }

        public override void forward(float amount)
        {
            center += new Vector4D(Math.Sin(_yaw), 0, Math.Cos(_yaw), 0) * amount;
        }

        public override void strafe(float amount)
        {
            center -= new Vector4D(amount * Math.Sin(_yaw + Math.PI / 2), 0, amount * Math.Cos(_yaw + Math.PI / 2), 0);
        }

        public override void elevate(float amount)
        {
            center.Y -= amount;
        }

        public override void roll(float amount)
        {
            strafe(amount);     // The FPS camera doesn't roll.
        }

        public override void pitch(float amount)
        {
            _pitch += amount;
            if (_pitch > 1.57) _pitch = 1.57;
            if (_pitch < -1.57) _pitch = -1.57;
        }

        public override void yaw(float amount)
        {
            _yaw -= amount;
        }

        #endregion
    }
}
