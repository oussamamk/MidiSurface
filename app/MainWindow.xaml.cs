using Melanchall.DryWetMidi.Multimedia;
using System.Windows;

namespace MidiSurface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MidiMessageRouter _router;
        MainViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;

            _router = new MidiMessageRouter(_vm);
            _router.StartListening("test");
        }

    }
}