using C1.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class PieSelection : UserControl
    {
        List<DataItem> _data;

        public PieSelection()
        {
            this.InitializeComponent();
        }

        void SelectionChanged(object sender, EventArgs args)
        {
            infoBar.Message = flexPie.SelectedIndex >= 0 ?
                $"Selected item = {Data[flexPie.SelectedIndex].Model}"
                : "Click on data to select.";
        }

        #region Data

        public double[] Offsets => new[] { 0, 0.1, 0.2 };
        public Position[] Positions => new[] { Position.None, Position.Left, Position.Top, Position.Right, Position.Bottom };

        public class DataItem
        {
            public string Model { get; set; }
            public int Vehicales  { get; set; }
        }

        public static List<DataItem> CreateData()
        {
            return new List<DataItem>
            { 
                new DataItem() { Model = "Others", Vehicales = 46292 },
                new DataItem() { Model = "Model 3", Vehicales = 26684 },
                new DataItem() { Model = "Model Y", Vehicales = 26148 },
                new DataItem() { Model = "Leaf", Vehicales = 13078 },
                new DataItem() { Model = "Model S", Vehicales = 7523 },
                new DataItem() { Model = "Bolt EV", Vehicales = 5594 },
                new DataItem() { Model = "Model X", Vehicales = 4994 },
                new DataItem() { Model = "Volt", Vehicales = 4869 },
                new DataItem() { Model = "ID.4", Vehicales = 2831 },
                new DataItem() { Model = "Niro", Vehicales = 2757 },
                new DataItem() { Model = "Prius Prime", Vehicales = 2499 },
            };
        }

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                    _data = CreateData();

                return _data;
            }
        }

        #endregion
    }
}
