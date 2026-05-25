using C1.Diagram;
using C1.WinUI.Diagram;
using DiagramExplorer.Common;
using DiagramExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
            Title = "WinUI FlexDiagram Explorer";

            root.DataContext = new SampleDataSource();
            themes.Items.Add(AppResources.Default);
            themes.Items.Add(AppResources.Light);
            themes.Items.Add(AppResources.Dark);

            comboBoxDirection.ItemsSource = Enum.GetValues<C1.Diagram.DiagramDirection>();
            comboBoxDirection.SelectionChanged += (s, e) =>
            {
                var diagram = CurrentDiagram;
                if (diagram != null)
                    diagram.Direction = (DiagramDirection)comboBoxDirection.SelectedItem;
            };

            comboBoxEdgeRouting.ItemsSource = Enum.GetValues<C1.Diagram.EdgeRouting>();
            comboBoxEdgeRouting.SelectionChanged += (s, e) =>
            {
                var diagram = CurrentDiagram;
                if (diagram != null)
                    diagram.EdgeRouting = (EdgeRouting)comboBoxEdgeRouting.SelectedItem;
            };

            comboBoxPalette.ItemsSource = Enum.GetValues<C1.Chart.Palette>();
            comboBoxPalette.SelectionChanged += (s, e) =>
            {
                var diagram = CurrentDiagram;
                if (diagram != null)
                    diagram.Palette = (C1.Chart.Palette)comboBoxPalette.SelectedItem;
            };
        }

        FlexDiagram CurrentDiagram => ((grid.DataContext as ISampleItem)?.Sample as IDiagramHolder)?.Diagram;

        private void OnRootLoaded(object sender, RoutedEventArgs e)
        {
            themes.SelectedIndex = (int)(root.XamlRoot.Content as FrameworkElement).RequestedTheme;

            treeViewSamples.SelectedNode = treeViewSamples.RootNodes[0];

            grid.DataContext = treeViewSamples.RootNodes[0].Content as ISampleItem;
        }

        //private void lbSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count < 1) 
        //        return;
        //    grid.DataContext = e.AddedItems[0] as ISampleItem;
        //}

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

            var diagram = CurrentDiagram;
            if (diagram != null)
            {
                comboBoxDirection.SelectedIndex = (int)diagram.Direction;
                comboBoxEdgeRouting.SelectedIndex = (int)diagram.EdgeRouting;
            }

            if (sample != null)
            {
                comboBoxDirection.Visibility = sample.Controls.Contains("Direction") ? Visibility.Visible : Visibility.Collapsed;
                comboBoxEdgeRouting.Visibility = sample.Controls.Contains("EdgeRouting") ? Visibility.Visible : Visibility.Collapsed;
                comboBoxPalette.Visibility = sample.Controls.Contains("Palette") ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
