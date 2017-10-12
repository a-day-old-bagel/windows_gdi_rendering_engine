using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class PointLight : Light
    {
        protected Vector3D position;
        protected int intensity;
        protected float distanceToCamera;

        public override Vector3D Position { get { return position; } set { position = value; } }
        public int Intensity { get { return intensity; } }
        public override float renderDepthZ { get { return distanceToCamera; } }
        public override float Radius { get { return 0; } set { } }

        public PointLight(Vector3D position, int intensity, byte red, byte green, byte blue, Camera debugCamera)
        {
            this.position = position;
            this.intensity = intensity;
            this.red = red;
            this.green = green;
            this.blue = blue;
            distanceToCamera = 0;
        }

        public override void draw(Graphics graphics, Camera camera, WorldLights worldLights)
        {
            if (Settings.debug)
            {
                Vector4D centerVector = Vector4D.Zero();

                centerVector.LeftMultiply(StaticMatrices.TranslationMatrix(position));
                centerVector.LeftMultiply(camera.View);

                distanceToCamera = (float)centerVector.getMagnitude();

                centerVector.LeftMultiply(camera.Frustum);
                centerVector.divideByW();
                centerVector.moveOriginToCenterScreen();

                Matrix4 mat = camera.Frustum * camera.View * StaticMatrices.TranslationMatrix(position) * StaticMatrices.ScaleMatrix(new Vector3D(intensity * .0005, intensity * .0005, intensity * .0005));

                Vector4D[] vertices = GameShapes.Tetrahedron();

                for (int i = 0; i < 4; i++)
                {
                    vertices[i].LeftMultiply(mat);
                    vertices[i].divideByW();
                    vertices[i].moveOriginToCenterScreen();
                }

                PointF[] pointsAll = vertices.Select(x => x.getPointXY()).ToArray();
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[0], pointsAll[1]);
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[0], pointsAll[2]);
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[0], pointsAll[3]);
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[1], pointsAll[2]);
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[1], pointsAll[3]);
                graphics.DrawLine(new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 2), pointsAll[2], pointsAll[3]);

                graphics.DrawString((intensity * .001).ToString() + "K", new Font("Arial", 12), new SolidBrush(Color.White), centerVector.getPointXY());
            }
        }
    }
}
