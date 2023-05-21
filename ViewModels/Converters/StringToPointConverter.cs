using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.ViewModels.Converters
{
    public class StringToPointConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            /*if(value is Point point)
            {
                string stringPoint = point.ToString();
                return stringPoint;
            }*/
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringPoint)
            {
                string[] coords = stringPoint.Split(',');

                if (coords.Length == 2)
                {
                    int X;
                    int Y;
                    if (int.TryParse(coords[0], out X) == true &&
                        int.TryParse(coords[1], out Y) == true)
                    {
                        return new Point(X, Y);
                    }
                }
            }

            return new Point(0, 0);
        }
    }
}
