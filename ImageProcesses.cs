using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessing
{
    static class ImageProcesses
    {
        public static void BasicCopy(ref Bitmap loaded, ref Bitmap processed)
        {
            if (loaded == null) { return; }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }
        }

        public static void Grayscale(ref Bitmap loaded, ref Bitmap processed)
        {
            if (loaded == null) { return; }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;
                    processed.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            }
        }

        public static void ColorInversion(ref Bitmap loaded, ref Bitmap processed)
        {
            if (loaded == null) { return; }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
        }

        public static void Histogram(ref Bitmap loaded, ref Bitmap processed)
        {
            if (loaded == null) { return; }
            Bitmap loadedCopy = new Bitmap(loaded);
            Color pixel;
            byte average;
            int[] histogram = new int[256];

            for (int x = 0; x < loadedCopy.Width; x++)
            {
                for (int y = 0; y < loadedCopy.Height; y++)
                {
                    pixel = loadedCopy.GetPixel(x, y);
                    average = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    loadedCopy.SetPixel(x, y, Color.FromArgb(average, average, average));

                    pixel = loadedCopy.GetPixel(x, y);
                    histogram[pixel.R]++;
                }
            }

            processed = new Bitmap(256, 800);

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    processed.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histogram[x] / 5, processed.Height - 1); y++)
                {
                    processed.SetPixel(x, (processed.Height - 1) - y, Color.Black);
                }
            }
        }

        public static void Sepia(ref Bitmap loaded, ref Bitmap processed)
        {
            if (loaded == null) { return; }
            processed = new Bitmap(loaded.Width, loaded.Height);
            double tr, tg, tb;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    tr = 0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B;
                    tg = 0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B;
                    tb = 0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B;

                    processed.SetPixel(x, y,
                        Color.FromArgb(
                            Math.Min((int)tr, 255),
                            Math.Min((int)tg, 255),
                            Math.Min((int)tb, 255))
                        );
                }
            }
        }

        public static void Subtraction(ref Bitmap foreground, ref Bitmap background, ref Bitmap processed, int value)
        {
            if (foreground == null || background == null) { return; }

            processed = new Bitmap(foreground.Width, foreground.Height);
            Color green = Color.FromArgb(0, 255, 0);
            int graygreen = (green.R + green.G + green.B) / 3;
            int threshold = 10 * value;

            for (int x = 0; x < foreground.Width; x++)
            {
                for (int y = 0; y < foreground.Height; y++)
                {
                    Color forePixel = foreground.GetPixel(x, y);
                    Color backPixel = background.GetPixel(x, y);
                    int gray = (forePixel.R + forePixel.G + forePixel.B) / 3;
                    int difference = Math.Abs(gray - graygreen);

                    if (difference > threshold)
                    {
                        processed.SetPixel(x, y, forePixel);
                    }
                    else
                    {
                        processed.SetPixel(x, y, backPixel);
                    }
                }
            }
        }
    }
}
