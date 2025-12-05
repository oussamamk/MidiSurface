using MidiSurface.ViewModels;
using System.Windows;
using System.Windows.Forms;           // For Screen
using System.Drawing;                 // For Rectangle
using System.Windows.Interop;         // For WindowInteropHelper

namespace MidiSurface
{
    public partial class MainWindow : Window
    {
        private MidiMessageRouter _router1;
        private MidiMessageRouter _router2;
        private MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            ApplySavedWindowSettings();

            _vm = new MainViewModel();
            DataContext = _vm;

            _router1 = new MidiMessageRouter(_vm.Devices[0]);
            _router1.StartListening("vtDev1");

            _router2 = new MidiMessageRouter(_vm.Devices[1]);
            _router2.StartListening("vtDev2");
        }

        private static Rectangle ParseRectangle(string rectString)
        {
            if (string.IsNullOrEmpty(rectString))
                return Rectangle.Empty;

            try
            {
                // Remove braces and split by commas
                string trimmed = rectString.Trim('{', '}');
                var parts = trimmed.Split(',');

                if (parts.Length != 4)
                    return Rectangle.Empty;

                // Extract values from "Key=Value" format
                int x = int.Parse(parts[0].Split('=')[1]);
                int y = int.Parse(parts[1].Split('=')[1]);
                int width = int.Parse(parts[2].Split('=')[1]);
                int height = int.Parse(parts[3].Split('=')[1]);

                return new Rectangle(x, y, width, height);
            }
            catch
            {
                return Rectangle.Empty;
            }
        }

        private void ApplySavedWindowSettings()
        {
            var settings = Properties.Settings.Default;

            // Step 1: Restore size
            if (settings.WindowSize.Width > 0 && settings.WindowSize.Height > 0)
            {
                this.Width = settings.WindowSize.Width;
                this.Height = settings.WindowSize.Height;
            }

            bool restoredToSavedScreen = false;
            if (!string.IsNullOrEmpty(settings.WindowScreenBounds))
            {
                try
                {
                    var savedBounds = ParseRectangle(settings.WindowScreenBounds);
                    if (savedBounds != Rectangle.Empty)
                    {
                        var savedScreen = Screen.AllScreens.FirstOrDefault(s => s.Bounds == savedBounds);
                        if (savedScreen != null)
                        {
                            this.WindowState = WindowState.Normal;

                            if (settings.WindowState == WindowState.Maximized)
                            {
                                // Place on saved screen
                                this.Left = savedScreen.WorkingArea.Left + 100;
                                this.Top = savedScreen.WorkingArea.Top + 100;
                            }
                            else
                            {
                                double left = Math.Max(savedScreen.WorkingArea.Left,
                                    Math.Min(settings.WindowLeft,
                                             savedScreen.WorkingArea.Right - this.Width));
                                double top = Math.Max(savedScreen.WorkingArea.Top,
                                    Math.Min(settings.WindowTop,
                                             savedScreen.WorkingArea.Bottom - this.Height));
                                this.Left = left;
                                this.Top = top;
                            }

                            restoredToSavedScreen = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error restoring window position: {e.Message}");
                }
            }

            // Fallback for normal state only
            if (!restoredToSavedScreen && settings.WindowState != WindowState.Maximized)
            {
                if (settings.WindowLeft >= 0 && settings.WindowTop >= 0)
                {
                    this.Left = settings.WindowLeft;
                    this.Top = settings.WindowTop;
                }
            }

            if (settings.WindowState == WindowState.Maximized && restoredToSavedScreen)
            {
                // Position is already set on correct screen — now maximize
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = settings.WindowState;
            }

            EnsureWindowIsVisibleOnScreen();
        }

        private void EnsureWindowIsVisibleOnScreen()
        {
            var virtualScreen = new Rect(
                SystemParameters.VirtualScreenLeft,
                SystemParameters.VirtualScreenTop,
                SystemParameters.VirtualScreenWidth,
                SystemParameters.VirtualScreenHeight
            );

            var windowRect = new Rect(this.Left, this.Top, this.Width, this.Height);

            // If completely off-screen → center on primary
            if (!virtualScreen.IntersectsWith(windowRect))
            {
                this.WindowState = WindowState.Normal;
                this.Left = (SystemParameters.PrimaryScreenWidth - this.ActualWidth) / 2;
                this.Top = (SystemParameters.PrimaryScreenHeight - this.ActualHeight) / 2;
                return;
            }

            // Ensure at least 100px is visible on each edge
            if (windowRect.Right < virtualScreen.Left + 100)
                this.Left = virtualScreen.Left;
            else if (windowRect.Left > virtualScreen.Right - 100)
                this.Left = virtualScreen.Right - this.Width;

            if (windowRect.Bottom < virtualScreen.Top + 100)
                this.Top = virtualScreen.Top;
            else if (windowRect.Top > virtualScreen.Bottom - 100)
                this.Top = virtualScreen.Bottom - this.Height;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var settings = Properties.Settings.Default;
            var currentScreen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

            // Save screen bounds
            settings.WindowScreenBounds = currentScreen.Bounds.ToString();

            if (this.WindowState == WindowState.Maximized)
            {
                // Save restore bounds
                settings.WindowSize = new System.Windows.Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
                settings.WindowLeft = this.RestoreBounds.Left;
                settings.WindowTop = this.RestoreBounds.Top;
                settings.WindowState = WindowState.Maximized;
            }
            else
            {
                // Save current size and position
                settings.WindowSize = new System.Windows.Size(this.ActualWidth, this.ActualHeight);
                settings.WindowLeft = this.Left;
                settings.WindowTop = this.Top;
                settings.WindowState = this.WindowState;
            }

            settings.Save();
            base.OnClosing(e);
        }
    }
}