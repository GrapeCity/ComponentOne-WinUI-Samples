using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class DataLabels : UserControl
    {
        public DataLabels()
        {
            this.InitializeComponent();
        }

        public object[] Data => ClimateData.GetData();

        public ChartType[] ChartTypes => new[] 
            { ChartType.Column, ChartType.Line, ChartType.LineSymbols, ChartType.Spline, ChartType.SplineSymbols };

        public LabelPosition[] Positions => new[] { LabelPosition.None, LabelPosition.Top, 
            LabelPosition.Center, LabelPosition.Bottom, LabelPosition.Left, LabelPosition.Right };

        public int[] Angles => new[] { 0, 45, 90 };
    }

    #region Data
    // The climate of New York & Los Angeles
    // https://en.wikipedia.org/wiki/Climate_of_New_York
    // https://en.wikipedia.org/wiki/Climate_of_Los_Angeles
    class ClimateData
    {
        public static object[] GetData()
        {
            return new object[]
            {
                new { month = "Jan", NewYork =  0.9, LosAngeles = 14.7, Seattle =  6.0},
                new { month = "Feb", NewYork =  2.2, LosAngeles = 15.0, Seattle =  6.7},
                new { month = "Mar", NewYork =  6.0, LosAngeles = 16.2, Seattle =  8.4},
                new { month = "Apr", NewYork = 12.1, LosAngeles = 17.6, Seattle = 10.7},
                new { month = "May", NewYork = 17.3, LosAngeles = 18.8, Seattle = 14.2},
                new { month = "Jun", NewYork = 22.2, LosAngeles = 20.7, Seattle = 16.7},
                new { month = "Jul", NewYork = 25.3, LosAngeles = 22.9, Seattle = 19.5},
                new { month = "Aug", NewYork = 24.5, LosAngeles = 23.7, Seattle = 19.7},
                new { month = "Sep", NewYork = 20.7, LosAngeles = 23.1, Seattle = 17.0},
                new { month = "Oct", NewYork = 14.4, LosAngeles = 20.7, Seattle = 12.1},
                new { month = "Nov", NewYork =  8.9, LosAngeles = 17.2, Seattle =  8.1},
                new { month = "Dec", NewYork =  3.9, LosAngeles = 14.3, Seattle =  5.6}
            };
        }
    }
    #endregion
}
