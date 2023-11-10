using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class StarSizing : UserControl
    {
        public StarSizing()
        {
            this.InitializeComponent();
            Tag = AppResources.StarSizingDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
        }
    }
}
