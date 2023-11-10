using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public sealed partial class OnDemand : UserControl
    {
        YouTubeCollectionView _dataCollection;

        public OnDemand()
        {
            this.InitializeComponent();
            Tag = AppResources.OnDemandDescription;
            emptyListLabel.Text = AppResources.EmptyListText;

            _dataCollection = new YouTubeCollectionView() { PageSize = 50 };
            grid.ItemsSource = _dataCollection;
            search.Text = "WinUI";
            var task = PerformSearch();
        }

        private async Task PerformSearch()
        {
            try
            {
                activityIndicator.Visibility = Visibility.Visible;
                await _dataCollection.SearchAsync(search.Text);
            }
            finally
            {
                activityIndicator.Visibility = Visibility.Collapsed;
            }
        }

        private async void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            await PerformSearch();
        }
    }
}
