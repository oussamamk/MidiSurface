using Melanchall.DryWetMidi.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class KnobViewModel : INotifyPropertyChanged
    {
        private string _label = "Knob";
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        private int _channel = 0;
        public int Channel
        {
            get => _channel;
            set
            {
                if (value is >= 0 and <= 15)
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

        private int _value = 64;
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

        internal void SetValue(SevenBitNumber controlValue)
        {
            //if (controlValue == 65)
            //{
            //    Value--;
            //}
            //else if (controlValue == 1)
            //{
            //    Value++;
            //}

            Value = controlValue;
        }

        internal void SetLabel(string valueString)
        {
            Label = valueString;
        }
    }
}