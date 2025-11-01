using System.Globalization;
using System.Windows.Data;

namespace MidiSurface.Converters
{
    public class HalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d) return d / 2.0;
            if (value is int i) return i / 2.0;
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}