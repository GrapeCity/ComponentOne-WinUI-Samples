using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ListViewExplorer
{
    public sealed partial class CoreView : UserControl
    {
        public CoreView()
        {
            InitializeComponent();
            this.DataContext = new SampleDataSource();
            themes.Items.Add("Default");
            themes.Items.Add("Light");
            themes.Items.Add("Dark");
            Loaded += CoreView_Loaded;
        }

        private void OnRootLoaded(object sender, RoutedEventArgs e)
        {
            themes.SelectedIndex = (int)(root.XamlRoot.Content as FrameworkElement).RequestedTheme;
        }

        private void CoreView_Loaded(object sender, RoutedEventArgs e)
        {
            lbSamples.SelectedItem = lbSamples.Items[0];
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (root.XamlRoot.Content as FrameworkElement).RequestedTheme = (ElementTheme)themes.SelectedIndex;
        }

        private void lbSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1) return;
            SampleItem selectedItem = e.AddedItems[0] as SampleItem;
            grid.DataContext = selectedItem;
        }
    }
}
