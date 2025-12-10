using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class SelectedItems : UserControl
    {
        public SelectedItems()
        {
            this.InitializeComponent();
            
            Tag = AppResources.SelectedItemsDescription;

            grid.ItemsSource = Customer.GetCustomerList(100);
        }
    }
}
