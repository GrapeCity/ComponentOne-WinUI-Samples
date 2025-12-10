using C1.Chart;
using FlexChartExplorer.Data;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Bubble : UserControl
    {
        DataService dataService = DataService.GetService();

        public Bubble()
        {
            this.InitializeComponent();
            flexChart.Palette = Palette.Custom;
            flexChart.CustomPalette = C1.WinUI.Chart.Palettes.Qualitative.Set1;
        }

        void BindingChanged(object sender, SelectionChangedEventArgs args)
        {
            flexChart.BeginUpdate();

            var x = cbX.SelectedItem as string;
            var y = cbY.SelectedItem as string;
            var size = cbSize.SelectedItem as string;

            flexChart.BindingX = x;
            flexChart.Binding = $"{y},{size}";

            flexChart.AxisX.Title = x;
            flexChart.AxisY.Title = y;

            flexChart.EndUpdate();
        }

        #region Data

        public string[] Fields => new[] { "MilesPerGallon", "HorsePower", "Cylinders", "Displacement" };

        List<Data.CarData> dataUSA = null;
        List<Data.CarData> dataJapan = null;
        List<Data.CarData> dataEurope = null;

        public List<Data.CarData> DataUSA => dataUSA != null ? dataUSA : dataUSA = 
            dataService.GetCarData().Where( item=>item.Origin == "USA").ToList();

        public List<Data.CarData> DataJapan => dataJapan != null ? dataJapan : dataJapan =
            dataService.GetCarData().Where(item => item.Origin == "Japan").ToList();

        public List<Data.CarData> DataEurope => dataEurope != null ? dataEurope : dataEurope =
            dataService.GetCarData().Where(item => item.Origin == "Europe").ToList();

        #endregion
    }
}
