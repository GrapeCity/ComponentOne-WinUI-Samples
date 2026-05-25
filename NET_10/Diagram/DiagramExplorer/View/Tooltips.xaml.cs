using C1.Chart;
using C1.WinUI.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Tooltips : UserControl, IDiagramHolder
    {
        public Tooltips()
        {
            InitializeComponent();

            CreateTooltipsDiagram(diagram);
        }

        public FlexDiagram Diagram => diagram;

        public void CreateTooltipsDiagram(FlexDiagram diagram)
        {
            var nodes = diagram.Nodes;
            var edges = diagram.Edges;

            var titleStyle = new ChartStyle()
            {
                Stroke = new SolidColorBrush(Microsoft.UI.Colors.White),
                FontSize = 16
            };
            var redNode = new Node()
            {
                Title = "Red",
                Shape = C1.Diagram.Shape.RoundedRectangle,
                NodeStyle = new ChartStyle() { Fill = new SolidColorBrush(Microsoft.UI.Colors.Red) },
                TitleStyle = titleStyle,
            };
            var greenNode = new Node()
            {
                Title = "Green",
                Shape = C1.Diagram.Shape.RoundedRectangle,
                NodeStyle = new ChartStyle() { Fill = new SolidColorBrush(Microsoft.UI.Colors.Green) },
                TitleStyle = titleStyle
            };
            var blueNode = new Node()
            {
                Title = "Blue",
                Shape = C1.Diagram.Shape.RoundedRectangle,
                NodeStyle = new ChartStyle() { Fill = new SolidColorBrush(Microsoft.UI.Colors.Blue) },
                TitleStyle = titleStyle
            };

            nodes.Add(redNode);
            nodes.Add(greenNode);
            nodes.Add(blueNode);

            var redNodes = new List<Node>();
            var greenNodes = new List<Node>();
            var blueNodes = new List<Node>();

            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if ((int)color >= 28 && (int)color <= 167)
                {
                    var c = System.Drawing.Color.FromKnownColor(color);
                    var node = new Node()
                    {
                        Tooltip = $"{color}\nR:{c.R}\nG:{c.G}\nB:{c.B}",
                        Shape = C1.Diagram.Shape.Circle,
                        DataContext = c
                    };

                    var s = color.ToString();
                    if (s.Contains("Red"))
                        redNodes.Add(node);
                    else if (s.Contains("Blue"))
                        blueNodes.Add(node);
                    else if (s.Contains("Green"))
                        greenNodes.Add(node);
                }
            }
            ;

            redNodes.Sort((c1, c2) => (((Color)c1.DataContext).R < ((Color)c2.DataContext).R) ? -1 : 1);
            greenNodes.Sort((c1, c2) => (((Color)c1.DataContext).G < ((Color)c2.DataContext).G) ? -1 : 1);
            blueNodes.Sort((c1, c2) => (((Color)c1.DataContext).B < ((Color)c2.DataContext).B) ? -1 : 1);

            foreach (var node in redNodes)
                AddColorNode(diagram, redNode, node);

            foreach (var node in greenNodes)
                AddColorNode(diagram, greenNode, node);

            foreach (var node in blueNodes)
                AddColorNode(diagram, blueNode, node);

            var emptyStyle = new ChartStyle() { Stroke = new SolidColorBrush(Microsoft.UI.Colors.Transparent) };

            edges.Add(new Edge()
            {
                Source = redNodes[redNodes.Count / 2],
                Target = greenNode,
                EdgeStyle = emptyStyle
            });
            edges.Add(new Edge()
            {
                Source = greenNodes[greenNodes.Count / 2],
                Target = blueNode,
                EdgeStyle = emptyStyle
            });
        }

        private static void AddColorNode(FlexDiagram diagram, Node parent, Node node)
        {
            var c = (Color)node.DataContext;
            var brush = new SolidColorBrush(Windows.UI.Color.FromArgb(c.A, c.R, c.G, c.B));
            var edgeStyle = new ChartStyle() { Fill = brush, Stroke = brush, StrokeThickness = 2 };
            node.NodeStyle = new ChartStyle() { StrokeThickness = 0.1f, Fill = brush };
            diagram.Nodes.Add(node);
            diagram.Edges.Add(new Edge()
            {
                Source = parent,
                Target = node,
                EdgeStyle = edgeStyle,
                TargetArrow = C1.Chart.ArrowStyle.Normal,
                Tooltip = $"{parent.Title} -> {c.Name}"
            });
        }

    }
}
