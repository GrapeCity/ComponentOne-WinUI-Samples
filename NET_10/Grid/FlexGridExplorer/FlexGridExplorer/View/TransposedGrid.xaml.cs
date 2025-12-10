using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class TransposedGrid : UserControl
    {
        public TransposedGrid()
        {
            InitializeComponent();
            Tag = AppResources.TransposedGridDescription;

            grid.ItemsSource = Customer.GetCustomerList(10);
        }
    }
}
