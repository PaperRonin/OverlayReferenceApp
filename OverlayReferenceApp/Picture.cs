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

            public static int SetImgFromFile(Image img, string filePath, Window window)
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
                    ResizeWindow(img, window);//Gets the image parent window
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
                CentralizeImg(img);
            }

            private static void CentralizeImg(Image img)
            {
                 double widthDelta = -8;// dragons for picture centralizing
                 double heightDelta = -19.5;
                img.Margin = new Thickness(widthDelta, heightDelta, 0, 0);
            }
            private static void MinimiseImg(Image img, double shortestSize, double proportions)
            {
                img.Width = shortestSize * proportions;
                img.Height = shortestSize;
            }


        }

        public static class Movement
        {
            public static int MoveTo(Point startingPoint, Point currentPoint, Image img, Point windowCenter)
            {

                Point upperLeftCornerDelta = new Point(currentPoint.X + img.Margin.Left - startingPoint.X,
                    currentPoint.Y + img.Margin.Top - startingPoint.Y);

                Point lowerRightCornerDelta = new Point(currentPoint.X + img.Margin.Left + img.Width - startingPoint.X,
                    currentPoint.Y + img.Margin.Top + img.Height - startingPoint.Y);


                if (upperLeftCornerDelta.X > windowCenter.X || upperLeftCornerDelta.X < -windowCenter.X * 2) //here be dragons
                {
                    upperLeftCornerDelta.X = img.Margin.Left;
                }
                if (upperLeftCornerDelta.Y > windowCenter.Y || upperLeftCornerDelta.Y < -windowCenter.Y * 2) //here be dragons
                {
                    upperLeftCornerDelta.Y = img.Margin.Top;
                }

                img.Margin = new Thickness(upperLeftCornerDelta.X, upperLeftCornerDelta.Y, 0, 0);
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
