using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class NewRow : UserControl
    {
        public NewRow()
        {
            this.InitializeComponent();

            Tag = AppResources.NewRowDescription;
            grid.ItemsSource = new CustomDataCollection<Customer>(Customer.GetCustomerList(100));
            grid.MinColumnWidth = 85;
        }
    }
}
