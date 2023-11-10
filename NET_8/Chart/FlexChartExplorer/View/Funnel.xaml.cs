using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using static FlexChartExplorer.Radar;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Funnel : UserControl
    {
        List<CategoricalPoint> _data;

        public Funnel()
        {
            this.InitializeComponent();
        }

        public List<FunnelChartType> ChartTypes => new() { FunnelChartType.Default, FunnelChartType.Rectangle };

        #region Data

        public class CategoricalPoint
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public List<CategoricalPoint> Data => _data == null ? _data = GetRecruitmentData() : _data;

        public static List<CategoricalPoint> GetRecruitmentData()
        {
            var data = new List<CategoricalPoint>();

            data.Add(new CategoricalPoint { Name = "Candidates Applied", Value = 250 });
            data.Add(new CategoricalPoint { Name = "Initial Validation", Value = 145 });
            data.Add(new CategoricalPoint { Name = "Screening", Value = 105 });
            data.Add(new CategoricalPoint { Name = "Telephonic Interview", Value = 85 });
            data.Add(new CategoricalPoint { Name = "Personal Interview", Value = 48 });
            data.Add(new CategoricalPoint { Name = "Hired", Value = 18 });
            return data;
        }
        #endregion
    }
}
