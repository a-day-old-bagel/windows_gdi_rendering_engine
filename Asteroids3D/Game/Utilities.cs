/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids3D
{
    class Utilities
    {
        public static Rectangle screenBounds;
        public static Point screenCenter;
        public static void findScreenEdges()
        {
            screenBounds = Screen.PrimaryScreen.Bounds;
            screenCenter = new Point(Utilities.screenBounds.Width / 2, Utilities.screenBounds.Height / 2);
        }

        public static float InverseSquareRoot(float x)
        {
            unsafe
            {
                float xhalf = 0.5f * x;
                int i = *(int*)&x;              // get bits for floating value
                i = 0x5f375a86 - (i >> 1);      // gives initial guess y0
                x = *(float*)&i;                // convert bits back to float
                x = x * (1.5f - xhalf * x * x); // Newton step, repeating increases accuracy
                return x;
            }
        }
    }
}
