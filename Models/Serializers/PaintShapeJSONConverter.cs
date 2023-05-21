using Avalonia;
using Avalonia.Controls.Shapes;
using GraphicEditor.ViewModels.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Media;
using System.Collections.ObjectModel;
using DynamicData.Binding;

namespace GraphicEditor.Models.Serializers
{
    public class PaintShapeJSONConverter : JsonConverter<ObservableCollection<PaintShape>>

    {
        Point parsePoint(string? pointValue)
        {
            string[] coords = pointValue.Split(',');
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
            return new Point(0, 0);
        }
        List<Point> parsePoints(string? poinstValue)
        {
            string[] points = poinstValue.Split(' ');
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
        public override ObservableCollection<PaintShape>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Missed StartObject token");
            }
            ObservableCollection<PaintShape> shapes = new ObservableCollection<PaintShape>();
            reader.Read();
            string? countName = reader.GetString();
            reader.Read();
            string? countValue = reader.GetString();
            int count;
            if(int.TryParse(countValue,out count)){
                for (int i = 0; i < count; i++)
                {
                    reader.Read();
                    string? propertyName = reader.GetString();
                    if (propertyName != null /*&& propertyName.Equals("type")*/)
                    {
                        reader.Read();
                        string? typeName = reader.GetString();

                        if (typeName != null && typeName.Equals("PaintStraightLine"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? startPointProperty = reader.GetString();
                            reader.Read();
                            string? startPointValue = reader.GetString();

                            reader.Read();
                            string? endPointProperty = reader.GetString();
                            reader.Read();
                            string? endPointValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();
                            
                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintStraightLine straightLine = new PaintStraightLine
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                StartPoint = parsePoint(startPointValue),
                                EndPoint = parsePoint(endPointValue),
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X,rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X,scale.Y),
                                Skew = new SkewTransform (skew.X,skew.Y),

                            };
                            shapes.Add(straightLine);
                        }
                        if (typeName != null && typeName.Equals("PaintPolyline"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? pointsProperty = reader.GetString();
                            reader.Read();
                            string? pointsValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();

                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintPolyline polyine = new PaintPolyline
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                Points = parsePoints(pointsValue),
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X, rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X, scale.Y),
                                Skew = new SkewTransform(skew.X, skew.Y),
                            };
                            shapes.Add(polyine);
                        }
                        if (typeName != null && typeName.Equals("PaintPolygon"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? fillColorProperty = reader.GetString();
                            reader.Read();
                            string? fillColorValue = reader.GetString();

                            reader.Read();
                            string? pointsProperty = reader.GetString();
                            reader.Read();
                            string? pointsValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();

                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintPolygon polygon = new PaintPolygon
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                FillColor = Color.Parse(fillColorValue),
                                Points = parsePoints(pointsValue),
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X, rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X, scale.Y),
                                Skew = new SkewTransform(skew.X, skew.Y),
                            };
                            shapes.Add(polygon);
                        }
                        if (typeName != null && typeName.Equals("PaintRectangle"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? fillColorProperty = reader.GetString();
                            reader.Read();
                            string? fillColorValue = reader.GetString();

                            reader.Read();
                            string? widthProperty = reader.GetString();
                            reader.Read();
                            string? widthValue = reader.GetString();

                            reader.Read();
                            string? heightProperty = reader.GetString();
                            reader.Read();
                            string? heightValue = reader.GetString();

                            reader.Read();
                            string? startPointProperty = reader.GetString();
                            reader.Read();
                            string? startPointValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();

                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintRectangle rectangle = new PaintRectangle
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                FillColor = Color.Parse(fillColorValue),
                                Width = int.Parse(widthValue),
                                Height = int.Parse(heightValue),
                                StartPoint = parsePoint(startPointValue),
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X, rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X, scale.Y),
                                Skew = new SkewTransform(skew.X, skew.Y),
                            };
                            shapes.Add(rectangle);
                        }
                        if (typeName != null && typeName.Equals("PaintEllipse"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? fillColorProperty = reader.GetString();
                            reader.Read();
                            string? fillColorValue = reader.GetString();

                            reader.Read();
                            string? widthProperty = reader.GetString();
                            reader.Read();
                            string? widthValue = reader.GetString();

                            reader.Read();
                            string? heightProperty = reader.GetString();
                            reader.Read();
                            string? heightValue = reader.GetString();

                            reader.Read();
                            string? startPointProperty = reader.GetString();
                            reader.Read();
                            string? startPointValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();

                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintEllipse ellipse = new PaintEllipse
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                FillColor = Color.Parse(fillColorValue),
                                Width = int.Parse(widthValue),
                                Height = int.Parse(heightValue),
                                StartPoint = parsePoint(startPointValue),
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X, rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X, scale.Y),
                                Skew = new SkewTransform(skew.X, skew.Y),
                            };
                            shapes.Add(ellipse);
                        }
                        if (typeName != null && typeName.Equals("PaintPath"))
                        {
                            reader.Read();
                            string? name = reader.GetString();
                            reader.Read();
                            string? nameValue = reader.GetString();

                            reader.Read();
                            string? strokeThicknessProperty = reader.GetString();
                            reader.Read();
                            string? strokeThicknessValue = reader.GetString();

                            reader.Read();
                            string? strokeColorProperty = reader.GetString();
                            reader.Read();
                            string? strokeColorValue = reader.GetString();

                            reader.Read();
                            string? fillColorProperty = reader.GetString();
                            reader.Read();
                            string? fillColorValue = reader.GetString();

                            reader.Read();
                            string? commandProperty = reader.GetString();
                            reader.Read();
                            string? commandValue = reader.GetString();

                            reader.Read();
                            string? rotateCenterProperty = reader.GetString();
                            reader.Read();
                            string? rotateCenterValue = reader.GetString();

                            reader.Read();
                            string? rotateAngleProperty = reader.GetString();
                            reader.Read();
                            string? rotateAngleValue = reader.GetString();

                            reader.Read();
                            string? scaleProperty = reader.GetString();
                            reader.Read();
                            string? scaleValue = reader.GetString();

                            reader.Read();
                            string? skewProperty = reader.GetString();
                            reader.Read();
                            string? skewValue = reader.GetString();

                            Point rotateCenter = parsePoint(rotateCenterValue);
                            Point scale = parsePoint(scaleValue);
                            Point skew = parsePoint(skewValue);

                            PaintPath path = new PaintPath
                            {
                                Name = nameValue,
                                StrokeThickness = int.Parse(strokeThicknessValue),
                                StrokeColor = Color.Parse(strokeColorValue),
                                FillColor = Color.Parse(fillColorValue),
                                Data = Geometry.Parse(commandValue),
                                Commands = commandValue,
                                Rotate = new RotateTransform(double.Parse(rotateAngleValue), rotateCenter.X, rotateCenter.Y),
                                Scale = new ScaleTransform(scale.X, scale.Y),
                                Skew = new SkewTransform(skew.X, skew.Y),
                            };
                            shapes.Add(path);
                        }

                    }
                    else
                    {
                        throw new JsonException("Missed type property");
                    }
                }
                reader.Read();
                return shapes;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, ObservableCollection<PaintShape> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Count", value.Count.ToString());
            //writer.WriteStartArray("Data");
            foreach (var shape in value) {
                if (shape is PaintStraightLine line)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintStraightLine");
                    writer.WriteString("Name", line.Name);
                    writer.WriteString("StrokeThickness", line.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", line.StrokeColor.ToString());
                    writer.WriteString("StartPoint", ((int)(line.StartPoint.X)).ToString() + "," + ((int)line.StartPoint.Y).ToString());
                    writer.WriteString("EndPoint", ((int)line.EndPoint.X).ToString() + "," + ((int)line.EndPoint.Y).ToString());
                    //writer.WriteEndObject();
                }
                if (shape is PaintPolyline polyline)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintPolyline");
                    writer.WriteString("Name", polyline.Name);
                    writer.WriteString("StrokeThickness", polyline.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", polyline.StrokeColor.ToString());
                    /*if(polyline.Points.Count > 0) {
                        
                        foreach (var point in polyline.Points)
                        {
                            writer.WriteString("Point", point.X.ToString() + "," + point.Y.ToString());
                        }
                    }*/
                    string points = "";
                    foreach (var point in polyline.Points)
                    {
                        points += ((int)point.X).ToString() + "," + ((int)point.Y).ToString() + " ";
                    }
                    writer.WriteString("Points", points);
                    //writer.WriteEndObject();
                }
                if (shape is PaintPolygon polygon)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintPolygon");
                    writer.WriteString("Name", polygon.Name);
                    writer.WriteString("StrokeThickness", polygon.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", polygon.StrokeColor.ToString());
                    writer.WriteString("FillColor", polygon.FillColor.ToString());
                    string points = "";
                    foreach (var point in polygon.Points)
                    {
                        points += ((int)point.X).ToString() + "," + ((int)point.Y).ToString() + " ";
                    }
                    writer.WriteString("Points", points);
                    //writer.WriteEndObject();
                }
                if (shape is PaintRectangle rectangle)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintRectangle");
                    writer.WriteString("Name", rectangle.Name);
                    writer.WriteString("StrokeThickness", rectangle.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", rectangle.StrokeColor.ToString());
                    writer.WriteString("FillColor", rectangle.FillColor.ToString());
                    writer.WriteString("Width", rectangle.Width.ToString());
                    writer.WriteString("Height", rectangle.Height.ToString());
                    writer.WriteString("StartPoint", ((int)rectangle.StartPoint.X).ToString() + "," + ((int)rectangle.StartPoint.Y).ToString());
                    //writer.WriteEndObject();
                }
                if (shape is PaintEllipse ellipse)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintEllipse");
                    writer.WriteString("Name", ellipse.Name);
                    writer.WriteString("StrokeThickness", ellipse.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", ellipse.StrokeColor.ToString());
                    writer.WriteString("FillColor", ellipse.FillColor.ToString());
                    writer.WriteString("Width", ellipse.Width.ToString());
                    writer.WriteString("Height", ellipse.Height.ToString());
                    writer.WriteString("StartPoint", ((int)ellipse.StartPoint.X).ToString() + "," + ((int)ellipse.StartPoint.Y).ToString());
                    //writer.WriteEndObject();
                }
                if (shape is PaintPath path)
                {
                    //writer.WriteStartObject();
                    writer.WriteString("type", "PaintPath");
                    writer.WriteString("Name", path.Name);
                    writer.WriteString("StrokeThickness", path.StrokeThickness.ToString());
                    writer.WriteString("StrokeColor", path.StrokeColor.ToString());
                    writer.WriteString("FillColor", path.FillColor.ToString());
                    writer.WriteString("Commands", path.Commands);
                    //writer.WriteEndObject();
                }
                writer.WriteString("RotateCenter", shape.Rotate.CenterX.ToString() + "," + shape.Rotate.CenterY.ToString());
                writer.WriteString("RotateAngle", shape.Rotate.Angle.ToString());
                writer.WriteString("Skale", shape.Scale.ScaleX.ToString() + "," + shape.Scale.ScaleY.ToString());
                writer.WriteString("Skew", shape.Skew.AngleX.ToString() + "," + shape.Skew.AngleY.ToString());
            }

            //writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
