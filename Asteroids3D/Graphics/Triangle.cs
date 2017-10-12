using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class Triangle                                              // UNUSED RIGHT NOW - FOR NON-GDI RENDERING
    {
        /*

        int[] vertexIndex;
        public Triangle(int pointA, int pointB, int pointC)
        {
            vertexIndex[0] = pointA;
            vertexIndex[1] = pointB;
            vertexIndex[2] = pointC;
        }

        public void draw(Vector4D[] meshVertices, Graphics graphics)
        {
            //graphics.DrawLine(new Pen(Color.Red), meshVertices[vertexIndex[0]].getPointXY(),
            //                                        meshVertices[vertexIndex[1]].getPointXY());

            //graphics.DrawLine(new Pen(Color.Red), meshVertices[vertexIndex[1]].getPointXY(),
            //                                        meshVertices[vertexIndex[2]].getPointXY());

            //graphics.DrawLine(new Pen(Color.Red), meshVertices[vertexIndex[2]].getPointXY(),
            //                                        meshVertices[vertexIndex[0]].getPointXY());

            graphics.DrawPolygon(new Pen(Color.Red), new PointF[] {meshVertices[vertexIndex[0]].getPointXY(), 
                                                                   meshVertices[vertexIndex[1]].getPointXY(),
                                                                   meshVertices[vertexIndex[2]].getPointXY()});
        }

        public Vector4D getNormal(Vector4D[] meshVertices)
        {
            return Vector4D.cross(meshVertices[vertexIndex[0]] - meshVertices[vertexIndex[1]],
                                  meshVertices[vertexIndex[1]] - meshVertices[vertexIndex[2]]);
        }
        
        */
    }
}
