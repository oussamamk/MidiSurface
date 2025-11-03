using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class ChannelStripViewModel : INotifyPropertyChanged
    {
        public string ChannelName { get; set; } = "CH 1";

        private KnobViewModel _knob = new();
        public KnobViewModel Knob
        {
            get => _knob;
            set
            {
                _knob = value;
                OnPropertyChanged();
            }
        }

        private FaderViewModel _fader = new();
        public FaderViewModel Fader
        {
            get => _fader;
            set
            {
                _fader = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ButtonViewModel> Buttons { get; } = new();

        public ChannelStripViewModel(int channel, int idx)
        {
            Buttons.Add(new ButtonViewModel { Label = "Mute", BtnContent = "M", Channel = channel, Note = 16 + idx });
            Buttons.Add(new ButtonViewModel { Label = "Solo", BtnContent = "S", Channel = channel, Note = 8 + idx });
            Buttons.Add(new ButtonViewModel { Label = "Record", BtnContent = "R", Channel = channel, Note = 0 + idx });
            Buttons.Add(new ButtonViewModel { Label = "Select", BtnContent = "⭘", Channel = channel, Note = 24 + idx });

            Knob = new KnobViewModel
            {
                Label = "Pan" + idx.ToString(),
                Channel = channel,
                CCNumber = 16 + idx,
                Value = 64
            };

            Fader = new FaderViewModel
            {
                Label = "Volume",
                Channel = idx,
                Value = 20 + idx * 10
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}