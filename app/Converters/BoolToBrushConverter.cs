using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MidiSurface.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var red = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C51012"));
            return value is true ? red: Brushes.DimGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}