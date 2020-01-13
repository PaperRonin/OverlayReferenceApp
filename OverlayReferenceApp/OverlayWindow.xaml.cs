using System;
using System.Windows;
using System.Windows.Input;

namespace OverlayReferenceApp
{
    /// <summary>
    /// Логика взаимодействия для OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        private Point mouseDownLocation;

        public OverlayWindow()
        {
            InitializeComponent();
        }

        public OverlayWindow(string filePath)
        {
            InitializeComponent();

            Picture.Creation.SetImgFromFile(ImageViewer, filePath, this);
        }

        private void ImageViewer_LBMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownLocation = Mouse.GetPosition(this);
            ImageViewer.CaptureMouse();
        }
        private void ImageViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Picture.Movement.MoveTo(mouseDownLocation, Mouse.GetPosition(this), ImageViewer, new Point(canvas.RenderSize.Width / 2, canvas.RenderSize.Height / 2));
                mouseDownLocation = Mouse.GetPosition(this);
            }
        }

        private void ImageViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageViewer.ReleaseMouseCapture();
        }
    }
}
