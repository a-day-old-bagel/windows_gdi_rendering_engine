/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    static class GameShapes
    {
        #region Simple Vertex Models

        static public Vector4D[] UnitCube()
        {
            return new Vector4D[] 
            {new Vector4D( .5,  .5,  .5,  1),
             new Vector4D( .5,  .5, -.5,  1),
             new Vector4D( .5, -.5,  .5,  1),
             new Vector4D( .5, -.5, -.5,  1),
             new Vector4D(-.5,  .5,  .5,  1),
             new Vector4D(-.5,  .5, -.5,  1),
             new Vector4D(-.5, -.5,  .5,  1),
             new Vector4D(-.5, -.5, -.5,  1)};
        }
        static public Vector4D[] Tetrahedron()
        {
            return new Vector4D[]
            {new Vector4D( 0, 0, 0.61237,  1),
             new Vector4D( -0.288675, -.5, -0.204124,  1),
             new Vector4D( -0.288675, .5, -0.204124,  1),
             new Vector4D( 0.57735, 0, -0.204124,  1)};
        }
        static public Vector4D[] UnitAxes()
        {
            return new Vector4D[] 
            {new Vector4D( 0, 0, 0, 1),
             new Vector4D( 1, 0, 0, 1),
             new Vector4D( 0, 1, 0, 1),
             new Vector4D( 0, 0, 1, 1)};
        }
        static public Vector4D[] CameraAxes()
        {
            return new Vector4D[]
            {new Vector4D(1, 0, 0, 1),
             new Vector4D(0, 1, 0, 1),
             new Vector4D(0, 0, 1, 1)};
        }

        #endregion

        #region Simple GDI Meshes

        static public MeshGDI UnitCubeGDI()
        {
            return new MeshGDI(
                new Vector4D[] 
                    {new Vector4D( .5,  .5,  .5,  1),
                     new Vector4D( .5,  .5, -.5,  1),
                     new Vector4D( .5, -.5,  .5,  1),
                     new Vector4D( .5, -.5, -.5,  1),
                     new Vector4D(-.5,  .5,  .5,  1),
                     new Vector4D(-.5,  .5, -.5,  1),
                     new Vector4D(-.5, -.5,  .5,  1),
                     new Vector4D(-.5, -.5, -.5,  1)},
                new FaceGDI[]
                    {new FaceGDI(new int[] {5, 7, 3, 1}),
                     new FaceGDI(new int[] {0, 2, 6, 4}),
                     new FaceGDI(new int[] {1, 3, 2, 0}),
                     new FaceGDI(new int[] {3, 7, 6, 2}),
                     new FaceGDI(new int[] {7, 5, 4, 6}),
                     new FaceGDI(new int[] {5, 1, 0, 4})});
        }

        #endregion
    }
}
