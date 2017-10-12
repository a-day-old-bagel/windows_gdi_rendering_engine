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
    class GameObject : GameEntity
    {
        #region Fields

        protected Vector3D position;
        protected Vector3D velocity;
        protected Vector3D scale;
        protected Vector3D rotation;
        protected MeshGDI mesh;

        #endregion

        #region Properties

        public override Vector3D Position { get { return position; } set { position = value; } }
        public Vector3D Velocity { get { return velocity; } set { velocity = value; } }
        public Vector3D Scale { get { return scale; } set { scale = value; } }
        public Vector3D Rotation { get { return rotation; } set { rotation = value; } }

        public override float Radius { get; set; }
        public override float renderDepthZ { get { return mesh.DistanceFromCamera; } }

        #endregion

        #region Constructor

        public GameObject
            (double posX, double posY, double posZ,
             double velX, double velY, double velZ,                          
             double rotX, double rotY, double rotZ,
             double sclX, double sclY, double sclZ,
             MeshGDI mesh, int radius)
            {
                position = new Vector3D(posX, posY, posZ);
                velocity = new Vector3D(velX, velY, velZ);
                scale    = new Vector3D(sclX, sclY, sclZ);
                rotation = new Vector3D(rotX, rotY, rotZ);
                this.mesh = mesh;
                Radius = radius;
            }

        #endregion

        #region Death Methods

        public override void Die()
        {

        }

        #endregion

        #region Graphics Engine Methods

        public override void preDraw(Graphics graphics, Camera camera, WorldLights worldLights)
        {
            mesh.preDraw(graphics, camera, worldLights, rotation, scale, position);
        }

        public override void draw(Graphics graphics, Camera camera, WorldLights worldLights)
        {
            mesh.draw(graphics);
        }

        #endregion

        #region Physics Engine Methods

        public override void move(float timeStep)
        {
            if (timeStep != 0)
            {
                Position.X += Velocity.X * timeStep;
                Position.Y += Velocity.Y * timeStep;
                Position.Z += Velocity.Z * timeStep;

                Rotation.X += Math.PI * .0005F * timeStep;          /// Momentary kludges for testing rotation...
                Rotation.Z += Math.PI * .00025F  * timeStep;
            }
        }

        #endregion
    }
}
