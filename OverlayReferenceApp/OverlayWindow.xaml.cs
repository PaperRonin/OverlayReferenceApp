using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public OverlayWindow(string fileName)
        {
            InitializeComponent();

            BitmapImage bitmapImg = new BitmapImage();
            bitmapImg.BeginInit();
            bitmapImg.UriSource = new Uri(fileName);
            bitmapImg.EndInit();

            ImageViewer.Source = bitmapImg;
            ImageViewer.Height = bitmapImg.Height;
            ImageViewer.Width = bitmapImg.Width;

            double proportions = ImageViewer.Width / ImageViewer.Height;
            if (proportions > 0)
            {
                this.Width = (int)Math.Round(proportions * this.Height);
                ImageViewer.Width = Math.Round(this.Height * proportions);
                ImageViewer.Height = Height;
            }
            else
            {
                proportions = ImageViewer.Height / ImageViewer.Width;
                this.Height = (int)Math.Round(proportions * this.Width);
                ImageViewer.Width = Math.Round(this.Width * proportions);
                ImageViewer.Height = Width;
            }
            ImageViewer.Margin = new Thickness((Width - ImageViewer.Width) / 2, (Height - ImageViewer.Height) / 2, 0, 0);
        }
    }
}
