using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FlexGridExplorer
{
    public sealed partial class ColumnDefinitions : UserControl
    {
        public ColumnDefinitions()
        {
            this.InitializeComponent();
            Tag = AppResources.ColumnDefinitionDescription;
            var data = Customer.GetCustomerList(1000);
            grid.ItemsSource = data;
            grid.Columns.RemoveAt(1);
            Dictionary<int, string> dct = new Dictionary<int, string>();
            foreach (var country in Customer.GetCountries())
            {
                dct[dct.Count] = country.Value;
            }
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = dct, SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.MinColumnWidth = 85;
        }
    }
}
