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
    public class StraightLineViewModel : ShapeViewModelBase 
    {
        public string ViewName => "Прямая линия";
        string name;
        Point startPoint;
        Point endPoint;
        ISolidColorBrush strokeColor;
        ushort strokeThickness;
        ObservableCollection<ISolidColorBrush> colors;
        
        public StraightLineViewModel() 
        {
            var brushes = typeof(Brushes).GetProperties().Select(brush => (ISolidColorBrush)brush.GetValue(brush));
            Colors = new ObservableCollection<ISolidColorBrush>(brushes);
            Name = "";
            StartPoint = new Point(0, 0);
            EndPoint = new Point(0, 0);
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            RotateAngle = 0;
            RotateCenter= new Point(0, 0);
            Scale = new Point(1, 1);
            Skew = new Point(0, 0);
        }
        public override PaintShape? GetShape()
        {
            if(Name != "" && StrokeThickness > 0)
            {
                if (StartPoint.Y != 0 && StartPoint.X != 0 || EndPoint.X != 0 && EndPoint.Y != 0)
                {
                    if(Scale.X == 0 || Scale.Y ==0)
                    {
                        Scale = new Point(1, 1);
                    }
                    return new PaintStraightLine { 
                        Name = Name,
                        StartPoint = StartPoint,
                        EndPoint = EndPoint,
                        StrokeColor = StrokeColor.Color,
                        StrokeThickness = StrokeThickness,
                        Rotate = new RotateTransform(RotateAngle,RotateCenter.X,RotateCenter.Y),
                        Scale = new ScaleTransform(Scale.X,Scale.Y),
                        Skew = new SkewTransform(Skew.X,Skew.Y),
                    };
                }
            }
            return null;
        }
        public override void ClearShape()
        {
            Name = "";
            StartPoint = new Point(0, 0);
            EndPoint = new Point(0, 0);
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            RotateAngle = 0;
            RotateCenter = new Point(0, 0);
            Scale = new Point(1, 1);
            Skew = new Point(0,0);
        }
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public Point StartPoint
        {
            get => startPoint;
            set => this.RaiseAndSetIfChanged(ref startPoint, value);
        }
        public Point EndPoint
        {
            get => endPoint;
            set => this.RaiseAndSetIfChanged(ref endPoint, value);
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
