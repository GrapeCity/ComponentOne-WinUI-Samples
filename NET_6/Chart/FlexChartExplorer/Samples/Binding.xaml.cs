// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FlexChartExplorer.Samples
{
#pragma warning disable 1591
    public sealed partial class Binding : UserControl
    {
        int npts = 50;
        ChartType[] _chartTypes = new[] { ChartType.Line, ChartType.LineSymbols, ChartType.Area };
        List<DataItem> _data;
        Random rnd = new();

        public Binding()
        {
            this.InitializeComponent();
        }

        public ChartType[] ChartTypes => _chartTypes;

        public class DataItem
        {
            public int? Downloads { get; set; }
            public int? Sales { get; set; }
            public DateTime Date { get; set; }
        }

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<DataItem>();
                    var dateStep = 0;
                    for (var i = 0; i < npts; i++)
                    {
                        var date = DateTime.Today.AddDays(dateStep += 9);
                        _data.Add(new DataItem()
                        {
                            Downloads = date.Month == 4 || date.Month == 8 ? (int?)null : rnd.Next(10, 20),
                            Sales = date.Month == 4 || date.Month == 8 ? (int?)null : rnd.Next(0, 10),
                            Date = date
                        });
                    }
                }

                return _data;
            }
        }
    }
}
