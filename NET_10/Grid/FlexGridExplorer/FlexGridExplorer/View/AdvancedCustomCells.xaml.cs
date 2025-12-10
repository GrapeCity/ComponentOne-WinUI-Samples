using C1.WebStandards.Svg;
using C1.WinUI.Core.Svg;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.Graphics.Canvas;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using Windows.Foundation;
using Windows.Storage;

namespace FlexGridExplorer
{
    public partial class AdvancedCustomCells : UserControl
    {
        public AdvancedCustomCells()
        {
            InitializeComponent();
            Tag = AppResources.AdvancedCustomCellsDescription;
        }
    }

    public class MyCustomCellFactory : GridCellFactory
    {
        private Random _rand = new Random();
        private bool?[,] _cells;

        public MyCustomCellFactory()
        {
            AllowCustomCells = true;

            _cells = new bool?[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    _cells[row, column] = _rand.NextDouble() > 0.5;
                }
            }
        }

        public override object GetCellKind(GridCellType cellType, GridCellRange range)
        {
            return typeof(MyCustomCell);
        }

        public override GridCellView CreateCell(GridCellType cellType, GridCellRange range, object cellKind)
        {
            return new MyCustomCell();
        }

        public override void BindCell(GridCellType cellType, GridCellRange range, GridCellView cell)
        {
            (cell as MyCustomCell).IsCross = _cells[range.Row, range.Column] ?? false;
        }

        public override void UnbindCell(GridCellType cellType, GridCellRange range, GridCellView cell)
        {
        }

        protected override void OnCellTapped(GridControlTapEventArgs e)
        {
            base.OnCellTapped(e);
            _cells[e.CellRange.Row, e.CellRange.Column] = !_cells[e.CellRange.Row, e.CellRange.Column];
            Grid.Refresh(GridCellType.Cell, new GridCellRange(e.CellRange.Row, e.CellRange.Column));
        }
    }

    public class MyCustomCell : GridCellView
    {
        private static C1SvgDoc _crossSvg;
        private static C1SvgDoc _circleSvg;

        private bool _isCross;

        public MyCustomCell()
        {
            NeedsDrawing = true;
        }

        static MyCustomCell()
        {
            var crossFile = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Images/cross.svg")).GetAwaiter().GetResult();
            var circleFile = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Images/circle.svg")).GetAwaiter().GetResult();
            var crossFileStream = crossFile.OpenStreamForReadAsync().GetAwaiter().GetResult();
            var circleFileStream = circleFile.OpenStreamForReadAsync().GetAwaiter().GetResult();
            _crossSvg = C1SvgDoc.Parse(crossFileStream);
            _circleSvg = C1SvgDoc.Parse(circleFileStream);
        }

        public bool IsCross
        {
            get => _isCross;
            set
            {
                _isCross = value;
                InvalidateDrawing();
            }
        }

        protected override void OnRenderBackground(CanvasDrawingSession drawingContext, Rect backgroundArea)
        {
            base.OnRenderBackground(drawingContext, backgroundArea);
            backgroundArea.Inflate(-8, -8);//Adds some padding
            if (IsCross)
            {
                drawingContext.DrawSvg(_crossSvg, Foreground, backgroundArea);
            }
            else
            {
                drawingContext.DrawSvg(_circleSvg, Foreground, backgroundArea);
            }
        }
    }
    public static class RectExtensions
    {
        public static Rect Inflate(this Rect rect, double dx, double dy)
        {
            return new Rect(
                rect.X - dx,
                rect.Y - dy,
                rect.Width + dx * 2,
                rect.Height + dy * 2
            );
        }
    }
}
