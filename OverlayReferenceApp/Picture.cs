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
            private const double sizeUpScale = 1.1;
            private const double sizeDownScale = 0.9;

            public static void ScaleUp(Image img)
            {
                ScaleUp(img, sizeUpScale);
            }

            public static void ScaleUp(Image img, double scale)
            {
                if (scale > 1)
                {
                    Resize(img, scale);
                }
            }

            public static void ScaleDown(Image img, Point windowCenter)
            {
                ScaleDown(img, windowCenter, sizeDownScale);

            }
            public static void ScaleDown(Image img, Point windowCenter, double scale)
            {
                if (scale < 1)
                {
                    Point startingPoint = new Point(Canvas.GetLeft(img) + img.Width, Canvas.GetTop(img) + img.Height);

                    Resize(img, scale);

                    Point NewImgPos = new Point(Canvas.GetLeft(img), Canvas.GetTop(img));
                    Point OOBFlag = MovementBoundaries.IsOutOfBoundaries(img, windowCenter, NewImgPos);
                    MovementBoundaries.CorrectPosition(OOBFlag, startingPoint, img);
                }
            }

            private static int Resize(Image img, double scale)
            {
                try
                {
                    img.Width *= scale;
                    img.Height *= scale;
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            public static int Centralize(Window window, Image img)
            {
                try
                {
                    Size imageDelta = new Size((window.Width - img.Width) / 2, (window.Height - img.Height) / 2);
                    Canvas.SetLeft(img, imageDelta.Width);
                    Canvas.SetTop(img, imageDelta.Height);
                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        private static class MovementBoundaries
        {
            public static void CorrectPosition(Point OOBFlag, Point startingPoint, Image img)
            {
                if (OOBFlag.X != 0)
                    Canvas.SetLeft(img, startingPoint.X - img.Width);

                if (OOBFlag.Y != 0)
                    Canvas.SetTop(img, startingPoint.Y - img.Height);
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
                return flag;
            }

        }
    }
}
