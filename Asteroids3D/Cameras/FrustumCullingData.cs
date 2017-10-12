using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    class FrustumCullingData
    {
        public float xMultiplier;
        public float yMultiplier;

        public float width;
        public float height;

        public float d;

        public float xSphereFactor;
        public float ySphereFactor;

        public Vector3D camRelativeCoordinates;
        public Vector3D utilityVector;

        public FrustumCullingData(float FOV, float aspRat)
        {
            xMultiplier = 2800 * (float)Math.Tan(FOV / 2);
            yMultiplier = xMultiplier * .562F;

            xSphereFactor = 1 / (float)Math.Cos(FOV);
            ySphereFactor = xSphereFactor;

            camRelativeCoordinates = new Vector3D();
            utilityVector = new Vector3D();
        }
    }
}
