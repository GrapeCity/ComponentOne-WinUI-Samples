using C1.Chart;
using FlexChartExplorer.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class LogAxes : UserControl
    {
        DataService dataService = DataService.GetService();

        public LogAxes()
        {
            this.InitializeComponent();
        }

        void Checked(object sender, RoutedEventArgs e)
        {
            if (flexChart == null)
                return;

            flexChart.AxisX.LogBase = flexChart.AxisY.LogBase = 10;
        }

        void Unchecked(object sender, RoutedEventArgs e)
        {
            if (flexChart == null)
                return;

            flexChart.AxisX.LogBase = flexChart.AxisY.LogBase = double.NaN;
        }

        #region Data

        List<Data.GdpData> data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData();

        #endregion
    }
}
