using Avalonia;
using Avalonia.Media;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicEditor.Models
{
    [Serializable]
    public class PaintStraightLine : PaintShape
    {

        Point endPoint;
        Point startPoint;
        public PaintStraightLine()
        {
            startPoint = new Point(0, 0);
            endPoint = new Point(0, 0);
            Rotate = new RotateTransform(0);
            Scale = new ScaleTransform(1, 1);
            Skew = new SkewTransform(0,0);
        }
        [XmlIgnore]
        public Point StartPoint
        {
            get => startPoint;
            set => SetAndRaise(ref startPoint, value);
        }
        [XmlIgnore]
        public Point EndPoint
        {
            get => endPoint;
            set => SetAndRaise(ref endPoint, value);
        }

        public double startPointX;
        public double startPointY;
        public double endPointX;
        public double endPointY;
        public override void Serialize()
        {
            colorA = StrokeColor.A;
            colorR = StrokeColor.R;
            colorG = StrokeColor.G;
            colorB = StrokeColor.B;
            rotateAngle = Rotate.Angle;
            rotateCenterX = Rotate.CenterX;
            rotateCenterY = Rotate.CenterY;
            scaleX = Scale.ScaleX;
            scaleY = Scale.ScaleY;
            skewX = Skew.AngleX;
            skewY = Skew.AngleY;
            startPointX = (int)startPoint.X;
            startPointY = (int)startPoint.Y;
            endPointX= (int)endPoint.X;
            endPointY = (int)endPoint.Y;
        }
        public override void Deserialize()
        {
            StrokeColor = Color.FromArgb(colorA, colorR, colorG, colorB);
            Rotate = new RotateTransform(rotateAngle, rotateCenterX, rotateCenterY);
            Scale = new ScaleTransform(scaleX, scaleY);
            Skew = new SkewTransform(skewX, skewY);
            StartPoint = new Point(startPointX, startPointY);
            EndPoint = new Point(endPointX, endPointY);
        }
        public override void Move(Point position)
        {
            Point shiftEndPoint = new Point(StartPoint.X - EndPoint.X, StartPoint.Y - EndPoint.Y);
            StartPoint = position;
            EndPoint = position - shiftEndPoint;

        }
    }
}
