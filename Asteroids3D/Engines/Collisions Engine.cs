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
    /// <summary>
    /// Still need to update the engines to allow removal from the delegates.  Will do...
    /// </summary>
    class CollisionsEngine : Engine     // STILL not yet tested or used
    {
        #region Constructor

        public CollisionsEngine(int inverseFPMS) : base(inverseFPMS) { }

        #endregion

        #region External Methods

        public void addObjectsToDetect(GameObject A, GameObject B)
        {
            workQueue += () => Collisions.handleCollision(A, B);
        }

        public void addObjectsToDetect(GameObject A, GameObject[] B)
        {
            foreach (GameObject obj in B)
                addObjectsToDetect(A, obj);
        }

        public void addObjectsToDetectParallel(GameObject A, GameObject[] B)
        {
            addPermanentRoutine(() =>
            {
                Parallel.For(0, B.Length, index =>
                {
                    Collisions.handleCollision(A, B[index]);
                });
            });
        }

        public void addObjectsToDetect(GameObject[] A, GameObject[] B)
        {
            foreach (GameObject objA in A)
                foreach (GameObject objB in B)
                    addObjectsToDetect(objA, objB);
        }

        public void addObjectsToDetectParallel(GameObject[] A, GameObject[] B)
        {
            addPermanentRoutine(() =>
            {
                Parallel.For(0, A.Length, indexA =>
                {
                    Parallel.For(0, B.Length, indexB =>
                    {
                        Collisions.handleCollision(A[indexA], B[indexB]);
                    });
                });
            });
        }

        #endregion
    }
}
