using C1.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Dynamic : UserControl, IDiagramHolder
    {
        public Dynamic()
        {
            InitializeComponent();

            CreateDiagram();

            Loaded += (s, e) => timer.Start();
            Unloaded += (s, e) => timer.Stop();
        }

        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };
        Random random = new Random();

        void CreateDiagram()
        {
            var insects = "🐝,🦋,🐞,🐜,🐛,🦗,🦟,🐌".Split(",");

            timer.Tick += (s, e) =>
            {
                diagram.BeginUpdate();

                var nodes = diagram.Nodes;
                var edges = diagram.Edges;

                if (nodes.Count >= 30)
                {
                    nodes.Clear();
                    edges.Clear();
                }

                var text = insects[random.Next(0, insects.Length)].Trim();
                nodes.Add(new Node() { Title = text, LegendItem = text, Shape = C1.Diagram.Shape.Circle });

                var i = random.Next(0, nodes.Count - 1);
                if (i != nodes.Count - 1)
                    edges.Add(new Edge() { Source = nodes[i], Target = nodes[nodes.Count - 1] });
                i = random.Next(0, nodes.Count - 1);
                if (i != nodes.Count - 1)
                    edges.Add(new Edge() { Source = nodes[i], Target = nodes[nodes.Count - 1] });

                diagram.EndUpdate();
            };

            diagram.PointerReleased += (s, e) =>
            {
                if (timer.IsEnabled)
                    timer.Stop();
                else
                    timer.Start();
            };
        }

        public FlexDiagram Diagram => diagram;
    }
}
