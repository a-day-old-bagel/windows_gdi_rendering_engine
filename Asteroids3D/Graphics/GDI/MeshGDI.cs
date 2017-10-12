using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    class MeshGDI
    {
        #region Fields

        /// <summary>
        /// Stores the object relative vertices. Does not change.
        /// </summary>
        Vector4D[] staticVertices;

        /// <summary>
        /// Stores the transformed vertices used for rendering.  Recalculated each frame.
        /// </summary>
        Vector4D[] dynamicVertices;

        /// <summary>
        /// Stores the face information for the mesh (a face is just a list of vertices right now)
        /// </summary>
        FaceGDI[] faces;

        /// <summary>
        /// Stores the distance of the center of the mesh from the camera.  Used by the graphics engine for Z-sorting.
        /// </summary>
        float distanceFromCamera;

        /// <summary>
        /// Stores the location in view space of the center of the mesh.  Useful for drawing the distance read out and stuff.
        /// </summary>
        Vector4D renderPositionGDI;

        /// Planning to use a boolean like this for each mesh during object culling (frustum culling). False if all vertices are outside of view.
        //bool drawsThisFrame;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the final render coordinates of the center of the mesh
        /// </summary>
        public Vector4D RenderPositionGDI { get { return renderPositionGDI; } }

        /// <summary>
        /// Gets the distance of the center of the mesh from the camera in terms of world space coordinates.
        /// </summary>
        public float DistanceFromCamera { get { return distanceFromCamera; } }

        #endregion

        #region Constructor

        public MeshGDI(Vector4D[] vertices, FaceGDI[] faces)
        {
            this.faces = faces;
            staticVertices = vertices;
            dynamicVertices = new Vector4D[staticVertices.Length];
            for (int i = 0; i < staticVertices.Length; i++)
                dynamicVertices[i] = new Vector4D();
            renderPositionGDI = Vector4D.Zero();
        }

        #endregion

        #region Render Methods

        /// <summary>
        /// preDraw is called by the graphics engine before object Z-sorting is done.  It get's everything ready for drawing.
        /// </summary>
        /// <param name="graphics">GDI graphics to use</param>
        /// <param name="camera">Camera to use</param>
        /// <param name="worldLights">Set of lights to use</param>
        /// <param name="rotation">Rotation of mesh</param>
        /// <param name="scale">Scale of mesh</param>
        /// <param name="position">Position of mesh</param>
        public void preDraw(Graphics graphics, Camera camera, WorldLights worldLights, Vector3D rotation, Vector3D scale, Vector3D position)
        {
            #region Generate transform matrices

            Matrix4 ViewProjectionMatrix = camera.Frustum * camera.View;
            Matrix4 WorldTransformMatrix = StaticMatrices.TranslationMatrix(position)
                                         * StaticMatrices.RotationMatrixZYX(rotation) * StaticMatrices.ScaleMatrix(scale);

            #endregion

            #region Start with vertices in object (local) space

            for (int i = 0; i < dynamicVertices.Length; i++)
                dynamicVertices[i].setVector(staticVertices[i]);

            #endregion

            #region Transform vertices to world space (combinining the scale, rotation, and position matrices)

            foreach (Vector4D vertex in dynamicVertices)
                vertex.LeftMultiply(WorldTransformMatrix);

            #endregion

            #region Calculate lighting from world lights

            /// Have yet to test whether parallel is actually faster, so leaving original code:

            //foreach (FaceGDI face in faces)
            //    face.calculateWorldLighting(dynamicVertices, worldLights, position);

            Parallel.For(0, faces.Length, i =>
            {
                faces[i].calculateWorldLighting(dynamicVertices, worldLights, position);
            });

            #endregion

            #region Transform vertices to view space (combining camera view and frustum transforms)

            foreach (Vector4D vertex in dynamicVertices)
            {
                /// Transforms vertex into camera space and applies frustum matrix
                vertex.LeftMultiply(ViewProjectionMatrix);

                /// Creates the illusion of perspective, shringing the x, y, and z values based on the original z value, as stored in w.
                vertex.divideByW();

                /// Required to compensate for GDI's upper-left-hand corner origin - could be done elsewhere - maybe doesn't need to be done for each vertex each frame - look into it.
                vertex.moveOriginToCenterScreen();
            }

            #endregion

            // Faces are now ready to draw.  This last bit is for the Z-Sorting and distance read out and stuff.

            #region Calculate render position and distance from camera

            renderPositionGDI.zero();
            renderPositionGDI.LeftMultiply(WorldTransformMatrix);
            renderPositionGDI.LeftMultiply(camera.View);
            distanceFromCamera = (float)renderPositionGDI.getMagnitude();

            if (Settings.debug)
            {
                renderPositionGDI.LeftMultiply(camera.Frustum);
                renderPositionGDI.divideByW();
                renderPositionGDI.moveOriginToCenterScreen();
            }

            #endregion
        }

        /// <summary>
        /// draw is called by the graphics engine after Z-sorting is done.  Just draws the mesh's faces onto the GDI graphics object.
        /// </summary>
        /// <param name="graphics"></param>
        public void draw(Graphics graphics)
        {
            foreach (FaceGDI face in faces)
                face.draw(dynamicVertices, graphics);

            #region Draw the distance read out

            if (Settings.debug)
                graphics.DrawString(((int)distanceFromCamera).ToString(), new Font("Arial", 8), new SolidBrush(Color.Red), renderPositionGDI.getPointXY());

            #endregion
        }

        #endregion
    }
}
