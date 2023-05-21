using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.VisualTree;
using GraphicEditor.ViewModels;
using GraphicEditor.Models;
using System.Linq;

namespace GraphicEditor.Views
{
    public partial class MainWindow : Window
    {
        private Point pointPointerPressed;
        private Point pointerPositionIntoShape;
        public MainWindow()
        {
            InitializeComponent();
            AddHandler(DragDrop.DropEvent, Drop);
            //DataContext = new MainWindowViewModel();
        }
        private void Drop(object? sender, DragEventArgs dragEventArgs)
        {
            if (dragEventArgs.Data.Contains(DataFormats.FileNames) == true)
            {
                string? fileName = dragEventArgs.Data.GetFileNames()?.FirstOrDefault();

                if (fileName != null)
                {
                    if (this.DataContext is MainWindowViewModel dataContext)
                    {
                        dataContext.LoadShapes(fileName);
                    }
                }
            }
        }
        private void PointerPressedOnCanvas(object sender, PointerPressedEventArgs pointerPressedEventArgs)
        {
            pointPointerPressed = pointerPressedEventArgs
                .GetPosition(
                this.GetVisualDescendants()
                .OfType<Canvas>()
                .FirstOrDefault());

            if (this.DataContext is MainWindowViewModel viewModel)
            {
                if (pointerPressedEventArgs.Source is Shape shape)
                {
                    pointerPositionIntoShape = pointerPressedEventArgs.GetPosition(shape);
                    this.PointerMoved += PointerMoveDragShape;
                    this.PointerReleased += PointerPressedReleasedDragShape;
                }
            }
        }
        private void PointerMoveDragShape(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (pointerEventArgs.Source is Shape shape)
            {
                Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                if (shape.DataContext is PaintShape myShape)
                {
                    
                    myShape.Move(new Point(
                        currentPointerPosition.X - pointerPositionIntoShape.X,
                        currentPointerPosition.Y - pointerPositionIntoShape.Y));
                    System.Diagnostics.Debug.WriteLine("currentPointerPosition.X", currentPointerPosition.X.ToString());
                    System.Diagnostics.Debug.WriteLine("currentPointerPosition.Y", currentPointerPosition.Y.ToString());

                    System.Diagnostics.Debug.WriteLine("pointerPositionIntoShape.X", pointerPositionIntoShape.X.ToString());
                    System.Diagnostics.Debug.WriteLine("pointerPositionIntoShape.Y\n", pointerPositionIntoShape.Y.ToString());

                }
            }
        }

        private void PointerPressedReleasedDragShape(object? sender, PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            this.PointerMoved -= PointerMoveDragShape;
            this.PointerReleased -= PointerPressedReleasedDragShape;;
            System.Diagnostics.Debug.WriteLine("pointerPositionIntoShape.X", pointerPositionIntoShape.X.ToString());
            System.Diagnostics.Debug.WriteLine("pointerPositionIntoShape.Y", pointerPositionIntoShape.Y.ToString());
            System.Diagnostics.Debug.WriteLine("Moved\n");

        }
    }
}