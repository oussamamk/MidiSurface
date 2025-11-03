using Melanchall.DryWetMidi.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        private string _label = "Btn";
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        private string _btnContent = "Btnc";
        public string BtnContent
        {
            get => _btnContent;
            set
            {
                _btnContent = value;
                OnPropertyChanged();
            }
        }

        private bool _isLit;
        public bool IsLit
        {
            get => _isLit;
            set
            {
                _isLit = value;
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

        private int _note = 0;
        public int Note
        {
            get => _note;
            set
            {
                if (value is >= 0 and <= 127)
                {
                    _note = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetPressedState(bool isPressed)
        {
            IsLit = isPressed;
        }

        internal void SetPressedState(SevenBitNumber velocity)
        {
            //if (velocity > 0)
            //{
            //    IsLit = true;
            //}
            //else
            //{
            //    IsLit = false;
            //}

            if (velocity == 0)
            {
                IsLit = !IsLit;
            }
        }
    }
}