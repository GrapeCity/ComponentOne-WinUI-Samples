using FlexChartExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
            Title = "WinUI FlexChart Explorer";

            root.DataContext = new SampleDataSource();
            themes.Items.Add(AppResources.Default);
            themes.Items.Add(AppResources.Light);
            themes.Items.Add(AppResources.Dark);
        }

        private void OnRootLoaded(object sender, RoutedEventArgs e)
        {
            themes.SelectedIndex = (int)(root.XamlRoot.Content as FrameworkElement).RequestedTheme;

            treeViewSamples.SelectedNode = treeViewSamples.RootNodes[0];
            grid.DataContext = treeViewSamples.RootNodes[0].Content as ISampleItem;
        }

        private void lbSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1) 
                return;
            grid.DataContext = e.AddedItems[0] as ISampleItem;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (root.XamlRoot.Content as FrameworkElement).RequestedTheme = (ElementTheme)themes.SelectedIndex;
        }

        private void ItemInvoked(object sender, TreeViewItemInvokedEventArgs e)
        {
            var sample = e.InvokedItem as ISampleItem;

            if (sample?.Sample != null)
                grid.DataContext = sample;
            else
            {
                treeViewSamples.Expand(treeViewSamples.SelectedNode);
                //treeViewSamples.SelectedNode = treeViewSamples.SelectedNode.Children[0];
            }
        }
    }
}
