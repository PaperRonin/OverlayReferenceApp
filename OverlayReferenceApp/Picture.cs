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
                    ResizeWindow(img, window);
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
                const double widthDelta = -8;//correction for centralizing image in window properly
                const double heightDelta = -19.5;

                Canvas.SetLeft(img, widthDelta);
                Canvas.SetTop(img, heightDelta);
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
                Point upperLeftCornerDelta = new Point(currentPoint.X + Canvas.GetLeft(img) - startingPoint.X,
                    currentPoint.Y + Canvas.GetTop(img) - startingPoint.Y);

                Point OOBFlag = MovementBoundaries.IsOutOfBoundaries(startingPoint, currentPoint, img, windowCenter, upperLeftCornerDelta);

                upperLeftCornerDelta.X = OOBFlag.X != 0 ? Canvas.GetLeft(img) : upperLeftCornerDelta.X;
                upperLeftCornerDelta.Y = OOBFlag.Y != 0 ? Canvas.GetTop(img) : upperLeftCornerDelta.Y;

                Canvas.SetLeft(img, upperLeftCornerDelta.X);
                Canvas.SetTop(img, upperLeftCornerDelta.Y);

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

            public static Point IsOutOfBoundaries(Point startingPoint, Point currentPoint, Image img, Point windowCenter, Point delta)
            {
                Point flag = new Point(0, 0);


                flag.X = (delta.X > windowCenter.X) ? -1 : 0;
                flag.X = (delta.X < windowCenter.X - img.Width) ? 1 : flag.X;

                flag.Y = (delta.Y > windowCenter.Y) ? -1 : 0;
                flag.Y = (delta.Y < windowCenter.Y - img.Height) ? 1 : flag.Y;
                Console.WriteLine(flag);
                return flag;
            }

        }
    }
}
