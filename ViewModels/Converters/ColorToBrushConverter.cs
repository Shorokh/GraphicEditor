using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.ViewModels.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color brush1)
            {
                return new SolidColorBrush(brush1);
            }
            /*if (value is ISolidColorBrush brush2)
            {
                return new SolidColorBrush(brush2.Color);
            }*/
            return new SolidColorBrush(Colors.Red);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
