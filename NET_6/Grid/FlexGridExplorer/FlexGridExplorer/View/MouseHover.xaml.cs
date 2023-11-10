using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FlexGridExplorer
{
    public sealed partial class MouseHover : UserControl
    {
        public MouseHover()
        {
            this.InitializeComponent();
            Tag = AppResources.MouseHoverDescription;

            grid.ItemsSource = Customer.GetCustomerList(1000);
            //grid.Columns.RemoveAt(1);
            Dictionary<int, string> dct = new Dictionary<int, string>();
            foreach (var country in Customer.GetCountries())
            {
                dct[dct.Count] = country.Value;
            }
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = dct, SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.MinColumnWidth = 85;

            cbMouseOverMode.Items.Add(GridMouseOverMode.None);
            cbMouseOverMode.Items.Add(GridMouseOverMode.Selection);
            cbMouseOverMode.Items.Add(GridMouseOverMode.Cell);
            cbMouseOverMode.Items.Add(GridMouseOverMode.Row);
            cbMouseOverMode.Items.Add(GridMouseOverMode.Column);
            cbMouseOverMode.SelectedIndex = 1;

            cbSelectionMode.Items.Add(GridSelectionMode.None);
            cbSelectionMode.Items.Add(GridSelectionMode.Cell);
            cbSelectionMode.Items.Add(GridSelectionMode.CellRange);
            cbSelectionMode.Items.Add(GridSelectionMode.Row);
            cbSelectionMode.Items.Add(GridSelectionMode.RowRange);
            cbSelectionMode.Items.Add(GridSelectionMode.Column);
            cbSelectionMode.Items.Add(GridSelectionMode.ColumnRange);
            cbSelectionMode.SelectedIndex = 3;
        }
    }
}
