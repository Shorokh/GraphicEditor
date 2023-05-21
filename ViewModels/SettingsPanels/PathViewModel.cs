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
    public class PathViewModel : ShapeViewModelBase
    {
        public string ViewName => "Составная фигура";
        string name;
        string commands;
        ISolidColorBrush fillColor;
        ISolidColorBrush strokeColor;
        ushort strokeThickness;
        ObservableCollection<ISolidColorBrush> colors;
        public PathViewModel()
        {
            var brushes = typeof(Brushes).GetProperties().Select(brush => (ISolidColorBrush)brush.GetValue(brush));
            Colors = new ObservableCollection<ISolidColorBrush>(brushes);
            Name = "";
            Commands = "";
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            FillColor = Colors[0];
            RotateAngle = 0;
            RotateCenter = new Point(0, 0);
            Scale = new Point(1, 1);
            Skew = new Point(0, 0);

        }
        public override PaintShape? GetShape()
        {
            if (Name != "")
            {
                if (Commands.Length > 0)
                {
                    if (Scale.X == 0 || Scale.Y == 0)
                    {
                        Scale = new Point(1, 1);
                    }
                    return new PaintPath
                    {
                        Name = Name,
                        Commands = Commands,
                        Data = Geometry.Parse(Commands),
                        FillColor = FillColor.Color,
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
            Commands = "";
            StrokeThickness = 1;
            StrokeColor = Colors[0];
            FillColor = Colors[0];
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
        public string Commands
        {
            get => commands;
            set => this.RaiseAndSetIfChanged(ref commands, value);
        }
        public ISolidColorBrush StrokeColor
        {
            get => strokeColor;
            set
            {
                this.RaiseAndSetIfChanged(ref strokeColor, value);
            }
        }
        public ISolidColorBrush FillColor
        {
            get => fillColor;
            set
            {
                this.RaiseAndSetIfChanged(ref fillColor, value);
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
