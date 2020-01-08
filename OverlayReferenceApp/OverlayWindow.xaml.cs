using System.Windows;

namespace OverlayReferenceApp
{
    /// <summary>
    /// Логика взаимодействия для OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        public OverlayWindow()
        {
            InitializeComponent();
        }

        public OverlayWindow(string filePath)
        {
            InitializeComponent();

            Picture.Creation.SetImgFromFile(ImageViewer, filePath);

        }
    }
}
