using System.Windows;
using System.Windows.Controls;

namespace MidiSurface.Controls
{
    public partial class ButtonControl : UserControl
    {
        public static readonly DependencyProperty BtnConentProperty =
            DependencyProperty.Register(nameof(BtnContent), typeof(string), typeof(ButtonControl), new PropertyMetadata("Ctn"));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(ButtonControl), new PropertyMetadata("Btn"));

        public static readonly DependencyProperty IsLitProperty =
            DependencyProperty.Register(nameof(IsLit), typeof(bool), typeof(ButtonControl), new PropertyMetadata(false));

        public string BtnContent
        {
            get => (string)GetValue(BtnConentProperty);
            set => SetValue(BtnConentProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public bool IsLit
        {
            get => (bool)GetValue(IsLitProperty);
            set => SetValue(IsLitProperty, value);
        }

        public ButtonControl()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Optional: Toggle or send MIDI
            // For now, just update state (if it's a toggle)
            // IsLit = !IsLit;
        }
    }
}