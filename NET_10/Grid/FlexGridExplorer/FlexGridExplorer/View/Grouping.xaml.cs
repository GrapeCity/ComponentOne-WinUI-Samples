using C1.DataCollection;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace FlexGridExplorer
{
    public sealed partial class Grouping : UserControl
    {
        public Grouping()
        {
            this.InitializeComponent();
            Tag = AppResources.GroupingDescription;
            UpdateVideos();
        }

        private void UpdateVideos()
        {
            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = new ObservableCollection<Customer>(data);
            _ = grid.DataCollection.GroupAsync("Country");
            grid.MinColumnWidth = 85;
        }
    }
}
