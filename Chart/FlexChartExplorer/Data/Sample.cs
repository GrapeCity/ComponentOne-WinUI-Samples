using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexChartExplorer.Data
{
#pragma warning disable 1591
    public class Sample
    {
        public string Name { get; set; }
        public UserControl Page { get; set; }

        public List<Sample> Children { get; set; }
    }

    public class SamplesList
    {
        public static List<Sample> GetSamples()
        {
            return new List<Sample>()
            {
                new Sample() { Name = "Intro", Page = new Samples.Intro() },
                new Sample() { Name = "Binding", Page = new Samples.Binding() },
                new Sample() { Name = "Series Binding", Page = new Samples.SeriesBinding() },
                new Sample() { Name = "Image Export", Page = new Samples.ImageExport() },
                new Sample() { Name = "Two Y Axes", Page = new Samples.TwoYAxes() },
                new Sample() { Name = "Pie Chart", Page = new Samples.PieChart() },
                new Sample() { Name = "Tree Map", Page = new Samples.TreeMap() },
            };
        }
    }
}
