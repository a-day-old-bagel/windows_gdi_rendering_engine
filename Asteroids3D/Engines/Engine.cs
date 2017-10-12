/***********************************************************************************
 * Nathan Bitikofer and Galen Cochrane 12 DEC 2014
 * Engine.cs
 * 
 * Following the general guideline for a delegate-based looping engine as laid
 * down by Nathan Bitikofer in the year of our Lord two thousand and fourteen...
 * 
 * ...I Galen Cochrane do pay homage to his elegant design, although I may have
 * screwed it up a bit in the process.
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
    /// Still need to update the engines to allow removal from the delegates.  Will do...
    /// </summary>
    class Engine
    {
        #region Fields

        protected delegate void WorkQueue();
        protected WorkQueue workQueue;

        private Task workTask;
        protected bool stopped;
        protected bool paused;

        private int FPS_currentTime;
        private int FPS_previousTime;
        protected int FPS_deltaTime;
        protected int FPS_inverseFPMS;

        #endregion

        #region Properties

        public bool Terminated { get { return stopped; } }
        public bool Paused { get { return paused; } }

        #endregion

        #region Constructor

        public Engine(int inverseFPMS)
        {
            workQueue = new WorkQueue(() => { });
            stopped = false;
            paused = false;
            FPS_inverseFPMS = inverseFPMS;
        }

        #endregion

        #region External Methods

        public void Pause() { paused = true; }
        public void Resume() { paused = false; }
        public void Terminate() { stopped = true; }

        public void Initialize()
        {
            FPS_previousTime = Environment.TickCount;
            FPS_currentTime = 0;
            FPS_deltaTime = 0;
            workTask = Task.Run(() => work());
        }

        public void addPermanentRoutine(Action routine)
        {
            workQueue += () => routine();
        }

        #endregion

        #region Internal Methods

        protected virtual void work()
        {
            while (!stopped)
            {
                WaitNextFrame();
                if (!paused)
                    workQueue();
            }
        }

        protected void WaitNextFrame()
        {
            FPS_currentTime = Environment.TickCount;
            FPS_deltaTime = FPS_currentTime - FPS_previousTime;
            if (FPS_deltaTime < FPS_inverseFPMS)
                Thread.Sleep(FPS_inverseFPMS - FPS_deltaTime);
            FPS_previousTime = FPS_currentTime;
        }

        #endregion
    }
}
