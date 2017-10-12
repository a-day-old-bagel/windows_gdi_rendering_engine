/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 * Origin.cs
 * 
 * A drawable origin for debugging purposes.  Appears in game as the positive
 * x, y, and z axes in Cyan, Magenta, and Yellow respectively.
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class Origin : GameEntity
    {
        #region Fields

        Vector4D[] renderVertices;
        double scale;
        float distanceToCamera;

        #endregion

        #region Properties
        public override float renderDepthZ { get { return distanceToCamera; } }
        public override float Radius { get { return 0; } set { } }
        public override Vector3D Position { get { return new Vector3D(); } set { } }

        #endregion

        #region Constructor

        public Origin(double scale)
        {
            this.scale = scale;
            renderVertices = GameShapes.UnitAxes();
            distanceToCamera = 0;
        }

        #endregion

        #region Graphics Engine Methods

        public override void draw(Graphics graphics, Camera camera, WorldLights worldLights)
        {
            if (Settings.debug)
            {
                distanceToCamera = (float)camera.Position.getMagnitude();
                Matrix4 ModelViewProjection = camera.Frustum * camera.View * StaticMatrices.UniformScaleMatrix(scale);

                renderVertices = GameShapes.UnitAxes();
                foreach (Vector4D vertex in renderVertices)
                {
                    vertex.LeftMultiply(ModelViewProjection);

                    if (Math.Abs(vertex.Z) < Math.Abs(vertex.W))
                    {
                        vertex.divideByW();
                        vertex.moveOriginToCenterScreen();
                    }
                }

                PointF[] pointsAll = renderVertices.Select(x => x.getPointXY()).ToArray();
                graphics.DrawLine(new Pen(Color.Cyan), pointsAll[0], pointsAll[1]);
                graphics.DrawLine(new Pen(Color.Magenta), pointsAll[0], pointsAll[2]);
                graphics.DrawLine(new Pen(Color.Yellow), pointsAll[0], pointsAll[3]);
            }
        }

        #endregion

        public override void Die() { }
        public override void move(float timeStep) { }
        public override void preDraw(Graphics graphics, Camera camera, WorldLights worldLights) { }
    }
}
