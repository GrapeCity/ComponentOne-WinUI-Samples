using C1.Chart;
using FlexChartExplorer.Data;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class ErrorBars : UserControl
    {
        public ErrorBars()
        {
            this.InitializeComponent();
        }

        public ChartType[] ChartTypes => new[] { 
            ChartType.Column, ChartType.Bar, ChartType.Line, ChartType.LineSymbols,
        };

        public ErrorBarDirection[] Directions => new[] { ErrorBarDirection.Both, ErrorBarDirection.Minus, ErrorBarDirection.Plus };
        public ErrorBarEndStyle[] EndStyles => new[] { ErrorBarEndStyle.Cap, ErrorBarEndStyle.NoCap };

        public ErrorAmount[] ErrorAmounts => new[] { ErrorAmount.FixedValue, ErrorAmount.Percentage };

        private void cbErrorAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((ErrorAmount)cbErrorAmount.SelectedValue)
            {
                case ErrorAmount.FixedValue:
                    errorBar.ErrorValue = 50;
                    break;
                case ErrorAmount.Percentage:
                    errorBar.ErrorValue = 0.10; // 10%
                    break;
            }
        }

        #region Data
        DataService dataService = DataService.GetService();

        List<CountrySalesData> _data;

        public List<CountrySalesData> Data => _data != null ? _data : _data = dataService.GetCountrySales();

        #endregion
    }
}
