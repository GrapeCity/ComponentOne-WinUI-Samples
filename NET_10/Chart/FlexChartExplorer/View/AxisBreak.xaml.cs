using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class AxisBreak : UserControl
    {
        Point[] _data;
        ChartType[] _chartTypes;

        public AxisBreak()
        {
            this.InitializeComponent();

            cbAxisBreak.IsChecked = true;
        }

        public ChartType[] ChartTypes => new[] { ChartType.Column, ChartType.Line, ChartType.LineSymbols, ChartType.Area };

        public Point[] Data => _data == null ? _data = CreateData() : _data;

        Point[] CreateData()
        {
            var rnd = new Random();
            var pts = new Point[20];

            for (var i = 0; i < pts.Length; i++)
            {
                if (rnd.NextDouble() < 0.85)
                    pts[i] = new Point(i, rnd.Next(0, 10));
                else
                    pts[i] = new Point(i, 50 + rnd.Next(0, 50));
            }

            return pts;
        }

        private void cbAxisBreak_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (cbAxisBreak.IsChecked == true)
            {
                if (flexChart.Rotated)
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisX, 0, 10, 50, 100);
                else
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisY, 0, 10, 50, 100);
            }
            else
                AxisBreakHelper.Remove(flexChart);
        }

        private void cbRotated_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            flexChart.Rotated = cbRotated.IsChecked == true;
            flexChart.AxisX.MajorGrid = flexChart.Rotated;
            flexChart.AxisY.MajorGrid = !flexChart.Rotated;

            if (cbAxisBreak.IsChecked == true)
            {
                if (flexChart.Rotated)
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisX, 0, 10, 50, 100);
                else
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisY, 0, 10, 50, 100);
            }
        }
    }
}
