﻿using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicEditor.Models
{
    public class PaintPolygon : PaintShape
    {

        List<Point> points;
        Color fillColor;
        public PaintPolygon()
        {
            points= new List<Point>();
            fillColor = Colors.White;
            pointsX = new List<double>();
            pointsY = new List<double>();
            Rotate = new RotateTransform(0);
            Scale = new ScaleTransform(1, 1);
            Skew = new SkewTransform(0, 0);
        }
        [XmlIgnore]
        public List<Point> Points
        {
            get => points;
            set => SetAndRaise(ref points, value);
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
        public List<double> pointsX;
        public List<double> pointsY;
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
            foreach (var point in points)
            {
                pointsX.Add((int)point.X);
                pointsY.Add((int)point.Y);
            }
        }
        public override void Deserialize()
        {
            StrokeColor = Color.FromArgb(colorA, colorR, colorG, colorB);
            Rotate = new RotateTransform(rotateAngle, rotateCenterX, rotateCenterY);
            Scale = new ScaleTransform(scaleX, scaleY);
            Skew = new SkewTransform(skewX, skewY);
            FillColor = Color.FromArgb(fillColorA, fillColorR, fillColorG, fillColorB);
            points.Clear();
            for (int i = 0; i < pointsX.Count; i++)
            {
                points.Add(new Point(pointsX[i], pointsY[i]));
            }
        }
        public override void Move(Point position)
        {
            List<Point> shiftPoints = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
            {
                shiftPoints.Insert(i, new Point(Points[0].X - Points[i].X, Points[0].Y - Points[i].Y));
            }
            for (int i = 0; i < Points.Count; i++)
            {
                shiftPoints[i] = position - shiftPoints[i];
            }
            Points = shiftPoints;

        }
    }
}
