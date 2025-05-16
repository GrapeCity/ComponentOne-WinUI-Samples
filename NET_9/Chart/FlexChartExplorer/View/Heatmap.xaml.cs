using C1.Chart;
using C1.WinUI.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.UI;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Heatmap : UserControl
    {
        public Heatmap()
        {
            this.InitializeComponent();
        }

        private void ChartLoaded(object sender, RoutedEventArgs e)
        {
            if (chart.Series.Count > 0)
                return;

            chart.BeginUpdate();

            var scale = new GradientColorScale() { Min = -20, Max = 20 };
            scale.Colors = new List<Color> { Colors.Blue, Colors.White, Colors.Red };

            var hmap = new C1.WinUI.Chart.Heatmap();
            hmap.ItemsSource = new double[,] {
                {  3.0, 3.1, 5.7, 8.2, 12.5, 15.0, 17.1, 17.1, 14.3, 10.6, 6.6, 4.3 },
                { -9.3, -7.7, -2.2, 5.8, 13.1, 16.6, 18.2, 16.4, 11.0, 5.1, -1.2, -6.1},
                 { -15.1, -12.5, -5.2, 3.1, 10.1, 15.5, 18.3, 15.0, 9.4, 1.4, -5.6, -11.4},
                };
            hmap.ColorScale = scale;
            chart.Series.Add(hmap);

            chart.AxisX.ItemsSource = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames;
            chart.AxisY.ItemsSource = new string[] { "Amsterdam", "Moscow", "Perm" };

            chart.EndUpdate();
        }
    }
}
