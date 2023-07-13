using C1.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace FlexChartExplorer.Samples
{
#pragma warning disable 1591
    public partial class TwoYAxes : UserControl
    {
        List<DataItem> _data;
        Random rnd = new Random();
        int npts = 12;

        public TwoYAxes()
        {
            this.InitializeComponent();
        }

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<DataItem>();
                    var dt = DateTime.Today;
                    for (var i = 0; i < npts; i++)
                    {
                        _data.Add(new DataItem()
                        {
                            Time = dt.AddMonths(i),
                            Precipitation = rnd.Next(30, 100),
                            Temperature = rnd.Next(7, 20)
                        });
                    }
                }

                return _data;
            }
        }

        public List<double> LabelAngles
        {
            get
            {
                return new List<double>() { -90, -45, 0, 45, 90 };
            }
        }

        public class DataItem
        {
            public DateTime Time { get; set; }
            public int Precipitation { get; set; }
            public int Temperature { get; set; }
        }
    }
}
