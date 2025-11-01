using System.Windows;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;

namespace MidiSurface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 👇 Keep a reference to prevent garbage collection
        private InputDevice? _midiIn = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // ← This connects everything!
        }

        //public MainWindow()
        //{
        //    InitializeComponent();

        //    _midiIn = InputDevice.GetByName("test");
        //    if (_midiIn != null)
        //    {
        //        _midiIn.EventReceived += OnMessageRecieved;
        //        _midiIn.StartEventsListening();
        //        Console.WriteLine($"Listening to MIDI input: {_midiIn.Name}");
        //    }
        //}

        private void OnMessageRecieved(object? sender, MidiEventReceivedEventArgs e)
        {
            // This will now fire!
            if (sender != null)
            {
                MidiDevice m = (MidiDevice)sender;

                Console.WriteLine($"MIDI: {m}, {e}");
            }

        }


        private void LoadChannelStrips()
        {
            for (int i = 0; i < 8; i++)
            {
                //var strip = new MidiSurface.Controls.ChannelStrip();
                //        strip.ChannelName = "CH" + (i + 1).ToString();
                //MainPanel.Children.Add(strip);
            }
        }

        // 👇 Clean up when window closes
        protected override void OnClosed(EventArgs e)
        {
            _midiIn?.StopEventsListening();
            _midiIn?.Dispose();
            base.OnClosed(e);
        }

        private void OnButtonCLick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked!");
        }
    }
}