/***********************************************************************************
 * Galen Cochrane and Nathan Bitikofer 12 DEC 2014
 * GameHub.cs
 * 
 * GameHub is the administrative center of the game.  All paths lead here.
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Asteroids3D
{
    class GameHub
    {
        /// <summary>
        /// GameHub's fields hold all of the engines, cameras, objects, and other entities used in the game
        /// </summary>
        #region Fields

        GraphicsEngineGDI graphics;
        PhysicsEngine physics;        
        Camera camera;
        WorldLights lights;
        Origin origin;

        GameEntity[] testObjs;      // These are for testing purposes only

        ToggleKeyRepeatPreventer toggleKeyRepeatPreventer;

        #endregion

        /// <summary>
        /// For now, the whole game is initialized right here in the construcor.  All it needs is a GDI graphics object.
        /// </summary>
        #region Constructor

        public GameHub(Graphics g)
        {
            /// New gadget I'm trying out to prevent repeat toggle key presses for pause and debug mode without causing pauses in engines.
            toggleKeyRepeatPreventer = new ToggleKeyRepeatPreventer();
            toggleKeyRepeatPreventer.registerNewToggleKey('p');
            toggleKeyRepeatPreventer.registerNewToggleKey('b');

            Utilities.findScreenEdges();

            System.Windows.Forms.Cursor.Hide();

            /// Makes the games camera. Use an FPSCamera for walking or a FreeCamera for space ship like controls
            camera = new FPSCamera(new Vector4D(500, 20, -60, 1));

            /// The WorldLights struct holds all of the lights for the game world.  It is given to the graphics engine.
            lights = new WorldLights();
            lights.AmbientLights = new AmbientLight[] { new AmbientLight(10, 10, 10) };
            lights.DirectionalLights = new DirectionalLight[] { /*new DirectionalLight(new Vector3D(0, 1, 0), 0, 100, 0)*/ };
            lights.PointLights = new PointLight[] { new PointLight(new Vector3D(100, 100, -200), 60000, 100, 170, 240, camera),
                                                 new PointLight(new Vector3D(500, -100, 100), 30000, 200,  90, 210, camera) };

            origin = new Origin(50);

            graphics = new GraphicsEngineGDI(16, g, camera, lights);
            physics = new PhysicsEngine(16);

            graphics.Initialize();
            physics.Initialize();

            testObjs = new GameEntity[128];
            for (int i = 0; i < 128; i++)
                testObjs[i] = new GameObject                // In a GameObject call, we have...
                   (i * 4 + 8, (i * 20) % 100, 8,       // Position     x, y, z,
                     0 , 0, 0,                          // Velocity     x, y, z,
                     0 , 0, 0,                          // Rotation     x, y, z,
                     10, 10, 10,                        // Scale        x, y, z,
                     GameShapes.UnitCubeGDI(), 10);     // Mesh, Radius

            /// Put our test cubes into the graphics engine for depth-sorted rendering
            graphics.addObjectsToRenderZSorted(testObjs.ToList());
            graphics.addObjectToRenderZSorted(origin);
            graphics.addObjectToRenderZSorted(lights.PointLights[0]);
            graphics.addObjectToRenderZSorted(lights.PointLights[1]);

            /// User input is processed by the physics engine right now.
            physics.addControlRoutine(ParseInput);

            /// Put our test cubes into the physics engine.
            physics.addObjectsToMove(testObjs);
        }

        #endregion

        /// <summary>
        /// Here is where key presses, mouse presses or mouse movement is recorded, and control methods called.
        /// </summary>
        #region Control Fetching

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);

        private enum myKeys
        {
            up = 0x26,
            down = 0x28,
            left = 0x25,
            right = 0x27,
            w = 0x57,
            a = 0x41,
            s = 0x53,
            d = 0x44,
            space = 0x20,
            enter = 0x0D,
            b = 0x42,
            h = 0x48,
            p = 0x50,
            lClick = 0x01,
            rClick = 0x02,
            mClick = 0x03,
            sClick = 0x05,
            escape = 0x1B,
            ctrl = 0x11
        }

        private void ParseInput()
        {
            #region Get Mouse Movement

            mouse_HorizontalMove(Utilities.screenCenter.X - System.Windows.Forms.Cursor.Position.X);
            mouse_VerticalMove(Utilities.screenCenter.Y - System.Windows.Forms.Cursor.Position.Y);
            System.Windows.Forms.Cursor.Position = Utilities.screenCenter;

            #endregion

            #region Get Key Presses

            if (GetAsyncKeyState((int)myKeys.escape) != 0)  key_ESC();
            if (GetAsyncKeyState((int)myKeys.up) != 0)      key_upArrow();
            if (GetAsyncKeyState((int)myKeys.down) != 0)    key_downArrow();
            if (GetAsyncKeyState((int)myKeys.right) != 0)   key_rightArrow();
            if (GetAsyncKeyState((int)myKeys.left) != 0)    key_leftArrow();
            if (GetAsyncKeyState((int)myKeys.w) != 0)       key_W();
            if (GetAsyncKeyState((int)myKeys.a) != 0)       key_A();
            if (GetAsyncKeyState((int)myKeys.s) != 0)       key_S();
            if (GetAsyncKeyState((int)myKeys.d) != 0)       key_D();
            if (GetAsyncKeyState((int)myKeys.h) != 0)       key_H();
            if (GetAsyncKeyState((int)myKeys.p) != 0)       key_P();
            if (GetAsyncKeyState((int)myKeys.b) != 0)       key_B();

            #endregion

            #region Advance key repeat counters

            toggleKeyRepeatPreventer.advanceCounters();

            #endregion
        }

        #endregion

        /// <summary>
        /// Here is what happens for each different key or mouse input.  Easily modify controls from here.
        /// </summary>
        #region Input Functions

        #region Mouse Functions

        /// <summary>
        /// This happens when the mouse moves vertically
        /// </summary>
        /// <param name="y">negative or positive value, for distance moved up or down</param>
        private void mouse_VerticalMove(int y)
        {
            camera.pitch(y * .001F);
        }

        /// <summary>
        /// This happens when the mouse moves horizontally
        /// </summary>
        /// <param name="y">negative or positive value, for distance moved left or right</param>
        private void mouse_HorizontalMove(int x)
        {
            camera.yaw(x * .001F);
        }

        #endregion

        #region Key Functions

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_W()
        {
            camera.forward(1F);
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_S()
        {
            camera.forward(-1F);    // Negative parameter means opposite direction.
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_A()
        {
            camera.roll(1F);    // calls "strafe" if using an FPS camera
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_D()
        {
            camera.roll(-1F);   // calls "strafe" if using an FPS camera // Negative parameter means opposite direction.
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_B()
        {
            toggleKeyRepeatPreventer.pressAttempt('b', toggleDebugMode);
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_H()
        {
            // Reset's camera's orientation, but not its position. Default orientation is looking along -Z with -Y as up.
            camera.reset();
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_P()
        {
            toggleKeyRepeatPreventer.pressAttempt('p', PauseGame);
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_upArrow()
        {
            lights.PointLights[0].Position.Z += 1;
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_downArrow()
        {
            lights.PointLights[0].Position.Z -= 1;
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_leftArrow()
        {
            lights.PointLights[0].Position.X -= 1;
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_rightArrow()
        {
            lights.PointLights[0].Position.X += 1;
        }

        /// <summary>
        /// This happens when the W button is pressed
        /// </summary>
        private void key_ESC()
        {
            ExitGame();
        }

        #endregion

        #region Control Methods

        private void PauseGame()        // Toggles between paused and unpaused.
        {
            if (!graphics.Paused)
            {
                graphics.Pause();
                physics.Pause();
            }
            else
            {
                graphics.Resume();
                physics.Resume();
            }
        }

        private void ToggleBool(ref bool boolToSwitch)
        {
            if (boolToSwitch)
                boolToSwitch = false;
            else
                boolToSwitch = true;
        }

        private void toggleDebugMode()
        {
            ToggleBool(ref Settings.debug);
        }

        private void ExitGame()         // Exits the game safely
        {
            graphics.Terminate();
            physics.Terminate();
            Thread.Sleep(100);      // give time for engines to finish current loop, otherwise exceptions.
            Application.Exit();
        }

        #endregion

        #endregion
    }
}
