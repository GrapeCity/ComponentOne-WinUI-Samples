using C1.Chart;
using FlexChartExplorer.Data;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class AxisGroups : UserControl
    {
        DataService dataService = DataService.GetService();

        public AxisGroups()
        {
            this.InitializeComponent();
        }

        #region Data

        public AxisGroupSeparator[] GroupSeparators => new[] { 
            AxisGroupSeparator.None, AxisGroupSeparator.Horizontal, 
            AxisGroupSeparator.Vertical, AxisGroupSeparator.Grid };

        List<Data.GdpData> data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData().Take(8).ToList();

        #endregion
    }

}
