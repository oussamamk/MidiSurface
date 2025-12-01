using System.Collections.ObjectModel;

namespace MidiSurface.ViewModels
{
    // MainViewModel.cs
    public class MainViewModel
    {
        public ObservableCollection<DeviceViewModel> Devices { get; }

        public MainViewModel()
        {
            Devices = new ObservableCollection<DeviceViewModel>
            {
                new DeviceViewModel("SMC-Mixer", 8, 1),
                new DeviceViewModel("iCon - iControls", 9, 2)
            };
        }
    }
}
