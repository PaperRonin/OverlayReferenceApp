using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using System.Windows;

namespace OverlayReferenceApp
{
    public static class Picture
    {

        public static class Creation
        {

            public static int SetImgFromFile(Image img, string filePath)
            {
                try
                {
                    BitmapImage bitmapImg = new BitmapImage();
                    bitmapImg.BeginInit();
                    bitmapImg.UriSource = new Uri(filePath);
                    bitmapImg.EndInit();

                    img.Source = bitmapImg;
                    img.Height = bitmapImg.Height;
                    img.Width = bitmapImg.Width;
                    ResizeWindow(img, (Window)img.Parent);//Gets the image parent window
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            private static void ResizeWindow(Image img, Window window)
            {

                double proportions = img.Width / img.Height;

                if (proportions > 0)
                {
                    window.Width = proportions * window.Height;
                    MinimiseImg(img, window.Height, proportions);
                }
                else
                {
                    proportions = img.Height / img.Width;
                    window.Height = proportions * window.Width;
                    MinimiseImg(img, window.Width, proportions);
                }
                CentralizeImg(img, window);
            }

            private static void CentralizeImg(Image img, Window window) =>
                img.Margin = new Thickness((window.Width - img.Width) / 2, (window.Height - img.Height) / 2, 0, 0);

            private static void MinimiseImg(Image img, double shortestSize, double proportions)
            {
                img.Width = shortestSize * proportions;
                img.Height = shortestSize;
            }


        }

        public static class Movement
        {

            public static int MoveTo()
            {

                return 0;
            }

        }

        public static class Resizing
        {
            public static int ScaleUp()
            {

                return 0;
            }

            public static int ScaleDown()
            {

                return 0;
            }
        }

        private static class MovementBoundaries
        {
            public static void CorrectPosition()
            {
            }

            private static bool IsOutOfBoundaries()
            {
                return false;
            }

        }
    }
}
