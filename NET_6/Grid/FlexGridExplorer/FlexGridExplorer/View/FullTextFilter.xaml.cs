using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class FullTextFilter : UserControl
    {
        public FullTextFilter()
        {
            this.InitializeComponent();
            Tag = AppResources.FullTextFilterDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }
    }
}
