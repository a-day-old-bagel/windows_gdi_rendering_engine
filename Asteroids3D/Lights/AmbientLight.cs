using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class AmbientLight : Light
    {
        public override float renderDepthZ { get { return 0; } }
        public override float Radius { get { return 0; } set { } }
        public override Vector3D Position { get { return new Vector3D(); } set { } }

        public AmbientLight(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public override void draw(Graphics graphics, Camera camera, WorldLights worldLights)
        {

        }
    }
}
