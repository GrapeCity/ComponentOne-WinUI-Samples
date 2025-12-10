using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class AxisLabels : UserControl
    {
        public AxisLabels()
        {
            this.InitializeComponent();
        }

        public int[] StaggeredLines => new[] { 1, 2, 3, 4 };
        public OverlappingLabels[] OverlapOptions => new[] { OverlappingLabels.Trim, OverlappingLabels.WordWrap };

        public object[] Data => GdpData.GetData();
    }

    #region Data
    class GdpData
    {
        public static object[] GetData()
        {
            return new object[]
            {
                new { Country = "United States", Gdp =  25462700 },
                new { Country = "China", Gdp = 17963171 },
                new { Country = "European Union", Gdp = 16600000 },
                new { Country = "Japan", Gdp = 4231141 },
                new { Country = "Germany", Gdp = 4072192 },
                new { Country = "India", Gdp = 3385090 },
                new { Country = "United Kingdom", Gdp = 3070668 },
                new { Country = "France", Gdp = 2782905 },
                new { Country = "Russia", Gdp = 2240422 },
                new { Country = "Canada", Gdp = 2139840 },
                new { Country = "Italy", Gdp = 2010432 },
                new { Country = "Brazil", Gdp = 1920096 },
                new { Country = "South Korea", Gdp = 1665246 },
            };
        }
    }
    #endregion
}
