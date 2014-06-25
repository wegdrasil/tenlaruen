using System;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tenlaruen
{
    class ImageMNIST
    {
        public byte[][] pixels;
        public byte label;
        public BitmapSource image;
        public double[] netInput;

        public ImageMNIST(byte[][] pixels, byte label)
        {
            this.pixels = new byte[28][];
            for(int i = 0; i < this.pixels.Length; i++)
                this.pixels[i] = new byte[28];

            for (int i = 0; i < 28; ++i)
                for (int j = 0; j < 28; ++j)
                    this.pixels[i][j] = pixels[i][j];

            this.label = label;

            byte[] imageData = new byte[28 * 28];
            netInput = new double[784];

            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    imageData[(i * 28) + j] = pixels[i][j];
                    if (pixels[i][j] >= 128)
                        netInput[(i * 28) + j] = 1.0f;
                    else
                        netInput[(i * 28) + j] = 0.0f;
                }
            }

            var width = 28;
            var height = 28;
            var dpiX = 96d;
            var dpiY = 96d;
            var pixelFormat = PixelFormats.Gray8;
            var bytesPerPixel = 1;
            var stride = bytesPerPixel * width;

            image = BitmapSource.Create(width, height, dpiX, dpiY, pixelFormat, null, imageData, stride);

        }

        public BitmapSource GetImage()
        {
            return image;
        }

        public double[] GetNetInput()
        {
            return netInput;
        }



    }
}
