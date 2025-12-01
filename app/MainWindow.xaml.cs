using Melanchall.DryWetMidi.Multimedia;
using MidiSurface.ViewModels;
using System.Windows;

namespace MidiSurface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MidiMessageRouter _router1;
        MidiMessageRouter _router2;
        MainViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;

            _router1 = new MidiMessageRouter(_vm.Devices[0]);
            _router1.StartListening("vtDev1");

            _router2 = new MidiMessageRouter(_vm.Devices[1]);
            _router2.StartListening("vtDev2");

        }

    }
}