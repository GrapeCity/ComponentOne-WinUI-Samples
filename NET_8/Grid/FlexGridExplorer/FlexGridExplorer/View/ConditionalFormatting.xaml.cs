using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace FlexGridExplorer
{
    public sealed partial class ConditionalFormatting : UserControl
    {
        public ConditionalFormatting()
        {
            this.InitializeComponent();
            Tag = AppResources.ConditionalFormattingDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.Columns[4].DataMap = new GridDataMap() { ItemsSource = Customer.GetCountries(), DisplayMemberPath = "Value", SelectedValuePath = "Key" };
            grid.CellFactory = new MyCellFactory();
            grid.MinColumnWidth = 85;
        }
    }

    public class MyCellFactory : GridCellFactory
    {
        public ElementTheme Theme
        {
            get
            {
                return ((FrameworkElement)Grid.XamlRoot.Content).ActualTheme;
            }
        }

        static SolidColorBrush LightRedForeground = new SolidColorBrush(Color.FromArgb(0xFF, 0xC4, 0x2B, 0x1C));
        static SolidColorBrush LightRedBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFD, 0xE7, 0xE9));
        static SolidColorBrush LightGreenForeground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x7B, 0x0F));
        static SolidColorBrush LightGreenBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xDF, 0xF6, 0xDD));
        static SolidColorBrush DarkRedForeground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x99, 0xA4));
        static SolidColorBrush DarkRedBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0x44, 0x27, 0x26));
        static SolidColorBrush DarkGreenForeground = new SolidColorBrush(Color.FromArgb(0xFF, 0x6C, 0xCB, 0x5F));
        static SolidColorBrush DarkGreenBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0x39, 0x3D, 0x1B));

        public Brush RedForeground
        {
            get
            {
                return Theme == ElementTheme.Light ? LightRedForeground : DarkRedForeground;
            }
        }

        public Brush RedBackground
        {
            get
            {
                return Theme == ElementTheme.Light ? LightRedBackground : DarkRedBackground;
            }
        }

        public Brush GreenForeground
        {
            get
            {
                return Theme == ElementTheme.Light ? LightGreenForeground : DarkGreenForeground;
            }
        }

        public Brush GreenBackground
        {
            get
            {
                return Theme == ElementTheme.Light ? LightGreenBackground : DarkGreenBackground;
            }
        }

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, Thickness internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            var orderCountColumn = Grid.Columns["OrderCount"];
            if (cellType == GridCellType.Cell && range.Column == orderCountColumn.Index)
            {
                var cellValue = Grid[range.Row, range.Column] as int?;
                if (cellValue.HasValue)
                {
                    cell.Background = cellValue < 50.0 ? RedBackground : GreenBackground;
                }
            }
        }

        /// <summary>
        /// Binds the content of the cell.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="range">The range.</param>
        /// <param name="cellContent">Content of the cell.</param>
        public override void BindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        {
            base.BindCellContent(cellType, range, cellContent);
            var orderTotalColumn = Grid.Columns["OrderTotal"];
            var orderCountColumn = Grid.Columns["OrderCount"];
            if (cellType == GridCellType.Cell && range.Column == orderTotalColumn.Index)
            {
                var label = cellContent as TextBlock;
                if (label != null)
                {
                    var cellValue = Grid[range.Row, range.Column] as double?;
                    if (cellValue.HasValue)
                    {
                        label.Foreground = cellValue < 5000.0 ? RedForeground : GreenForeground;
                    }
                }
            }
        }

        public override void UnbindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        {
            base.UnbindCellContent(cellType, range, cellContent);
            var label = cellContent as TextBlock;
            if (label != null)
            {
                label.ClearValue(TextBlock.ForegroundProperty);
            }
        }
    }
}
