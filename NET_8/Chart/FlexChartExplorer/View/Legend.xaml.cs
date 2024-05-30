using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Legend : UserControl
    {
        public Legend()
        {
            this.InitializeComponent();
        }

        public object[] Data => ClimateData.GetData();

        public Position[] Positions => new[] 
            { 
                Position.Left, Position.LeftTop, Position.LeftBottom,
                Position.Right, Position.RightTop, Position.RightBottom,
                Position.Top, Position.TopLeft, Position.TopRight,
                Position.Bottom, Position.BottomLeft, Position.BottomRight
            };

        public C1.Chart.Orientation[] Orientations => new[] {
            C1.Chart.Orientation.Auto, C1.Chart.Orientation.Horizontal, C1.Chart.Orientation.Vertical };
    }
}
