using C1.Chart;
using C1.WinUI.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class NodeTemplate : UserControl, IDiagramHolder
    {
        public NodeTemplate()
        {
            InitializeComponent();

            diagram.BeginUpdate();
            diagram.ChildItemsPath = "Childs";
            diagram.ItemsSource = new List<Data.TypeInfo> { new Data.TypeInfo(typeof(FlexChartBase), new[] { typeof(FlexDiagram).Assembly }) };
            diagram.EndUpdate();
        }

        public FlexDiagram Diagram => diagram;
    }
}
