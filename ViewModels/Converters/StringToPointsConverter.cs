using Avalonia;
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
    public class StringToPointsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringPoints)
            {
                string[] points = stringPoints.Split(' ');
                List<Point> listPoints = new List<Point>();

                foreach (string i in points)
                {
                    string[] coords = i.Split(',');
                    if (coords.Length == 2)
                    {
                        int X;
                        int Y;
                        if (int.TryParse(coords[0], out X) == true &&
                            int.TryParse(coords[1], out Y) == true)
                        {
                            listPoints.Add(new Point(X, Y));
                        }
                    }
                }
                return listPoints;
            }

            return null;
        }
    }
}
