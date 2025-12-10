using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
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
