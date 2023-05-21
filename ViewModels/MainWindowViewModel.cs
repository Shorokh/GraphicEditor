using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using DynamicData;
using GraphicEditor.Models;
using GraphicEditor.Models.Serializers;
using GraphicEditor.ViewModels.SettingsPanels;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text.Json;
using GraphicEditor.Views;
using Avalonia.VisualTree;
using System.Linq;

namespace GraphicEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ushort selectedShapeType = 0;
        ObservableCollection<PaintShape> shapes;
        ObservableCollection<ShapeViewModelBase> vmbaseCollection;
        ShapeViewModelBase content;
        public MainWindowViewModel()
        {
            shapes = new ObservableCollection<PaintShape>();
            vmbaseCollection = new ObservableCollection<ShapeViewModelBase>();
            vmbaseCollection.Add(new StraightLineViewModel());
            vmbaseCollection.Add(new PolylineViewModel());
            vmbaseCollection.Add(new PolygonViewModel());
            vmbaseCollection.Add(new RectangleViewModel());
            vmbaseCollection.Add(new EllipseViewModel());
            vmbaseCollection.Add(new PathViewModel());
            Content = vmbaseCollection[0];

            /*PaintStraightLine line = new PaintStraightLine { Name = "Line1", StartPoint = new Point(0, 0), EndPoint = new Point(100, 100), StrokeColor = new Color(255, 0, 0, 0), StrokeThickness = 1 };
            shapes.Add(line);
            PaintPolyline polyline = new PaintPolyline { Name = "polyline1", Points = new List<Point> { new Point(1, 1), new Point(50, 100), new Point(150, 100) }, StrokeThickness = 3, StrokeColor = new Color(255, 0, 0, 0) };
            shapes.Add(polyline);
            PaintPolygon polygon = new PaintPolygon { Name = "polygon1", Points = new List<Point> { new Point(300, 300), new Point(200, 200), new Point(350, 180) }, FillColor = new Color(255, 240, 116, 39), StrokeThickness = 3, StrokeColor = new Color(255, 0, 0, 0) };
            shapes.Add(polygon);
            PaintRectangle rectangle = new PaintRectangle { Name = "rectangle1", StartPoint = new Point(400, 50), Width = 50, Height = 50, FillColor = new Color(255, 240, 116, 39), StrokeThickness = 3, StrokeColor = new Color(255, 0, 0, 0) };
            shapes.Add(rectangle);
            PaintEllipse ellipse = new PaintEllipse { Name = "ellipse1", StartPoint = new Point(350, 50), Width = 50, Height = 50, FillColor = new Color(255, 240, 116, 39), StrokeThickness = 3, StrokeColor = new Color(255, 0, 0, 0) };
            shapes.Add(ellipse);
            PaintPath path = new PaintPath { Name = "path1", Data = Geometry.Parse("M 200,350 c 0,0 50,0 50,-50 c 0,0 50,0 50,50 h -50 v 50 l -50,-50 Z"), Commands = "M 200,350 c 0,0 50,0 50,-50 c 0,0 50,0 50,50 h -50 v 50 l -50,-50 Z", FillColor = new Color(255, 240, 116, 39), StrokeThickness = 3, StrokeColor = new Color(255, 0, 0, 0) };
            shapes.Add(path);*/

            System.Diagnostics.Debug.WriteLine("Start\n");
            buttonAdd = ReactiveCommand.Create(() =>
            {
                var shape = Content.GetShape();
                if (shape != null)
                {
                    Shapes.Add(shape);
                    System.Diagnostics.Debug.WriteLine("Added\n", shape.Name);
                }
            });
            buttonClear = ReactiveCommand.Create(() =>
            {
                Content.ClearShape();
            });
            buttonDelete = ReactiveCommand.Create<PaintShape>(shape =>
            {
                Shapes.Remove(shape);
                System.Diagnostics.Debug.WriteLine("Deleted\n", shape.Name);
            });
            SaveXML = ReactiveCommand.Create<MainWindow>(async(window) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save xml file";
                List<FileDialogFilter> filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("xml");
                filter.Extensions = extension;
                filter.Name = "Xml Files";
                filters.Add(filter);
                saveFileDialog.Filters = filters;
                saveFileDialog.DefaultExtension = "xml";
                string? result = await saveFileDialog.ShowAsync(window);
                if (result != null)
                {
                    foreach (var shape in Shapes)
                    {
                        shape.Serialize();
                    }
                    XMLSerializer<ObservableCollection<PaintShape>>.Save(result, Shapes);
                }
            });
            LoadXML = ReactiveCommand.Create<MainWindow>(async (window) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Open xml file";
                List<FileDialogFilter> filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("xml");
                filter.Extensions = extension;
                filter.Name = "Xml Files";
                filters.Add(filter);
                openFileDialog.Filters = filters;
                openFileDialog.AllowMultiple = false;
                string[]? result = await openFileDialog.ShowAsync(window);
                if(result != null)
                {
                    Shapes = XMLSerializer<ObservableCollection<PaintShape>>.Load(result[0]);
                    foreach (var shape in Shapes)
                    {
                        shape.Deserialize();
                    }
                }
                
            });
            SaveJSON = ReactiveCommand.Create<MainWindow>(async(window) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save json file";
                List<FileDialogFilter> filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("json");
                filter.Extensions = extension;
                filter.Name = "Json Files";
                filters.Add(filter);
                saveFileDialog.Filters = filters;
                saveFileDialog.DefaultExtension = "json";
                string? result = await saveFileDialog.ShowAsync(window);
                if(result != null)
                {
                    JSONSerializer<ObservableCollection<PaintShape>>.Save(result, Shapes);
                }
                
            });
            LoadJSON = ReactiveCommand.Create<MainWindow>(async (window) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Open json file";
                List<FileDialogFilter> filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("json");
                filter.Extensions = extension;
                filter.Name = "Json Files";
                filters.Add(filter);
                openFileDialog.Filters = filters;
                openFileDialog.AllowMultiple = false;
                string[]? result = await openFileDialog.ShowAsync(window);
                if (result != null)
                {
                    Shapes = JSONSerializer<ObservableCollection<PaintShape>>.Load(result[0]);
                }
                    
            });
            SavePNG = ReactiveCommand.Create<MainWindow>(async (window) =>
            {
                SaveFileDialog saveFileDialog= new SaveFileDialog();
                saveFileDialog.Title = "Save png file";
                List<FileDialogFilter> filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("png");
                filter.Extensions = extension;
                filter.Name = "Image Files";
                filters.Add(filter);
                saveFileDialog.Filters = filters;
                saveFileDialog.DefaultExtension = "png";
                string? result = await saveFileDialog.ShowAsync(window);
                if (result != null)
                {
                    Canvas canvas = window.GetVisualDescendants().OfType<Canvas>().First(c => c.Name == "canvas");
                    var pixelSize = new PixelSize((int)canvas.Bounds.Width, (int)canvas.Bounds.Height);
                    var size = new Size(canvas.Bounds.Width, canvas.Bounds.Height);
                    using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(96, 96)))
                    {
                        canvas.Measure(size);
                        canvas.Arrange(new Rect(size));
                        bitmap.Render(canvas);
                        bitmap.Save(result);
                    }
                }
                
            });
        }
        public void LoadShapes(string path)
        {
            if (".xml".Equals(System.IO.Path.GetExtension(path)))
            {
                Shapes = XMLSerializer<ObservableCollection<PaintShape>>.Load(path);
                foreach (var shape in Shapes)
                {
                    shape.Deserialize();
                }
            }
            if (".json".Equals(System.IO.Path.GetExtension(path)))
            {
                Shapes = JSONSerializer<ObservableCollection<PaintShape>>.Load(path);
            }
            
        }
        public void Update(PaintShape shape)
        {
            Shapes.Remove(shape);
            Shapes.Add(shape);
        }
        public ObservableCollection<PaintShape> Shapes
        {
            get => shapes;
            set => this.RaiseAndSetIfChanged(ref shapes, value);
        }
        public ushort SelectedShapeType
        {
            get => selectedShapeType;
            set => this.RaiseAndSetIfChanged(ref selectedShapeType, value); 
        }
        public ShapeViewModelBase Content
        {
            get => content;
            set
            {
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }

        public ObservableCollection<ShapeViewModelBase> VmbaseCollection
        {
            get => vmbaseCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref vmbaseCollection, value);
            }
        }
        public ReactiveCommand<Unit, Unit> buttonAdd { get; }
        public ReactiveCommand<Unit, Unit> buttonClear { get; }
        public ReactiveCommand<PaintShape, Unit> buttonDelete { get; }
        public ReactiveCommand<MainWindow, Unit> SavePNG { get; }
        public ReactiveCommand<MainWindow, Unit> SaveJSON { get; }
        public ReactiveCommand<MainWindow, Unit> SaveXML { get; }
        public ReactiveCommand<MainWindow, Unit> LoadJSON { get; }
        public ReactiveCommand<MainWindow, Unit> LoadXML { get; }
    }
}