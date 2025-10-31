using System.Windows;
using System.Windows.Controls;

namespace MidiSurface.Controls
{
    public partial class Fader : UserControl
    {
        // Define Value as a DependencyProperty
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(Fader),
                new FrameworkPropertyMetadata(
                    0.0, // default value
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // Optional: Add Minimum/Maximum if you want to bind them too
        public double Minimum
        {
            get => MyVerticalSlider.Minimum;
            set => MyVerticalSlider.Minimum = value;
        }

        public double Maximum
        {
            get => MyVerticalSlider.Maximum;
            set => MyVerticalSlider.Maximum = value;
        }

        public Fader()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // This ensures the internal Slider updates when Value changes from outside
            if (d is Fader fader && fader.MyVerticalSlider != null)
            {
                fader.MyVerticalSlider.Value = (double)e.NewValue;
            }
        }
    }
}