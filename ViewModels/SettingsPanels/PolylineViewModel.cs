using Avalonia;
using Avalonia.Media;
using GraphicEditor.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.ViewModels.SettingsPanels
{
    public class PolylineViewModel : ShapeViewModelBase
    {
        public string ViewName => "Ломаная линия";
        string name;
        List<Point> points;
        ISolidColorBrush strokeColor;
        ushort strokeThickness;
        ObservableCollection<ISolidColorBrush> colors;
        
        public PolylineViewModel()
        {
            var brushes = typeof(Brushes).GetProperties().Select(brush => (ISolidColorBrush)brush.GetValue(brush));
            Colors = new ObservableCollection<ISolidColorBrush>(brushes);
            Name = "";
            Points = new List<Point>();
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            RotateAngle = 0;
            RotateCenter = new Point(0, 0);
            Scale = new Point(1, 1);
            Skew = new Point(0, 0);

        }
        public override PaintShape? GetShape()
        {
            if (Name != "" && StrokeThickness > 0)
            {
                if (Points.Count > 1)
                {
                    if (Scale.X == 0 || Scale.Y == 0)
                    {
                        Scale = new Point(1, 1);
                    }
                    return new PaintPolyline
                    {
                        Name = Name,
                        Points = Points,
                        StrokeColor = StrokeColor.Color,
                        StrokeThickness = StrokeThickness,
                        Rotate = new RotateTransform(RotateAngle, RotateCenter.X, RotateCenter.Y),
                        Scale = new ScaleTransform(Scale.X, Scale.Y),
                        Skew = new SkewTransform(Skew.X, Skew.Y),
                    };
                }
            }
            return null;
        }
        public override void ClearShape()
        {
            Name = "";
            Points = new List<Point>();
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            RotateAngle = 0;
            RotateCenter = new Point(0, 0);
            Scale = new Point(1, 1);
            Skew = new Point(0, 0);
        }
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public List<Point> Points
        {
            get => points;
            set => this.RaiseAndSetIfChanged(ref points, value);
        }
        public ISolidColorBrush StrokeColor
        {
            get => strokeColor;
            set
            {
                this.RaiseAndSetIfChanged(ref strokeColor, value);
            }
        }
        public ushort StrokeThickness
        {
            get => strokeThickness;
            set => this.RaiseAndSetIfChanged(ref strokeThickness, value);
        }
        public ObservableCollection<ISolidColorBrush> Colors
        {
            get => colors;
            set
            {
                this.RaiseAndSetIfChanged(ref colors, value);
            }
        }
    }
}
