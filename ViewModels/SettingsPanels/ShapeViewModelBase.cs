using GraphicEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using ReactiveUI;

namespace GraphicEditor.ViewModels.SettingsPanels
{
    public abstract class ShapeViewModelBase : ViewModelBase
    {
        double rotateAngle;
        Point rotateCenter;
        Point scale;
        Point skew;
        public double RotateAngle
        {
            get => rotateAngle;
            set => this.RaiseAndSetIfChanged(ref rotateAngle, value);
        }
        public Point RotateCenter
        {
            get => rotateCenter;
            set => this.RaiseAndSetIfChanged(ref rotateCenter, value);
        }
        public Point Scale
        {
            get => scale;
            set => this.RaiseAndSetIfChanged(ref scale, value);
        }
        public Point Skew
        {
            get => skew;
            set => this.RaiseAndSetIfChanged(ref skew, value);
        }
        public abstract PaintShape? GetShape();
        public abstract void ClearShape();
    }
}
