using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

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
            grid.MinColumnWidth = 85;

            segmentsRule.Ranges = "[OrderCount]";
        }
    }
}
