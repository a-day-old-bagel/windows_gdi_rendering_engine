using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    abstract class Camera
    {
        #region Fields

        Matrix4 frustum;
        
        protected Matrix4 view;
        protected Vector4D center;
        float fov;
        float zNear;
        float zFar;
        float aspRat;

        #endregion

        #region Properties

        public Matrix4 Frustum { get { return frustum; } }
        public Matrix4 View { get { return view; } }
        public Vector4D Position { get { return center; } }
        public float FOV { get { return fov; } set { fov = value; } }
        public float ZNear { get { return zNear; } set { zNear = value; } }
        public float ZFar { get { return zFar; } set { zFar = value; } }
        public float AspRat { get { return aspRat; } set { aspRat = value; } }

        public abstract Vector3D zAxis { get; }
        public abstract Vector3D yAxis { get; }
        public abstract Vector3D xAxis { get; }

        #endregion

        #region Constructor

        protected Camera(Vector4D position)
        {
            fov = .002F;        // Field Of View -  I have no idea what units this ended up being in.  Certainly nothing human.
            aspRat = 1F;         // Aspect Ratio - this is supposed to match the screen.  I think maybe GDI's graphics object already accounts for this.
            zNear = 1F;         // How close to the camera's position objects can render before they get culled
            zFar = 1000F;      // How far away they can be before they're culled
            createFrustum();    // generates the frustum perspective matrix
            center = position;  // positions the camera at the provided coordinates in space
        }

        #endregion

        #region View and Projection Matrix Creation

        public virtual void createFrustum()
        {
            frustum = StaticMatrices.FrustumMatrix(fov, aspRat, zNear, zFar);       // should be 1.78 FOV for 1366x768, but whatever...
        }

        public abstract void createView();

        #endregion

        #region Abstract Movement Controls

        public abstract void reset();
        public abstract void forward(float amount);
        public abstract void strafe(float amount);
        public abstract void elevate(float amount);
        public abstract void roll(float amount);
        public abstract void pitch(float amount);
        public abstract void yaw(float amount);

        #endregion
    }
}
