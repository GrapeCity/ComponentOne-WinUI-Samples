using C1.Chart;
using C1.WinUI.Chart;
using C1.WinUI.Diagram;
using DiagramExplorer.Data;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class OrgChart : UserControl, IDiagramHolder
    {
        public OrgChart()
        {
            InitializeComponent();

            var data = DataService.GetOrgChartData();

            var fontFamily = new FontFamily("Segoe UI");

            var nodeStyle = new ChartStyle() { FontFamily = fontFamily, FontSize = 12 };
            var nodeContentStyle = new ChartStyle()
            {
                FontFamily = fontFamily,
                FontSize = 10,
                //FontStyle = FontStyles.Italic,
                StrokeThickness = 0f,
            };

            var headerStyle = new ChartStyle()
            {
                FontFamily = fontFamily,
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                StrokeThickness = 0,
            };

            diagram.NodeCreated += (s, a) =>
            {
                var orgNode = a.Data as OrgNode;
                var node = a.Node as C1.WinUI.Diagram.Node;

                if (orgNode == null || node == null)
                    return;

                if (!string.IsNullOrEmpty(orgNode.FirstName))
                {
                    node.Title = $"{orgNode.FirstName}\n{orgNode.LastName}";
                    node.Content = orgNode.JobTitle;

                    node.TitleImage = orgNode.Image;
                    node.TitleImageSize = new Size(60, 80);
                    node.TitleStyle = nodeStyle;
                    node.NodeStyle = nodeContentStyle;
                }
                else
                {
                    node.Title = orgNode.Name;
                    node.TitleStyle = node.NodeStyle = headerStyle;
                }

                node.LegendItem = orgNode.Department;
                node.Shape = C1.Diagram.Shape.RoundedRectangle;
            };

            diagram.ItemsSource = data;
        }

        public FlexDiagram Diagram => diagram;
    }
}
