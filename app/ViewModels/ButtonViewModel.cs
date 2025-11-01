using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MidiSurface.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        // This is the text shown on the button (e.g., "M", "S")
        public string Label { get; set; } = "Btn";

        // This controls whether the button is "lit" (red) or not (gray)
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

        // This lets WPF know a property changed (so UI updates)
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}