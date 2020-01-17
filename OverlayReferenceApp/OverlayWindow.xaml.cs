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
        private Size previousWindowSize;
        private MainWindow menu;

        public OverlayWindow()
        {
            InitializeComponent();
        }

        public OverlayWindow(string filePath, MainWindow menu)
        {
            InitializeComponent();
            this.menu = menu;
            previousWindowSize = new Size(Width, Height);
            Picture.Creation.SetImgFromFile(ImageViewer, filePath, this);
        }

        private void Window_LBMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownLocation = Mouse.GetPosition(this);
            this.CaptureMouse();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point windowCenter = new Point(canvas.RenderSize.Width / 2, canvas.RenderSize.Height / 2);
                Picture.Movement.MoveTo(mouseDownLocation, Mouse.GetPosition(this), ImageViewer, windowCenter);
                mouseDownLocation = Mouse.GetPosition(this);
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            switch (e.Delta)
            {
                case 120:
                    Picture.Resizing.ScaleUp(ImageViewer);
                    break;

                case -120:
                    Point windowCenter = new Point(canvas.RenderSize.Width / 2, canvas.RenderSize.Height / 2);
                    Picture.Resizing.ScaleDown(ImageViewer, windowCenter);
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            menu.RemoveFromWindowList(this);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Picture.Resizing.Centralize(this, ImageViewer);
        }
    }
}
