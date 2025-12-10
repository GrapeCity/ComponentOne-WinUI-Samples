using C1.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Selection : UserControl
    {
        List<DataItem> _data;

        public Selection()
        {
            this.InitializeComponent();
        }

        void SelectionChanged(object sender, EventArgs args)
        {
            infoBar.Message = flexChart.SelectedSeries != null ?
                flexChart.SelectedIndex != -1 ? 
                    $"Selected Series: {flexChart.SelectedSeries.SeriesName}, Point Index: {flexChart.SelectedIndex}" 
                    : $"Selected Series: {flexChart.SelectedSeries.SeriesName}"
                : "Click on data to select";
        }

        #region Data

        public ChartType[] ChartTypes => new[] { ChartType.Column, ChartType.Bar, ChartType.LineSymbols, ChartType.Scatter };

        public IEnumerable<ChartSelectionMode> SelectionModes => 
            Enum.GetValues(typeof(ChartSelectionMode)).OfType<ChartSelectionMode>();

        public class DataItem
        {
            public string Month { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
        }

        public static List<DataItem> CreateData()
        {
            var result = new List<DataItem>();
            var rnd = new Random();
            for (var i = 1; i <= 6; i++)
                result.Add(new DataItem()
                {
                    Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i),
                    Sales = rnd.Next(30),
                    Expenses = rnd.Next(20),
                });
            return result;
        }

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                    _data = CreateData();

                return _data;
            }
        }

        #endregion
    }
}
