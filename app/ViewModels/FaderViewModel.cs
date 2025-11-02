using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class FaderViewModel : INotifyPropertyChanged
    {
        private string _label = "Volume";
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        private int _channel = 1;
        public int Channel
        {
            get => _channel;
            set
            {
                if (value is >= 1 and <= 16)
                {
                    _channel = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ccNumber = 1;
        public int CCNumber
        {
            get => _ccNumber;
            set
            {
                if (value is >= 0 and <= 127)
                {
                    _ccNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _value = 20;
        public int Value
        {
            get => _value;
            set
            {
                if (value is >= 0 and <= 127)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}