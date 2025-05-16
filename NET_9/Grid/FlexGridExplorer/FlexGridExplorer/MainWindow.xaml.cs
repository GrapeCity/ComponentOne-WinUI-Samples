using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            root.DataContext = new SampleDataSource();
            themes.Items.Add("Default");
            themes.Items.Add("Light");
            themes.Items.Add("Dark");
        }

        private void OnRootLoaded(object sender, RoutedEventArgs e)
        {
            themes.SelectedIndex = (int)(root.XamlRoot.Content as FrameworkElement).RequestedTheme;
        }

        private void lbSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1) return;
            SampleItem selectedItem = e.AddedItems[0] as SampleItem;
            grid.DataContext = selectedItem;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (root.XamlRoot.Content as FrameworkElement).RequestedTheme = (ElementTheme)themes.SelectedIndex;
            //App.Current.RequestedTheme = themes.SelectedIndex == 0 ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }
    }
}
