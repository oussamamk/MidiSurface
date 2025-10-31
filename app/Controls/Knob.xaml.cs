using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MidiSurface.Controls
{
    public partial class Knob : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(Knob),
                new PropertyMetadata(0.0, OnValueChanged));

        public double Minimum { get; set; } = 0;
        public double Maximum { get; set; } = 127;

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private Point _lastPos;

        public Knob()
        {
            InitializeComponent();
            MouseDown += (s, e) => _lastPos = e.GetPosition(this);
            MouseMove += Knob_MouseMove;
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(this);
                double delta = _lastPos.Y - pos.Y;
                _lastPos = pos;
                Value = Math.Max(Minimum, Math.Min(Maximum, Value + delta));
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var k = (Knob)d;
            double angle = 270 * ((k.Value - k.Minimum) / (k.Maximum - k.Minimum)) - 135;
            k.Rotation.Angle = angle;
        }
    }
}
