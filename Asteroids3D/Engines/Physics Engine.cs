/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asteroids3D
{
    /// <summary>
    /// Removal of objects now possible.
    /// </summary>
    class PhysicsEngine : Engine
    {
        #region Fields

        private delegate void PhysicsQueue(float t);
        private PhysicsQueue physicsQueue;

        private float timeStep;
        private float FPMS;

        #endregion

        #region Constructor

        public PhysicsEngine (int inverseFPMS) : base (inverseFPMS)
        {
            physicsQueue = new PhysicsQueue((timeStep) => { });
            FPMS = 1 / (float)inverseFPMS;
        }

        #endregion

        #region Internal Methods

        protected override void work()
        {
            while (!stopped)
            {
                base.WaitNextFrame();

                // All physics calculations are multiplied by a timestep for creamy smoothness.  Make it bigger to speed it up.
                if (FPS_deltaTime <= FPS_inverseFPMS) timeStep = 1;
                else timeStep = (float)FPS_deltaTime * FPMS;

                if (!paused)
                {
                    physicsQueue(timeStep);
                }

                // Even when the game is paused, controls are active (so that you can un-pause).  Needs work.
                workQueue();
            }
        }

        #endregion

        #region External Methods

        public void addObjectToMove(GameEntity gameObj)
        {
            physicsQueue += gameObj.move;
        }

        public void addObjectsToMove(GameEntity[] gameObjs)
        {
            foreach (GameObject obj in gameObjs)
                physicsQueue += obj.move;
        }

        public void removeObjectToMove(GameEntity gameObj)
        {
            physicsQueue -= gameObj.move;
        }

        public void removeObjectsToMove(GameEntity[] gameObjs)
        {
            foreach (GameObject obj in gameObjs)
                physicsQueue -= obj.move;
        }

        public void addControlRoutine(Action routine)
        {
            workQueue += () => routine();
        }

        #endregion        
    }
}
