using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    class Plane
    {
        //Vector3D normal;
        //Vector3D point;

        public Vector3D Normal { get; set; }
        public Vector3D Point { get; set; }

        public Plane(Vector3D point, Vector3D normal)
        {
            Normal = normal;
            Point = point;
        }
    }
}
