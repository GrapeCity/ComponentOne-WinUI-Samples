using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class FilterRow : UserControl
    {
        public FilterRow()
        {
            this.InitializeComponent();
            Tag = AppResources.FilterRowDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
        }
    }
}
