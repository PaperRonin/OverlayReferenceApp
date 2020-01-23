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
        private MainWindow menu;
        private Picture picture;


        public OverlayWindow(string filePath, MainWindow menu)
        {
            InitializeComponent();
            this.menu = menu;
            picture = new Picture(ImageViewer, filePath, this);
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
                picture.MoveTo(mouseDownLocation, Mouse.GetPosition(this));
                mouseDownLocation = Mouse.GetPosition(this);
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            switch (e.Delta)
            {
                case 120:
                    picture.ScaleUp();
                    break;

                case -120:
                    picture.ScaleDown();
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            menu.RemoveFromWindowList(this);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            picture.Centralize();
        }
    }
}
