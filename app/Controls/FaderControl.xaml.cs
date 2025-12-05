using System.Windows;

namespace MidiSurface.Controls
{
    public partial class FaderControl : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(FaderControl),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(FaderControl), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(FaderControl), new PropertyMetadata(127.0));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(FaderControl), new PropertyMetadata("Fader"));

        public static readonly DependencyProperty ChannelProperty =
            DependencyProperty.Register(nameof(Channel), typeof(int), typeof(FaderControl), new PropertyMetadata(1));

        public static readonly DependencyProperty CCNumberProperty =
            DependencyProperty.Register(nameof(CCNumber), typeof(int), typeof(FaderControl), new PropertyMetadata(11));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public int Channel
        {
            get => (int)GetValue(ChannelProperty);
            set => SetValue(ChannelProperty, value);
        }

        public int CCNumber
        {
            get => (int)GetValue(CCNumberProperty);
            set => SetValue(CCNumberProperty, value);
        }


        public FaderControl()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FaderControl fader)
            {
                fader.MyVerticalSlider.Value = (double)e.NewValue;
            }
        }
    }
}