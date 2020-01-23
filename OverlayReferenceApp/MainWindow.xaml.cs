using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Win32;

namespace OverlayReferenceApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Hotkey handling
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;

        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_CAPITAL); //CTRL + CAPS_LOCK
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_CAPITAL)
                            {
                                HideSwitch();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        #endregion

        #region Main programm
        private List<OverlayWindow> windowList;
        private bool hideSwitch = true;

        public MainWindow()
        {
            InitializeComponent();
            windowList = new List<OverlayWindow>();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";

            OpenFileDialog selectedFile = new OpenFileDialog
            {
                Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.JPEG;*.PNG"
            };

            if ((bool)selectedFile.ShowDialog())
            {
                fileName = selectedFile.FileName;
            }
            if (fileName != "")
            {
                CreateNewOverlay(fileName);
            }
        }

        public void CreateNewOverlay(string fileName)
        {
            OverlayWindow f = new OverlayWindow(fileName, this);
            windowList.Add(f);
            f.Show();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            HideSwitch();
        }

        private void HideSwitch()
        {
            switch (hideSwitch)
            {
                case true:
                    foreach (OverlayWindow window in windowList)
                    {
                        window.Hide();
                    }
                    hideSwitch = false;
                    break;

                case false:
                    foreach (OverlayWindow window in windowList)
                    {
                        window.Show();
                    }
                    hideSwitch = true;
                    break;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DataRepository.ClearPreset();
            DataRepository.Save(windowList);
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            CloseAllOverlays();
            DataRepository.Load(this);
        }

        public void RemoveFromWindowList(OverlayWindow window)
        {
            windowList.Remove(window);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            CloseAllOverlays();
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }

        private void CloseAllOverlays()
        {
            while (windowList.Count > 0)
            {
                windowList[windowList.Count - 1].Close();
            }
        }
    }
    #endregion
}