using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class ChannelStripViewModel : INotifyPropertyChanged
    {
        // Top label (e.g., "Vocals", "Drums", "CH 1")
        public string ChannelName { get; set; } = "CH 1";

        // === KNOB ===
        public string KnobLabel { get; set; } = "Pan";
        private double _knobValue = 63;
        public double KnobValue
        {
            get => _knobValue;
            set
            {
                _knobValue = Math.Clamp(value, 0, 127);
                OnPropertyChanged();
            }
        }

        // === FADER ===
        public string FaderLabel { get; set; } = "Volume";
        private double _faderValue = 20;
        public double FaderValue
        {
            get => _faderValue;
            set
            {
                _faderValue = Math.Clamp(value, 0, 127);
                OnPropertyChanged();
            }
        }

        // === BUTTONS (4 of them) ===
        public ObservableCollection<ButtonViewModel> Buttons { get; } = new();

        // Constructor: creates 4 buttons with labels M, S, A, R
        public ChannelStripViewModel()
        {
            Buttons.Add(new ButtonViewModel { Label = "M" });
            Buttons.Add(new ButtonViewModel { Label = "S" });
            Buttons.Add(new ButtonViewModel { Label = "A" });
            Buttons.Add(new ButtonViewModel { Label = "R" });
        }

        // Needed for UI updates
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}