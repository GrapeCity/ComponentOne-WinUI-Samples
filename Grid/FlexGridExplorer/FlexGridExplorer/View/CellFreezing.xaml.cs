using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FlexGridExplorer
{
    public sealed partial class CellFreezing : UserControl
    {
        public CellFreezing()
        {
            this.InitializeComponent();
            Tag = AppResources.CellFreezingDescription;

            var data = Customer.GetCustomerList(1000);
            grid.ItemsSource = data;
            Dictionary<int, string> dct = new Dictionary<int, string>();
            foreach (var country in Customer.GetCountries())
            {
                dct[dct.Count] = country.Value;
            }
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = dct, SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.Columns["CountryID"].AllowMerging = true;
            grid.MinColumnWidth = 85;
        }
    }
}
