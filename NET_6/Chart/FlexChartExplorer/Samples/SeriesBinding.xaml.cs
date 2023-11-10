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
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace FlexChartExplorer.Samples
{
#pragma warning disable 1591
    public partial class SeriesBinding : UserControl
    {
        List<Point> _data1;
        List<Point> _data2;

        public SeriesBinding()
        {
            this.InitializeComponent();
        }

        public List<Point> Data1
        {
            get
            {
                if (_data1 == null)
                    _data1 = CreateData(160, x => 2 * Math.Sin(0.16 * x), y => 2 * Math.Cos(0.12 * y));
                return _data1;
            }
        }

        public List<Point> Data2
        {
            get
            {
                if (_data2 == null)
                    _data2 = CreateData(160, x => Math.Sin(0.1 * x), y => Math.Cos(0.15 * y));
                return _data2;
            }
        }

        List<Point> CreateData(int npts, Func<double, double> fx, Func<double,double> fy) 
        {
            List<Point> data = new (npts);
            for(int i = 0; i<npts; i++)
                data.Add(new Point(fx(i), fy(i)));
            return data;
        }
    }
}
