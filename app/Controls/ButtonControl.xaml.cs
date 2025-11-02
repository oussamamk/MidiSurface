using System.Windows;
using System.Windows.Controls;

namespace MidiSurface.Controls
{
    public partial class ButtonControl : UserControl
    {
        public static readonly DependencyProperty BtnContentProperty =
            DependencyProperty.Register(nameof(BtnContent), typeof(string), typeof(ButtonControl), new PropertyMetadata("Ctn"));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(ButtonControl), new PropertyMetadata("Btn"));

        public static readonly DependencyProperty IsLitProperty =
            DependencyProperty.Register(nameof(IsLit), typeof(bool), typeof(ButtonControl), new PropertyMetadata(false));

        public static readonly DependencyProperty ChannelProperty =
            DependencyProperty.Register(nameof(Channel), typeof(int), typeof(ButtonControl), new PropertyMetadata(1));

        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register(nameof(Note), typeof(int), typeof(ButtonControl), new PropertyMetadata(60));

        public string BtnContent
        {
            get => (string)GetValue(BtnContentProperty);
            set => SetValue(BtnContentProperty, value);
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

        public int Channel
        {
            get => (int)GetValue(ChannelProperty);
            set => SetValue(ChannelProperty, value);
        }

        public int Note
        {
            get => (int)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        public ButtonControl()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Optional: Toggle state
            // IsLit = !IsLit;

            // Later: Send MIDI Note On/Off using Channel and Note
            // For now, just log or prepare
            System.Diagnostics.Debug.WriteLine($"Button clicked: Ch={Channel}, Note={Note}, Lit={IsLit}");
        }
    }
}