using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            Tag = AppResources.GettingStartedDescription;

            grid.ItemsSource = Customer.GetCustomerList(100);
        }
    }
}
