using C1.Chart;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using Windows.UI;
using System.Linq;
using FlexChartExplorer.Data;
using C1.WinUI.Chart.Palettes;
using Microsoft.UI;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class ExtendedPalettes : UserControl
    {
        DataService dataService = DataService.GetService();
        List<PaletteItem> _palettes;

        public ExtendedPalettes()
        {
            this.InitializeComponent();
        }

        private void ItemInvoked(object sender, TreeViewItemInvokedEventArgs e)
        {
            var item = e.InvokedItem as PaletteItem;
            var node = treeViewPalettes.ContainerFromItem(e.InvokedItem) as TreeViewItem;
            if (item.Children.Count > 0)
            {
                node.IsExpanded = true;
                item = item.Children[0];
                treeViewPalettes.SelectedItem = item;
            }

            UpdateSelection(item);
        }

        private void treeViewPalettes_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (treeViewPalettes.SelectedItem == null)
            {
                treeViewPalettes.RootNodes[0].IsExpanded = true;
                UpdateSelection(Palettes[0].Children[0]);
                treeViewPalettes.SelectedItem = Palettes[0].Children[0];
            }
        }

        private void UpdateSelection(PaletteItem item)
        {
            if (item?.Brushes != null)
            {
                flexPie.Palette = flexChart.Palette = Palette.Custom;
                flexPie.CustomPalette = flexChart.CustomPalette = item?.Brushes;
            }
        }

        private void SymbolRendering(object sender, C1.WinUI.Chart.RenderSymbolEventArgs args)
        {
            if (flexChart.CustomPalette != null)
            {
                args.Engine.SetFill(flexChart.CustomPalette[args.Index]);
                args.Engine.SetStroke(null);
            }
        }

        #region Data

        List<Data.GdpData> data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData().Take(8).ToList();

        void AddItems(IList<PaletteItem> list, string name, IDictionary<string, IList<Brush>> brushes)
        {
            var parent = new  PaletteItem() { Name = name };
            foreach (var key in brushes.Keys)
            { 
               var item = new PaletteItem() { Name = key, Brushes = brushes[key] };
               parent.Children.Add(item);
            }
            list.Add(parent);
        }

        public List<PaletteItem> Palettes
        {
            get
            {
                if (_palettes == null)
                {
                    _palettes = new List<PaletteItem>();
                    AddItems(_palettes, "Diverging", Diverging.Brushes);
                    AddItems(_palettes, "Qualitative", Qualitative.Brushes);
                    AddItems(_palettes, "Sequential Single", SequentialSingle.Brushes);
                    AddItems(_palettes, "Sequential Multi", SequentialMulti.Brushes);
                }

                return _palettes;
            }
        }

        #endregion
    }

    public class PaletteItem
    {
        List<PaletteItem> children = new List<PaletteItem>();
        Canvas canvas;

        public string Name { get; set; }

        public IList<Brush> Brushes { get; set; }

        public IList<PaletteItem> Children => children;

        public Canvas Canvas => Children.Count > 0 ? null : CreateSwatch();

        Canvas CreateSwatch()
        {
            if (Brushes == null)
                return null;

            if (canvas == null)
                canvas = new Canvas() { Width = 100, Height = 20 };

            var cnt = Brushes.Count;

            if (cnt > 0)
            {
                var w = 100.0;
                var dw = w / cnt;

                for (var i = 0; i < cnt; i++)
                {
                    var rect = new Rectangle() { Width = dw, Height = 20, Fill = Brushes[i] };

                    Canvas.SetTop(rect, 0);
                    Canvas.SetLeft(rect, i * dw);
                    canvas.Children.Add(rect);
                }
            }

            return canvas;
        }
    }

}