using Avalonia;
using Avalonia.Media;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicEditor.Models
{
    [Serializable]
    [XmlInclude(typeof(PaintStraightLine))]
    [XmlInclude(typeof(PaintPolyline))]
    [XmlInclude(typeof(PaintPolygon))]
    [XmlInclude(typeof(PaintRectangle))]
    [XmlInclude(typeof(PaintEllipse))]
    [XmlInclude(typeof(PaintPath))]
    public abstract class PaintShape : AbstractNotifyPropertyChanged
    {
        string name;
        int strokeThickness;
        Color strokeColor;
        RotateTransform rotate;
        ScaleTransform scale;
        SkewTransform skew;
        public PaintShape()
        {
            name = "";
            strokeThickness = 1;
            strokeColor = Colors.Black;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }

        public int StrokeThickness
        {
            get => strokeThickness;
            set => strokeThickness = value;
        }
        [XmlIgnore]
        public Color StrokeColor
        {
            get => strokeColor;
            set => strokeColor = value;
        }
        [XmlIgnore]
        public RotateTransform Rotate
        {
            get => rotate;
            set => rotate = value;
        }
        [XmlIgnore]
        public ScaleTransform Scale
        {
            get => scale;
            set => scale = value;
        }
        [XmlIgnore]
        public SkewTransform Skew
        {
            get => skew;
            set => skew = value;
        }
        
        public byte colorA;
        public byte colorR;
        public byte colorG;
        public byte colorB;
        public double rotateAngle;
        public double rotateCenterX;
        public double rotateCenterY;
        public double scaleX;
        public double scaleY;
        public double skewX;
        public double skewY;
        public virtual void Serialize()
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
        }
        public virtual void Deserialize()
        {
            StrokeColor = Color.FromArgb(colorA, colorR, colorG, colorB);
            Rotate = new RotateTransform(rotateAngle, rotateCenterX, rotateCenterY);
            Scale = new ScaleTransform(scaleX, scaleY);
            Skew = new SkewTransform(skewX, skewY);
        }
        public virtual void Move(Point posistion) { }
    }
}
