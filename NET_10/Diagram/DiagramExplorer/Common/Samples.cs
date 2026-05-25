using C1.Chart.Drawing;
using C1.Chart.Standard;
using C1.Diagram.Parser;
using DiagramExplorer.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace DiagramExplorer.Common
{
    /// <summary>
    /// Common stuff shared across samples.
    /// </summary>
    class Samples
    {
        static Stream? GetResourceStream(string name)
        {
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream($"{asm.GetName().Name}.Resources.{name}");
        }

        public static void LoadDiagramFromResource(C1.WinUI.Diagram.FlexDiagram diagram, string name)
        {
            var stream = GetResourceStream(name);

            var text = "";
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                    text = sr.ReadToEnd();
            }

            
            diagram.LoadMermaidGraph(text);
        }

        public static Color Interpolate(Color clr1, Color clr2, double pos)
        {
            var a = clr1.A + (clr2.A - clr1.A) * pos;
            var r = clr1.R + (clr2.R - clr1.R) * pos;
            var g = clr1.G + (clr2.G - clr1.G) * pos;
            var b = clr1.B + (clr2.B - clr1.B) * pos;

            return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
        }

        static Random rnd = new Random();

        static async Task<BitmapImage> ImageFromStream(Stream stream)
        {
            var bitmap = new BitmapImage();
            stream.Seek(0, SeekOrigin.Begin);
            await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            return bitmap;
        }

        public static async Task<ImageSource> CreateFlexChart(int w, int h)
        {
            var pts = new List<Point>();
            for (var i = 0; i < 6; i++)
                pts.Add(new Point(i, rnd.Next(100)));

            var chart = new FlexChart()
            {
                Binding = "Y",
                BindingX = "X",
                DataSource = pts,
                //ForeColor = foreColor, 
                //BackColor = backColor 
            };
            chart.Series.Add(new Series());
            //chart.Margin = new Padding(0);
            var ms = new MemoryStream();
            chart.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }

        public static async Task<ImageSource> CreateFlexPie(int w, int h)
        {
            var pts = new List<Point>();
            for (var i = 0; i < 4; i++)
                pts.Add(new Point(i, rnd.Next(100)));

            var chart = new FlexPie()
            {
                Binding = "Y",
                BindingName = "X",
                DataSource = pts,
                //ForeColor = foreColor,
                //BackColor = backColor
            };
            chart.Legend.Position = C1.Chart.Position.None;
            //chart.Margin = new Padding(0);
            var ms = new MemoryStream();
            chart.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }

        public static async Task<ImageSource> CreateTreeMap(int w, int h)
        {
            var treeMap = new TreeMap()
            {
                Binding = "Value",
                BindingName = "Name",
                //ForeColor = foreColor, 
                //BackColor = backColor 
            };

            treeMap.DataSource = new object[] {
                            new { Name = "Group1", Value = 15 },
                            new { Name = "Group2", Value = 12},
                            new { Name = "Group3", Value = 8},
                        };
            //treeMap.Margin = new Padding(0);
            var ms = new MemoryStream();
            treeMap.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }

        public static async Task<ImageSource> CreateRadarChart(int w, int h)
        {
            var chart = new FlexRadar()
            {
                //ForeColor = foreColor, 
                //BackColor = backColor 
            };
            chart.Binding = "Value";
            chart.BindingX = "Name";

            for (int iser = 0; iser < 3; iser++)
            {
                var data = new List<object>();
                for (var i = 0; i < 6; i++)
                    data.Add(new { Name = $"S{i}", Value = rnd.NextDouble() });
                var ser = new RadarSeries() { Name = $"ser {iser}", DataSource = data };

                chart.Series.Add(ser);
            }
            //chart.Margin = new Padding(0);
            chart.Legend.Position = C1.Chart.Position.None;
            var ms = new MemoryStream();
            chart.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }

        public static async Task<ImageSource> CreateSunburstChart(int w, int h)
        {
            var sunburst = new Sunburst()
            {
                Binding = "sales",
                BindingName = "type",
                ChildItemsPath = "items",
                //ForeColor = foreColor,
                //BackColor = backColor
            };
            sunburst.Offset = 0.2;
            sunburst.DataLabel.Position = C1.Chart.PieLabelPosition.None;
            sunburst.Legend.Position = C1.Chart.Position.None;
            sunburst.DataSource = DataService.CreateHierarchicalData();
            //sunburst.Margin = new Padding(0);
            var ms = new MemoryStream();
            sunburst.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }

        public static async Task<ImageSource> CreateDiagram(int w, int h)
        {
            var diagram = new C1.Diagram.Standard.FlexDiagram()
            {
                //ForeColor = foreColor, 
                //BackColor = backColor 
            };
            diagram.Legend.Position = C1.Chart.Position.None;
            var nodes = diagram.Nodes;
            var edges = diagram.Edges;

            for (var i = 0; i < 3; i++)
                nodes.Add(new C1.Diagram.Standard.Node() { Shape = C1.Diagram.Shape.Circle, LegendItem = $"{i + 1}" });
            edges.Add(new C1.Diagram.Standard.Edge() { Source = nodes[0], Target = nodes[1] });
            edges.Add(new C1.Diagram.Standard.Edge() { Source = nodes[0], Target = nodes[2] });
            var ms = new MemoryStream();
            diagram.SavePng(ms, w, h);
            return await ImageFromStream(ms);
        }
    }
}
