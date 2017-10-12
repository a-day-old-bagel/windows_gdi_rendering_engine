using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Asteroids3D
{
    class AwesomeSauceCanvas
    {
        Bitmap visible;
        Bitmap backBuffer;
        System.IntPtr visibleStartPtr;
        System.IntPtr backBufferStartPtr;

        int width;
        int height;
        byte[] pixelData;

        public AwesomeSauceCanvas(Bitmap visibleBMP)
        {
            visible = visibleBMP;
            width = visibleBMP.Width;
            height = visibleBMP.Height;
            backBuffer = new Bitmap(width, height);
            pixelData = new byte[width * height * 3];

            BitmapData imgData = visible.LockBits(new Rectangle(0, 0, width, height),
                                                ImageLockMode.ReadWrite,
                                                PixelFormat.Format24bppRgb);
            visibleStartPtr = imgData.Scan0;
            imgData = backBuffer.LockBits(new Rectangle(0, 0, width, height),
                                                ImageLockMode.ReadWrite,
                                                PixelFormat.Format24bppRgb);
            backBufferStartPtr = imgData.Scan0;
        }
    }
}
