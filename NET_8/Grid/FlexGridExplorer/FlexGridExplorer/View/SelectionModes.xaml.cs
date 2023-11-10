using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;

namespace FlexGridExplorer
{
    public sealed partial class SelectionModes : UserControl
    {
        public SelectionModes()
        {
            this.InitializeComponent();
            Tag = AppResources.SelectionModesDescription;

            foreach (var value in Enum.GetValues(typeof(GridSelectionMode)))
            {
                selectionMode.Items.Add(value.ToString());
            }
            selectionMode.SelectedIndex = selectionMode.Items.IndexOf(GridSelectionMode.CellRange.ToString());
            lblShowMarquee.Text = AppResources.ShowMarquee;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }

        private void selectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grid.SelectionMode = (GridSelectionMode)Enum.Parse(typeof(GridSelectionMode), selectionMode.Items[selectionMode.SelectedIndex].ToString());
        }

        private void grid_SelectionChanging(object sender, GridCellRangeEventArgs e)
        {
            // e.Cancel = true;
        }

        private void grid_SelectionChanged(object sender, GridCellRangeEventArgs e)
        {
            if (e.CellRange != null && e.CellRange.Row != -1)
            {
                int rowsSelected = Math.Abs(e.CellRange.Row2 - e.CellRange.Row) + 1;
                int colsSelected = Math.Abs(e.CellRange.Column2 - e.CellRange.Column) + 1;

                lblSelection.Text = (rowsSelected * colsSelected).ToString() + " " + AppResources.CellsSelectedText;
            }
            else
            {
                lblSelection.Text = "";
            }
        }
    }
}
