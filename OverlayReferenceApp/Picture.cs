using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using System.Windows.Media;

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
                try
                {
                    Point NewImgPos = new Point(currentPoint.X + Canvas.GetLeft(img) - startingPoint.X,
                        currentPoint.Y + Canvas.GetTop(img) - startingPoint.Y);

                    Point OOBFlag = MovementBoundaries.IsOutOfBoundaries(img, windowCenter, NewImgPos);

                    NewImgPos.X = OOBFlag.X != 0 ? Canvas.GetLeft(img) : NewImgPos.X;
                    NewImgPos.Y = OOBFlag.Y != 0 ? Canvas.GetTop(img) : NewImgPos.Y;

                    Canvas.SetLeft(img, NewImgPos.X);
                    Canvas.SetTop(img, NewImgPos.Y);

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public static class Resizing
        {
            public static int ScaleUp(Image img)
            {
                img.Width *= 1.1;
                img.Height *= 1.1;
                return 0;
            }

            public static int ScaleDown(Image img)
            {

                img.Width *= 0.9;
                img.Height *= 0.9;
                return 0;
            }
        }

        private static class MovementBoundaries
        {
            public static void CorrectPosition()
            {
            }

            public static Point IsOutOfBoundaries(Image img, Point windowCenter, Point newPos)
            {
                Point flag = new Point(0, 0)
                {
                    X = (newPos.X > windowCenter.X) ? -1 : 0,
                    Y = (newPos.Y > windowCenter.Y) ? -1 : 0
                };
                flag.X = (newPos.X < windowCenter.X - img.Width) ? 1 : flag.X;
                
                flag.Y = (newPos.Y < windowCenter.Y - img.Height) ? 1 : flag.Y;
                Console.WriteLine(flag);
                return flag;
            }

        }
    }
}
