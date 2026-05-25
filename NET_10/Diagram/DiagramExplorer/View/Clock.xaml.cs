using C1.Chart;
using C1.Diagram;
using C1.WinUI.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Clock : UserControl, IDiagramHolder
    {
        public Clock()
        {
            InitializeComponent();

            timer.Tick += (s, e) => UpdateTimeDiagram(diagram);

            Loaded += (s, e) => timer.Start();
            Unloaded += (s, e) => timer.Stop();
        }

        public FlexDiagram Diagram => diagram;

        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };

        static void UpdateTimeDiagram(FlexDiagram diagram)
        {
            diagram.BeginUpdate();

            var nodes = diagram.Nodes;
            var edges = diagram.Edges;
            nodes.Clear();
            edges.Clear();

            var time = DateTime.Now;

            var styleActive = new ChartStyle() { Stroke = diagram.Foreground, StrokeThickness = 2 };
            var styleBlink = new ChartStyle() { Fill = (time.Second % 2) == 0 ? diagram.Foreground : diagram.Background, StrokeThickness = 2 };

            var minuteNode = new Node() { Shape = Shape.Circle, NodeStyle = styleBlink };
            nodes.Add(minuteNode);
            var secondNode = new Node() { Shape = Shape.Circle, NodeStyle = styleBlink };
            nodes.Add(secondNode);

            for (var j = 0; j < 10; j++)
                AddTimeNode(diagram, null, minuteNode, time, (t) => t.Hour, "h", j, styleActive);

            for (var j = 0; j < 10; j++)
                AddTimeNode(diagram, minuteNode, secondNode, time, (t) => t.Minute, "m", j, styleActive);

            for (var j = 0; j < 10; j++)
                AddTimeNode(diagram, secondNode, null, time, (t) => t.Second, "s", j, styleActive);

            diagram.EndUpdate();
        }

        private static void AddTimeNode(FlexDiagram diagram, Node? from, Node? to, DateTime time, Func<DateTime, int> getValue,
            string suffix, int i, ChartStyle styleActive)
        {
            var val = getValue(time);
            var node = new Node() { Title = $"{10 * (val / 10) + i: 00}{suffix}", Shape = Shape.RoundedRectangle };

            var pos = 1 - (double)Math.Abs((i - (val % 10)) / 9.0);
            var color = Common.Samples.Interpolate(System.Drawing.Color.White, System.Drawing.Color.Black, pos * pos * pos);

            var active = val % 10 == i;
            node.NodeStyle = node.TitleStyle = active ? styleActive : new ChartStyle()
            {
                Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B)),
            };

            diagram.Nodes.Add(node);

            if (from != null)
            {
                diagram.Edges.Add(new Edge()
                {
                    Source = from,
                    Target = node,
                    EdgeStyle = node.TitleStyle,
                    TargetArrow = active ? ArrowStyle.Normal : ArrowStyle.None
                });
            }

            if (to != null)
            {
                diagram.Edges.Add(new Edge()
                {
                    Source = node,
                    Target = to,
                    EdgeStyle = node.TitleStyle,
                    TargetArrow = active ? ArrowStyle.Normal : ArrowStyle.None
                });
            }
        }

    }
}
