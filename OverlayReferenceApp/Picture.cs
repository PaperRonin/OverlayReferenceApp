using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using System.Windows.Media;

namespace OverlayReferenceApp
{
    public class Picture
    {
        private Image img;
        private OverlayWindow parentWindow;

        #region initialization
        public Picture(Image img, string filePath, OverlayWindow parentWindow)
        {
            this.img = img;
            this.parentWindow = parentWindow;

            BitmapImage bitmapImg = new BitmapImage();
            bitmapImg.BeginInit();
            bitmapImg.UriSource = new Uri(filePath);
            bitmapImg.EndInit();

            img.Source = bitmapImg;
            img.Height = bitmapImg.Height;
            img.Width = bitmapImg.Width;
            ResizeWindow();
        }

        private void ResizeWindow()
        {

            double proportions = img.Width / img.Height;

            if (proportions > 0)
            {
                parentWindow.Width = proportions * parentWindow.Height;
                MinimiseImg(parentWindow.Height, proportions);
            }
            else
            {
                proportions = img.Height / img.Width;
                parentWindow.Height = proportions * parentWindow.Width;
                MinimiseImg(parentWindow.Width, proportions);
            }

            const double widthDelta = -8;//correction for centralizing image in parentWindow properly
            const double heightDelta = -19.5;

            Canvas.SetLeft(img, widthDelta);
            Canvas.SetTop(img, heightDelta);
        }

        private void MinimiseImg(double shortestSize, double proportions)
        {
            img.Width = shortestSize * proportions;
            img.Height = shortestSize;
        }
        #endregion

        #region Movement
        public int MoveTo(Point startingPoint, Point currentPoint)
        {
            try
            {
                Point windowCenter = new Point(parentWindow.canvas.RenderSize.Width / 2,
                    parentWindow.canvas.RenderSize.Height / 2);

                Point NewImgPos = new Point(currentPoint.X + Canvas.GetLeft(img) - startingPoint.X,
                    currentPoint.Y + Canvas.GetTop(img) - startingPoint.Y);

                Point OOBFlag = IsOutOfBoundaries(windowCenter, NewImgPos);

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
        #endregion

        #region Resizing
        private const double sizeUpScale = 1.1;
        private const double sizeDownScale = 0.9;

        public void ScaleUp()
        {
            ScaleUp(sizeUpScale);
        }

        public void ScaleUp(double scale)
        {
            if (scale > 1)
            {
                Resize(scale);
            }
        }

        public void ScaleDown()
        {
            ScaleDown(sizeDownScale);

        }
        public void ScaleDown(double scale)
        {
            if (scale < 1)
            {
                Point windowCenter = new Point(parentWindow.canvas.RenderSize.Width / 2,
                   parentWindow.canvas.RenderSize.Height / 2);

                Point startingPoint = new Point(Canvas.GetLeft(img) + img.Width, Canvas.GetTop(img) + img.Height);

                Resize(scale);

                Point NewImgPos = new Point(Canvas.GetLeft(img), Canvas.GetTop(img));
                Point OOBFlag = IsOutOfBoundaries(windowCenter, NewImgPos);
                CorrectPosition(OOBFlag, startingPoint);
            }
        }

        private int Resize(double scale)
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

        public int Centralize()
        {
            try
            {
                Point imageDelta = new Point((parentWindow.Width - img.Width) / 2, (parentWindow.Height - img.Height) / 2);
                Canvas.SetLeft(img, imageDelta.X);
                Canvas.SetTop(img, imageDelta.Y);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region Movement boundaries 
        public void CorrectPosition(Point OOBFlag, Point startingPoint)
        {
            if (OOBFlag.X != 0)
                Canvas.SetLeft(img, startingPoint.X - img.Width);

            if (OOBFlag.Y != 0)
                Canvas.SetTop(img, startingPoint.Y - img.Height);
        }

        public Point IsOutOfBoundaries(Point windowCenter, Point newPos)
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

        #endregion
    }
}
