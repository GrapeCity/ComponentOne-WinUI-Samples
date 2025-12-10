using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class AxisScrollbar : UserControl
    {
        DataService dataService = DataService.GetService();

        public AxisScrollbar()
        {
            this.InitializeComponent();
        }

        #region Data

        List<Quote> _data;

        public List<Quote> Data => _data!=null ? _data : _data = dataService.CreateFinancialData(DateTime.Today, 160);

        #endregion
    }
}
