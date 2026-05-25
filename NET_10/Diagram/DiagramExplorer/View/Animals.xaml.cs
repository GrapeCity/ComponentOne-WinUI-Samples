using C1.Chart;
using C1.Diagram;
using C1.WinUI.Diagram;
using DiagramExplorer.Data;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Animals : UserControl, IDiagramHolder
    {
        public Animals()
        {
            InitializeComponent();

            diagram.NodeCreated += (s, args) =>
            {
                var node = (Node)args.Node;
                var animal = args.Data as Animal;

                node.Title = animal != null ? animal.Name : args.Data.ToString();

                if (animal != null && animal.Image != null)
                {
                    node.TitleImage = animal.Image;
                    node.TitleImageSize = new Windows.Foundation.Size(64, 64);
                    node.TitleDirection = Direction.Vertical;
                }

                node.Shape = Shape.RoundedRectangle;
                node.LegendItem = args.ParentNode == null ? args.Data?.ToString() : args.ParentNode.LegendItem;
            };

            diagram.ItemsSource = Data.DataService.GetService().GetAnimalData();
        }

        public FlexDiagram Diagram => diagram;
    }
}
