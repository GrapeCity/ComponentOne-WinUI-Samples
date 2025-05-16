using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FlexChartExplorer.Data;
using C1.WinUI.Chart;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class RangeSelector : UserControl
    {
        DataService dataService = DataService.GetService();

        public RangeSelector()
        {
            this.InitializeComponent();
        }


        #region Data

        List<Quote> _data;

        public List<Quote> Data => _data!=null ? _data : _data = dataService.CreateFinancialData(new DateTime(2022,1,1), 365);

        #endregion

        private void rangeSelector_UpperValueChanged(object sender, EventArgs e)
        {
            flexChart.AxisX.Max = rangeSelector.UpperValue;
        }

        private void rangeSelector_LowerValueChanged(object sender, EventArgs e)
        {
            flexChart.AxisX.Min = rangeSelector.LowerValue;
        }

        private void rangeSelector_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            rangeSelector.LowerValue = Data.First().Date.ToOADate();
            rangeSelector.UpperValue = Data.Last().Date.ToOADate();
        }
    }
}
