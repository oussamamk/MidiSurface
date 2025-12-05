using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MidiSurface.ViewModels;

namespace MidiSurface.Controls
{
    public partial class DeviceStripRow : UserControl
    {
        public static readonly DependencyProperty StripsProperty =
            DependencyProperty.Register(
                nameof(Strips),
                typeof(IEnumerable<ChannelStripViewModel>),
                typeof(DeviceStripRow),
                new PropertyMetadata(null, OnStripsChanged));

        public static readonly DependencyProperty GlobalButtonsProperty =
            DependencyProperty.Register(
                nameof(GlobalButtons),
                typeof(IEnumerable<ButtonViewModel>),
                typeof(DeviceStripRow),
                new PropertyMetadata(null, OnGlobalButtonsChanged));

        public IEnumerable<ChannelStripViewModel> Strips
        {
            get => (IEnumerable<ChannelStripViewModel>)GetValue(StripsProperty);
            set => SetValue(StripsProperty, value);
        }

        public IEnumerable<ButtonViewModel> GlobalButtons
        {
            get => (IEnumerable<ButtonViewModel>)GetValue(GlobalButtonsProperty);
            set => SetValue(GlobalButtonsProperty, value);
        }

        public DeviceStripRow()
        {
            InitializeComponent();
        }

        private static void OnStripsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DeviceStripRow control)
            {
                control.RebuildLayout(); // ✅ Renamed
            }
        }

        private static void OnGlobalButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DeviceStripRow control)
            {
                control.RebuildLayout(); // ✅ Renamed
            }
        }

        // ✅ Renamed to avoid hiding UIElement.UpdateLayout()
        private void RebuildLayout()
        {
            StripGrid.Children.Clear();
            StripGrid.RowDefinitions.Clear();
            StripGrid.ColumnDefinitions.Clear();

            var strips = Strips?.ToList();
            if (strips == null || !strips.Any()) return;

            var globalButtons = GlobalButtons; // Capture once
            bool hasGlobalButtons = globalButtons?.Count() == 6;

            // Add column for global buttons (if needed)
            if (hasGlobalButtons)
            {
                StripGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            // Add star-sized columns for strips
            foreach (var _ in strips)
            {
                StripGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Add global buttons
            if (hasGlobalButtons)
            {
                // ✅ Null-safe: we already confirmed GlobalButtons != null and has 6 items
                var buttonsList = GlobalButtons.Take(6).ToList(); // Safe now
                var globalControl = new GlobalButtonsControl
                {
                    DataContext = new { Buttons = buttonsList },
                    Margin = new Thickness(5, 0, 5, 0)
                };
                Grid.SetColumn(globalControl, 0);
                StripGrid.Children.Add(globalControl);
            }

            // Add strip controls
            int colIndex = hasGlobalButtons ? 1 : 0;
            for (int i = 0; i < strips.Count; i++)
            {
                var stripControl = new ChannelStripControl
                {
                    DataContext = strips[i]
                };
                Grid.SetColumn(stripControl, colIndex + i);
                StripGrid.Children.Add(stripControl);
            }
        }
    }
}