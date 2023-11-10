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
    public sealed partial class LineMarker : UserControl
    {
        DataService dataService = DataService.GetService();

        public LineMarker()
        {
            this.InitializeComponent();
        }

        void PositionChanged(object sender, PositionChangedArgs e)
        {
            var pt = e.Position;
            var dataPoint = flexChart.PointToData(e.Position);
            var date = DateTime.FromOADate(dataPoint.X);
            var i = (int)(date.Date - Data[0].Date).TotalDays;
            if (i >= 0 && i < Data.Count)
            {
                var item = Data[i];
                marker.Content = 
                    $"Date: {item.Date.ToShortDateString()}\n" +
                    $"O{item.Open} H{item.High} L{item.Low} C{item.Close}";
            }
        }


        #region Data

        List<Quote> _data;

        public List<Quote> Data => _data!=null ? _data : _data = dataService.CreateFinancialData(new DateTime(2022,1,1), 365);

        #endregion
    }
}
