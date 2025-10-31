using System.Windows;
using System.Windows.Controls;

namespace MidiSurface.Controls
{
    public partial class ChannelStrip : UserControl
    {
        // === Channel Name ===
        public string ChannelName
        {
            get => (string)GetValue(ChannelNameProperty);
            set => SetValue(ChannelNameProperty, value);
        }
        public static readonly DependencyProperty ChannelNameProperty =
            DependencyProperty.Register(
                nameof(ChannelName),
                typeof(string),
                typeof(ChannelStrip),
                new PropertyMetadata("CH 1", OnChannelNameChanged));

        private static void OnChannelNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ChannelStrip strip && e.NewValue is string name)
                strip.ChannelNameTextBlock.Text = name;
        }

        // === Knob Value ===
        public double KnobValue
        {
            get => (double)GetValue(KnobValueProperty);
            set => SetValue(KnobValueProperty, value);
        }
        public static readonly DependencyProperty KnobValueProperty =
            DependencyProperty.Register(
                nameof(KnobValue),
                typeof(double),
                typeof(ChannelStrip),
                new PropertyMetadata(63.0, OnKnobValueChanged));

        private static void OnKnobValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ChannelStrip strip && e.NewValue is double value)
                strip.KnobControl.Value = value;
        }

        // === Knob Label ===
        public string KnobLabelText
        {
            get => (string)GetValue(KnobLabelTextProperty);
            set => SetValue(KnobLabelTextProperty, value);
        }
        public static readonly DependencyProperty KnobLabelTextProperty =
            DependencyProperty.Register(
                nameof(KnobLabelText),
                typeof(string),
                typeof(ChannelStrip),
                new PropertyMetadata("Pan", OnKnobLabelChanged));

        private static void OnKnobLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ChannelStrip strip && e.NewValue is string text)
                strip.KnobLabel.Text = text;
        }

        // === Fader Value ===
        public double FaderValue
        {
            get => (double)GetValue(FaderValueProperty);
            set => SetValue(FaderValueProperty, value);
        }
        public static readonly DependencyProperty FaderValueProperty =
            DependencyProperty.Register(
                nameof(FaderValue),
                typeof(double),
                typeof(ChannelStrip),
                new PropertyMetadata(20.0, OnFaderValueChanged));

        private static void OnFaderValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ChannelStrip strip && e.NewValue is double value)
                strip.FaderControl.Value = value;
        }

        // === Fader Label ===
        public string FaderLabelText
        {
            get => (string)GetValue(FaderLabelTextProperty);
            set => SetValue(FaderLabelTextProperty, value);
        }
        public static readonly DependencyProperty FaderLabelTextProperty =
            DependencyProperty.Register(
                nameof(FaderLabelText),
                typeof(string),
                typeof(ChannelStrip),
                new PropertyMetadata("Volume", OnFaderLabelChanged));

        private static void OnFaderLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ChannelStrip strip && e.NewValue is string text)
                strip.FaderLabel.Text = text;
        }

        // === Constructor ===
        public ChannelStrip()
        {
            InitializeComponent();
        }

        // === Button Click Events (for external handling) ===
        public event RoutedEventHandler Button1Click
        {
            add => Button1.Click += value;
            remove => Button1.Click -= value;
        }

        public event RoutedEventHandler Button2Click
        {
            add => Button2.Click += value;
            remove => Button2.Click -= value;
        }

        public event RoutedEventHandler Button3Click
        {
            add => Button3.Click += value;
            remove => Button3.Click -= value;
        }

        public event RoutedEventHandler Button4Click
        {
            add => Button4.Click += value;
            remove => Button4.Click -= value;
        }
    }
}