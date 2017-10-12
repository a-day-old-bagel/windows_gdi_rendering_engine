using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    abstract class Light : GameEntity
    {
        protected byte red;
        protected byte green;
        protected byte blue;

        public byte Red { get { return red; } }
        public byte Green { get { return green; } }
        public byte Blue { get { return blue; } }
        public abstract override float renderDepthZ { get; }

        public override void Die() { }
        public override void move(float timeStep) { }
        public override void preDraw(Graphics graphics, Camera camera, WorldLights worldLights) { }
        public abstract override void draw(Graphics graphics, Camera camera, WorldLights worldLights);
    }
}
