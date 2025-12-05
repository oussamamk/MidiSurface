using System.Windows;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;

namespace MidiSurface.Controls
{
    public partial class KnobControl : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(KnobControl), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(KnobControl), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(KnobControl), new PropertyMetadata(127.0));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(KnobControl), new PropertyMetadata("Knob"));

        public static readonly DependencyProperty ChannelProperty =
            DependencyProperty.Register(nameof(Channel), typeof(int), typeof(KnobControl), new PropertyMetadata(1));

        public static readonly DependencyProperty CCNumberProperty =
            DependencyProperty.Register(nameof(CCNumber), typeof(int), typeof(KnobControl), new PropertyMetadata(11));

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

        private System.Windows.Point _lastPos;

        public KnobControl()
        {
            InitializeComponent();

            MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    _lastPos = e.GetPosition(this);
                    CaptureMouse();
                }
            };

            MouseUp += (s, e) =>
            {
                if (IsMouseCaptured)
                    ReleaseMouseCapture();
            };

            MouseMove += Knob_MouseMove;
        }

        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                var pos = e.GetPosition(this);
                double delta = _lastPos.Y - pos.Y; // vertical drag = value change
                //double delta = pos.X - _lastPos.X; // Horizontal drag = value change
                _lastPos = pos;

                double newValue = Value + delta; // sensitivity multiplier
                Value = Math.Clamp(newValue, Minimum, Maximum);

                // Optional: Raise event for MIDI send (if needed)
                // OnKnobValueChanged?.Invoke(this, Value);
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is KnobControl knob && e.NewValue is double newValue)
            {
                // Normalize to 0–1
                double range = knob.Maximum - knob.Minimum;
                if (range <= 0) return;

                double normalized = (newValue - knob.Minimum) / range;
                // Map to angle: +135° (min) → +45° (max) = 270° sweep
                double angle = (normalized * 270) - 135;

                knob.Rotation.Angle = angle;

                // OR with animation (uncomment if desired):
                //var anim = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(30))
                //{
                //    EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
                //};
                //knob.Rotation.BeginAnimation(RotateTransform.AngleProperty, anim);

            }
        }
    }
}