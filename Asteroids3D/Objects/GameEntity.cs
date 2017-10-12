using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    abstract class GameEntity
    {
        bool drawsThisFrame = true;

        public bool DrawsThisFrame { get { return drawsThisFrame; } set { drawsThisFrame = value; } }
        public abstract float Radius { get; set; }
        public abstract Vector3D Position { get; set; }
        public abstract float renderDepthZ { get; }

        public abstract void draw(Graphics graphics, Camera camera, WorldLights worldLights);
        public abstract void preDraw(Graphics graphics, Camera camera, WorldLights worldLights);
        public abstract void move(float timeStep);
        public abstract void Die();
    }
}
