using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace OverlayReferenceApp
{
    public class DataRepository
    {
        public static void ClearPreset()
        {
            int count = 0;
            while (File.Exists(@"..\SavedPreset\" + count + ".png"))
            {
                File.Delete(@"..\SavedPreset\" + count++ + ".png");
            }
        }
        public static void Save(List<OverlayWindow> windowList)
        {
            int count = 0;
            foreach (OverlayWindow window in windowList)
            {
                BitmapImage img = (BitmapImage)window.ImageViewer.Source;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));

                Directory.CreateDirectory(@"..\SavedPreset\");

                FileStream fs = File.Open(@"..\SavedPreset\" + count++ + ".png", FileMode.Create);
                encoder.Save(fs);
                fs.Close();

            }
        }

        public static void Load(MainWindow parentWindow)
        {
            int count = 0;
            while (File.Exists(@"..\SavedPreset\" + count + ".png"))
            {
                parentWindow.CreateNewOverlay(new FileInfo(@"..\SavedPreset\" + count++ + ".png").FullName);
            }
        }

    }
}
