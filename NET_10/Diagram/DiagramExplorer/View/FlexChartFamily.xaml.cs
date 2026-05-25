using C1.Chart;
using C1.Diagram;
using C1.WinUI.Diagram;
using DiagramExplorer.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class FlexChartFamily : UserControl, IDiagramHolder
    {
        public FlexChartFamily()
        {
            InitializeComponent();

            CreateFlexChartFamily(diagram);
        }

        public FlexDiagram Diagram => diagram;

        public static async Task CreateFlexChartFamily(FlexDiagram diagram)
        {
            diagram.BeginUpdate();

            diagram.FontSize = 12;
            //diagram.Header.Content = "FlexChart Family";

            var nodes = diagram.Nodes;
            var edges = diagram.Edges;

            var w = 120;
            var h = 80;

            nodes.Add(new Node() { Text = "📈 FlexChart", Content = "6 Charting Components" });// 0
            nodes.Add(new Node() { Text = "📋 Table-based Data" }); // 1
            nodes.Add(new Node() { Appearance = NodeAppearance.Hidden });//
            nodes.Add(new Node() { Text = "🌳 Hierarchical Data" }); // 2

            nodes.Add(new Node() { Text = "📈 FlexChart", TitleImage = await Samples.CreateFlexChart(w, h) });//3
            nodes.Add(new Node() { Text = "🥧 FlexPie", TitleImage = await Samples.CreateFlexPie(w, h) });// 4
            nodes.Add(new Node()
            {
                Text = "📡 FlexRadar",
                TitleImage = await Samples.CreateRadarChart(w, h)
            }); // 5

            nodes.Add(new Node()
            {
                Text = "🧱 TreeMap",
                TitleImage = await Samples.CreateTreeMap(w, h)
            }); // 6
            nodes.Add(new Node()
            {
                Text = "💥 Sunburst",
                TitleImage = await Samples.CreateSunburstChart(w, h)
            });// 7
            nodes.Add(new Node()
            {
                Text = "🔀 FlexDiagram",
                TitleImage = await Samples.CreateDiagram(w, h)
            });// 7

            edges.Add(new Edge() { Source = nodes[0], Target = nodes[1] });
            edges.Add(new Edge() { Source = nodes[0], Target = nodes[2] });
            edges.Add(new Edge() { Source = nodes[0], Target = nodes[3] });

            edges.Add(new Edge() { Source = nodes[1], Target = nodes[4] });
            edges.Add(new Edge() { Source = nodes[1], Target = nodes[5] });
            edges.Add(new Edge() { Source = nodes[1], Target = nodes[6] });

            edges.Add(new Edge() { Source = nodes[3], Target = nodes[7] });
            edges.Add(new Edge() { Source = nodes[3], Target = nodes[8] });
            edges.Add(new Edge() { Source = nodes[3], Target = nodes[9] });

            foreach (var node in nodes)
            {
                node.TitleDirection = Direction.Vertical;
                node.TitleOrder = LabelOrder.TextImage;
                node.Shape = Shape.RoundedRectangle;
            }
            diagram.ScaleMode = ScaleMode.ScaleToFit;

            diagram.EndUpdate();
        }
    }
}
