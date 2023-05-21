using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicEditor.Models
{
    [Serializable]
    public class PaintPath : PaintShape
    {

        Geometry data;
        Color fillColor;
        public PaintPath()
        {
            data = Geometry.Parse("");
            fillColor = Colors.White;
            Rotate = new RotateTransform(0);
            Scale = new ScaleTransform(1, 1);
            Skew = new SkewTransform(0, 0);
        }
        [XmlIgnore]
        public Geometry Data
        {
            get => data;
            set => SetAndRaise(ref data, value);
        }
        [XmlIgnore]
        public Color FillColor
        {
            get => fillColor;
            set => fillColor = value;
        }

        public byte fillColorA;
        public byte fillColorR;
        public byte fillColorG;
        public byte fillColorB;
        string commands;
        public string Commands
        {
            get => commands;
            set => SetAndRaise(ref commands, value);
        }
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
            fillColorA = FillColor.A;
            fillColorR = FillColor.R;
            fillColorG = FillColor.G;
            fillColorB = FillColor.B;
            
        }
        public override void Deserialize()
        {
            StrokeColor = Color.FromArgb(colorA, colorR, colorG, colorB);
            Rotate = new RotateTransform(rotateAngle, rotateCenterX, rotateCenterY);
            Scale = new ScaleTransform(scaleX, scaleY);
            Skew = new SkewTransform(skewX, skewY);
            FillColor = Color.FromArgb(fillColorA, fillColorR, fillColorG, fillColorB);
            Data = Geometry.Parse(Commands);
        }
        public override void Move(Point position)
        {
            for(int i=0; i < Commands.Length-1;i++)
            {
                if (Commands[i] > 64 && Commands[i] < 91)
                {
                    for(int j = i+1; j < Commands.Length-1; j++)
                    {
                        if (Commands[j] > 57)
                        {
                            string stringPoints = Commands.Substring(i + 2, j - i - 3);
                            string[] pointsValue = stringPoints.Split(' ');
                            List<Point> listPoints = new List<Point>();

                            foreach (string s in pointsValue)
                            {
                                string[] coords = s.Split(',');
                                if (coords.Length == 2)
                                {
                                    double X;
                                    double Y;
                                    if (double.TryParse(coords[0], out X) == true &&
                                        double.TryParse(coords[1], out Y) == true)
                                    {
                                        listPoints.Add(new Point(X, Y));
                                    }
                                }
                            }
                            List<Point> shiftPoints = new List<Point>();
                            for (int k = 0; k < listPoints.Count; k++)
                            {
                                shiftPoints.Insert(k, new Point(listPoints[0].X - listPoints[k].X, listPoints[0].Y - listPoints[k].Y));
                            }
                            for (int k = 0; k < listPoints.Count; k++)
                            {
                                shiftPoints[k] = position - shiftPoints[k];
                            }
                            string stringShiftPoints = "";
                            foreach (Point point in shiftPoints)
                            {
                                stringShiftPoints += ((int)point.X).ToString() + "," + ((int)point.Y).ToString() + " ";
                            }
                            Commands = Commands.Substring(0, i+1) + " " + stringShiftPoints + Commands.Substring(j,Commands.Length-j);
                            Data = Geometry.Parse(Commands);
                        }
                    }
                }
            }
        }
    }
}
