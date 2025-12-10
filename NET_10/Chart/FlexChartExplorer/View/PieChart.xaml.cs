
using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class PieChart : UserControl
    {
        List<FruitDataItem> _fruits;
        List<Palette> _palettes;
        List<PieLabelPosition> _labelPositions;

        public PieChart()
        {
            this.InitializeComponent();
        }

        public List<double> Radii => new () { 0, 0.25, 0.50, 0.75 };

        public List<double> Offsets => new () { 0, 0.1, 0.2, 0.3, 0.4, 0.5 };

        public List<double> StartAngles => new () { 0, 90, 180, -90 };

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

        public List<PieLabelPosition> LabelPositions
        {
            get
            {
                if (_labelPositions == null)
                {
                    _labelPositions = Enum.GetValues(typeof(PieLabelPosition)).OfType<PieLabelPosition>().ToList();
                    _labelPositions.Remove(PieLabelPosition.Auto);
                }

                return _labelPositions;
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
