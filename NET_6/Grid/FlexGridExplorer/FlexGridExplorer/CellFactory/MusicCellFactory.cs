using C1.WinUI.Grid;
using Microsoft.UI.Xaml;
using System;

namespace FlexGridExplorer
{
    // cell factory used to create iTunes cells
    public class MusicCellFactory : GridCellFactory
    {
        public override object GetCellContentType(GridCellType cellType, GridCellRange range)
        {
            if (cellType == GridCellType.Cell)
            {
                var row = Grid.Rows[range.Row];
                var col = Grid.Columns[range.Column];

                if (row is GridGroupRow groupRow && range.Column == 0)
                {
                    return groupRow.Level == 0 ? typeof(ArtistCell) : typeof(AlbumCell);
                }

                var colName = col.ColumnName;
                if (colName == "Name")
                {
                    return typeof(SongCell);
                }
                if (colName == "Rating")
                {
                    return typeof(RatingCell);
                }
            }
            return base.GetCellContentType(cellType, range);
        }

        public override FrameworkElement CreateCellContent(GridCellType cellType, GridCellRange range, object cellContentType)
        {
            if (cellContentType as Type == typeof(ArtistCell))
                return new ArtistCell();
            else if (cellContentType as Type == typeof(AlbumCell))
                return new AlbumCell();
            else if (cellContentType as Type == typeof(SongCell))
                return new SongCell();
            else if (cellContentType as Type == typeof(RatingCell))
                return new RatingCell();
            return base.CreateCellContent(cellType, range, cellContentType);
        }

        public override void BindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        {
            if (cellContent is ImageCell imageCell)
            {
                var cellText = Grid.GetCellText(range);
                imageCell.TextBlock.Text = cellText;
                if (cellContent is NodeCell nodeCell)
                {
                    var groupRow = Grid.Rows[range.Row] as GridGroupRow;
                    nodeCell.Tag = groupRow;
                    nodeCell.IsCollapsed = groupRow?.IsCollapsed ?? false;
                    nodeCell.IsCollapsedChanged += OnIsCollapsedChanged;
                }
            }
            else if (cellContent is RatingCell ratingCell)
            {
                var col = Grid.Columns[range.Column];
                var cellValue = Grid.GetCellValue(range);
                ratingCell.Rating = Convert.ToInt32(cellValue);
            }
            else
            {
                base.BindCellContent(cellType, range, cellContent);
            }
        }

        public override void UnbindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        {
            if (cellContent is NodeCell nodeCell)
            {
                nodeCell.IsCollapsedChanged -= OnIsCollapsedChanged;
            }
            else
            {
                base.UnbindCellContent(cellType, range, cellContent);
            }
        }
        private void OnIsCollapsedChanged(object sender, EventArgs e)
        {
            var nodeCell = sender as NodeCell;
            var groupRow = nodeCell.Tag as GridGroupRow;
            groupRow.IsCollapsed = nodeCell.IsCollapsed;
        }

    }

}
