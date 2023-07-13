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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FlexChartExplorer.Samples
{
#pragma warning disable 1591
    public sealed partial class Intro : UserControl
    {
        ChartType[] _chartTypes;
        List<FruitDataItem> _fruits;
        List<Palette> _palettes;
        List<Stacking> _stackings;

        public Intro()
        {
            this.InitializeComponent();
        }

        public ChartType[] ChartTypes
        {
            get
            {
                if (_chartTypes == null)
                {
                    _chartTypes = new[] { ChartType.Column, ChartType.Bar, ChartType.Line, ChartType.LineSymbols, 
                        ChartType.Scatter, ChartType.Area, 
                        ChartType.Spline, ChartType.SplineSymbols, ChartType.SplineArea,
                        ChartType.Step, ChartType.StepSymbols, ChartType.StepArea
                    };
                }

                return _chartTypes;
            }
        }

        public List<Palette> Palettes
        {
            get
            {
                if (_palettes == null)
                {
                    _palettes = Enum.GetValues(typeof(Palette)).OfType<Palette>().ToList();
                    _palettes.Remove(Palette.Custom);
                }

                return _palettes;
            }
        }

        public List<Stacking> Stackings
        {
            get
            {
                if (_stackings == null)
                {
                    _stackings = Enum.GetValues(typeof(Stacking)).OfType<Stacking>().ToList();
                }

                return _stackings;
            }
        }

        public class FruitDataItem
        {
            public string Fruit { get; set; }
            public double March { get; set; }
            public double April { get; set; }
            public double May { get; set; }
        }

        public static List<FruitDataItem> CreateData()
        {
            var fruits = new string[] { "Oranges", "Apples", "Pears", "Bananas" };
            var count = fruits.Length;
            var result = new List<FruitDataItem>();
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                result.Add(new FruitDataItem()
                {
                    Fruit = fruits[i],
                    March = rnd.Next(20),
                    April = rnd.Next(20),
                    May = rnd.Next(20),
                });
            return result;
        }


        public List<FruitDataItem> Data
        {
            get
            {
                if (_fruits == null)
                {
                    _fruits = CreateData();
                }

                return _fruits;
            }
        }
    }
}
