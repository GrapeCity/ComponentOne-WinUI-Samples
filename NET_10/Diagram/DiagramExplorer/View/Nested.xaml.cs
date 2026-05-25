using C1.Chart;
using C1.WinUI.Diagram;
using DiagramExplorer.Common;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Nested : UserControl, IDiagramHolder
    {
        public Nested()
        {
            InitializeComponent();
        }

        public FlexDiagram Diagram => diagram;

        private void FlexDiagram_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var diagram = sender as FlexDiagram;

            var node = diagram.DataContext as Node;

            if (node.Title != "System")
                Samples.LoadDiagramFromResource(diagram, $"{node.ID}.mermaid");
            else
                diagram.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }

        private void TextBlock_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var node = textBlock.DataContext as Node;

            if (node.Title == "System")
                textBlock.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }

        private void Expander_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var expander = sender as Expander;
            var node = expander.DataContext as Node;
            if (node.Title == "System")
                expander.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }
}
