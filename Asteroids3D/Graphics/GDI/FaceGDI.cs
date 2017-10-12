using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class FaceGDI
    {
        #region Fields

        int[] vertexIndex;
        PointF[] drawPoints;
        Vector3D normal;
        int[] color;
        Vector3D lightDirection;
        float lightBrightness;
        float pointLightBrightnessFallOff;

        #endregion

        #region Constructor

        /// <summary>
        /// Face is provided with a set of indices to its parent mesh's vertices
        /// </summary>
        /// <param name="points">set of integer indices</param>
        public FaceGDI(int[] points)
        {
            color = new int[3];
            vertexIndex = points;
            drawPoints = new PointF[vertexIndex.Length];
        }

        #endregion

        #region Render Methods

        /// <summary>
        /// Calculates the lighting for a face before it is drawn
        /// </summary>
        /// <param name="meshVertices">List of all vertices of the mesh to which this face belongs</param>
        /// <param name="worldLights">Set of all the world's lights that affect this face</param>
        /// <param name="position">Position of the mesh to which this face belongs, in world space</param>
        public void calculateWorldLighting(Vector4D[] meshVertices, WorldLights worldLights, Vector3D position)
        {
            color[0] = 0;
            color[1] = 0;
            color[2] = 0;

            normal = Vector4D.cross3D(meshVertices[vertexIndex[0]] - meshVertices[vertexIndex[1]],
                                        meshVertices[vertexIndex[1]] - meshVertices[vertexIndex[2]]);

            normal.normalize();

            #region Ambient Lights

            foreach (AmbientLight light in worldLights.AmbientLights)
            {
                color[0] += light.Red;
                color[1] += light.Green;
                color[2] += light.Blue;
            }

            #endregion

            #region Directional Lights

            foreach (DirectionalLight light in worldLights.DirectionalLights)
            {
                lightBrightness = (float)Math.Max(0, Vector3D.dot(normal, light.Direction));
                color[0] += (int)(light.Red * lightBrightness);
                color[1] += (int)(light.Green * lightBrightness);
                color[2] += (int)(light.Blue * lightBrightness);
            }

            #endregion

            #region Point Lights

            foreach (PointLight light in worldLights.PointLights)
            {
                lightDirection = position - light.Position;
                pointLightBrightnessFallOff = (float)(Math.Pow(lightDirection.getMagnitude(), 2) / light.Intensity);

                lightDirection.normalize();

                lightBrightness = (float)Math.Max(0, Vector3D.dot(normal, lightDirection) / Math.Max(1, pointLightBrightnessFallOff));
                color[0] += (int)(light.Red * lightBrightness);
                color[1] += (int)(light.Green * lightBrightness);
                color[2] += (int)(light.Blue * lightBrightness);
            }

            #endregion
        }

        /// <summary>
        /// draws the face as a 2d polygon on the GDI graphics object provided
        /// </summary>
        /// <param name="meshVertices">List of all vertices of the mesh to which this face belongs</param>
        /// <param name="graphics">GDI graphics object on which to draw</param>
        public void draw(Vector4D[] meshVertices, Graphics graphics)
        {
            if (Vector4D.frontFacing(meshVertices[vertexIndex[0]] - meshVertices[vertexIndex[1]],
                                     meshVertices[vertexIndex[1]] - meshVertices[vertexIndex[2]], out normal))
            {
                for (int i = 0; i < drawPoints.Length; i++)
                    drawPoints[i] = meshVertices[vertexIndex[i]].getPointXY();

                for (int i = 0; i < 3; i++)
                    if (color[i] > 255)
                        color[i] = 255;

                graphics.FillPolygon(new SolidBrush(Color.FromArgb(color[0], color[1], color[2])), drawPoints);
            }
        }

        #endregion
    }
}
