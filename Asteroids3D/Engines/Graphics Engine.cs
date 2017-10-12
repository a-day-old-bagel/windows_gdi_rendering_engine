/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids3D
{
    /// <summary>
    /// Removal of objects now possible.
    /// </summary>
    class GraphicsEngineGDI : Engine
    {
        #region Fields

        protected delegate void RenderQueue(Graphics g, Camera c, WorldLights l);
        protected RenderQueue renderQueue;

        Graphics graphics;

        BufferedGraphicsContext backBufferContext;
        BufferedGraphics backBuffer;

        List<GameEntity> zSortedObjects;
        Camera camera;
        FrustumCullingData cullData;
        WorldLights worldLights;

        #endregion

        #region Constructor

        public GraphicsEngineGDI(int inverseFPMS, Graphics graphics, Camera camera, WorldLights worldLights)
            : base(inverseFPMS)
        {
            renderQueue = new RenderQueue((Graphics g, Camera c, WorldLights l) => { });
            zSortedObjects = new List<GameEntity>();
            cullData = new FrustumCullingData(camera.FOV, camera.AspRat);
            this.camera = camera;
            this.graphics = graphics;
            this.worldLights = worldLights;
            backBufferContext = BufferedGraphicsManager.Current;
            backBuffer = backBufferContext.Allocate(graphics, Utilities.screenBounds);
            
            addPermanentRoutine(backBuffer.Render);            
            addPermanentRoutine(camera.createView);            
            addPermanentRoutine(ClearScreen);
            addPermanentRoutine(cullAndSortRenderOrder);            
        }

        #endregion

        #region Internal Methods

        private void ClearScreen()
        {
            if (!stopped && !paused)
            {
                backBuffer.Graphics.Clear(Color.Black);
            }
        }

        protected override void work()
        {
            while (!stopped)
            {
                WaitNextFrame();
                if (!paused)
                {
                    workQueue();
                    renderQueue(backBuffer.Graphics, camera, worldLights);
                }
            }
        }

        private void cullAndSortRenderOrder()
        {
            foreach (GameEntity obj in zSortedObjects)
                if (obj.DrawsThisFrame)
                    renderQueue -= obj.draw;

            performFrustumCulling();

            /// Have yet to test whether parallel is actually faster, so leaving original code:

            //foreach (GameEntity obj in zSortedObjects)
            //    if (obj.DrawsThisFrame)
            //        obj.preDraw(backBuffer.Graphics, camera, worldLights);

            Parallel.For(0, zSortedObjects.Count, i =>
                {
                    if (zSortedObjects[i].DrawsThisFrame)
                        zSortedObjects[i].preDraw(backBuffer.Graphics, camera, worldLights);
                });

            zSortedObjects.Sort((a, b) => b.renderDepthZ.CompareTo(a.renderDepthZ)); // Sorts all - not optimized

            foreach (GameEntity obj in zSortedObjects)
                if (obj.DrawsThisFrame)
                    renderQueue += obj.draw;
        }

        private void performFrustumCulling()
        {
            foreach (GameEntity obj in zSortedObjects)
                if (sphereIntersectsFrustum(obj.Position, obj.Radius))
                    obj.DrawsThisFrame = true;
                else obj.DrawsThisFrame = false;
        }

        private bool sphereIntersectsFrustum(Vector3D center, float radius)
        {
            cullData.utilityVector = center - camera.Position;
            cullData.camRelativeCoordinates.Z = Vector3D.dot(cullData.utilityVector, camera.zAxis);

            if (cullData.camRelativeCoordinates.Z - radius < camera.ZNear || cullData.camRelativeCoordinates.Z + radius > camera.ZFar)
                return false;

            cullData.camRelativeCoordinates.Y = Vector3D.dot(cullData.utilityVector, camera.yAxis);

            cullData.height = (float)cullData.camRelativeCoordinates.Z * cullData.yMultiplier;
            cullData.d = radius * cullData.ySphereFactor;

            if (cullData.camRelativeCoordinates.Y > cullData.height * .5F + cullData.d || cullData.camRelativeCoordinates.Y < -cullData.height * .5F - cullData.d)
                return false;

            cullData.camRelativeCoordinates.X = Vector3D.dot(cullData.utilityVector, camera.xAxis);
            cullData.width = (float)cullData.camRelativeCoordinates.Z * cullData.xMultiplier;
            cullData.d = radius * cullData.xSphereFactor;

            if (cullData.camRelativeCoordinates.X > cullData.width * .5F + cullData.d || cullData.camRelativeCoordinates.X < -cullData.width * .5F - cullData.d)
                return false;

            return true;
        }

        #endregion

        #region External Methods

        public void addObjectToRender(GameEntity gameObj)
        {
            renderQueue += gameObj.draw;
        }

        public void removeObjectToRender(GameEntity gameObj)
        {
            renderQueue -= gameObj.draw;
        }

        public void addObjectsToRender(GameEntity[] gameObjs)
        {
            foreach (GameObject obj in gameObjs)
                renderQueue += obj.draw;
        }

        public void removeObjectsToRender(GameEntity[] gameObjs)
        {
            foreach (GameObject obj in gameObjs)
                renderQueue -= obj.draw;
        }

        public void addObjectToRenderZSorted(GameEntity gameObj)
        {
            zSortedObjects.Add(gameObj);
        }

        public void removeObjectToRenderZSorted(GameEntity gameObj)
        {
            zSortedObjects.Remove(gameObj);
        }

        public void addObjectsToRenderZSorted(List<GameEntity> gameObjs)
        {
            zSortedObjects.AddRange(gameObjs);
        }

        #endregion
    }
}
