using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Radar : UserControl
    {
        List<CountryData> _data;
        List<Palette> _palettes;

        public Radar()
        {
            this.InitializeComponent();
        }

        public RadarChartType[] ChartTypes =>  new RadarChartType[] { RadarChartType.Line,
            RadarChartType.LineSymbols, RadarChartType.Area, RadarChartType.Scatter}; 

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

        public Stacking[] Stackings => 
            new Stacking[] { Stacking.None, Stacking.Stacked, Stacking.Stacked100pc };

        #region Data

        public class CountryData
        {
            public string Name { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
        }

        public static List<CountryData> GetCountrySales(int rangeMin = 100, int rangeMax = 1000)
        {
            var rnd = new Random();
            var data = new List<CountryData>();
            var countries = "US,China,Japan,Germany,UK,France".Split(',');
            for (int i = 0; i < countries.Length; i++)
            {
                var country = new CountryData
                {
                    Name = countries[i],
                    Sales = rnd.Next(rangeMin, rangeMax),
                    Expenses = rnd.Next(rangeMin, rangeMax)
                };
                data.Add(country);
            }
            return data;
        }

        public List<CountryData> Data => _data == null ? _data = GetCountrySales() : _data;

        #endregion
    }
}
