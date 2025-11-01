using System.Globalization;
using System.Windows.Data;

namespace MidiSurface.Converters
{
    public class KnobPointerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 ||
                !(values[0] is double width) ||
                !(values[1] is double height))
                return 0.0;

            double centerX = width / 2;
            double centerY = height / 2;

            // Radius: 40% of the smaller dimension (so it fits inside ellipse)
            double radius = Math.Min(width, height) * 0.4;

            return parameter switch
            {
                "X1" => centerX,
                "Y1" => centerY,
                "X2" => centerX,
                "Y2" => centerY - radius, // pointer points upward
                _ => 0.0
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}