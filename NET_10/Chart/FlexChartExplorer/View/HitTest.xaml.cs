using C1.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class HitTest : UserControl
    {
        public HitTest()
        {
            this.InitializeComponent();
        }

        void Chart_PointerExited(object sender, PointerRoutedEventArgs args)
        {
            infoBar.Message = DefaultMessage;
        }

        void Chart_PointerMoved(object sender, PointerRoutedEventArgs args)
        {
            var hitTestInfo = flexChart.HitTest(args.GetCurrentPoint(flexChart).Position);

            if (hitTestInfo != null)
            {
                infoBar.Message = $"Chart element: {hitTestInfo.ChartElement}";
                if (hitTestInfo.Distance < 10 && hitTestInfo.PointIndex >= 0)
                    infoBar2.Message = $"Series: {hitTestInfo.Series.Name} | Point Index: {hitTestInfo.PointIndex} | X: {hitTestInfo.X} | Y: {hitTestInfo.Y:p0}";
                else
                    infoBar2.Message = "";

            }
            else
                infoBar.Message = "No info";
        }

        #region Data
        const string DefaultMessage = "Move pointer over chart to see hit test results.";

        public string Message { get; set; } = DefaultMessage;

        static Random rnd = new();
        List<object> _data;

        public static List<object> CreateData()
        {
            var data = new List<Object>();
            for (int year = 2000; year <= 2017; year++)
            {
                var count = year - 1999;
                data.Add(new
                {
                    Year = year,
                    TV = rnd.Next(71 - count / 2, 71 + count / 2) / 100f,
                    Newspaper = rnd.Next(40 - count / 3, 40 - count / 3 + 10) / 100f,
                    Radio = rnd.Next(30 - count, 30 - count + 3) / 100f,
                    Internet = rnd.Next(count * 3, count * 3 + 10) / 100f,
                });
            }
            return data;
        }

        public List<object> Data => _data != null ? _data : _data = CreateData();

        #endregion
    }
}
