using System.Windows;
using Microsoft.Win32;

namespace OverlayReferenceApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";

            OpenFileDialog selectedFile = new OpenFileDialog();
            selectedFile.Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.JPEG;*.PNG";

            if ((bool)selectedFile.ShowDialog())
            {
                fileName = selectedFile.FileName;
            }
            if (fileName != "") {
                OverlayWindow f = new OverlayWindow(fileName);
                f.Show();
            }
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
