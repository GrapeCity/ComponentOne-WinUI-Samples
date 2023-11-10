using C1.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Input;
using Windows.Foundation;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class Zoom : UserControl
    {
        bool zooming = false;
        Point pointStart = new Point();

        public Zoom()
        {
            this.InitializeComponent();
        }

        private void PerformZoom(Point p1, Point p2)
        {
            p1 = flexChart.PointToData(p1);
            p2 = flexChart.PointToData(p2);
            flexChart.BeginUpdate();
            // Update axes with new limits
            flexChart.AxisX.Min = Math.Min(p1.X, p2.X);
            flexChart.AxisY.Min = Math.Min(p1.Y, p2.Y);
            flexChart.AxisX.Max = Math.Max(p1.X, p2.X);
            flexChart.AxisY.Max = Math.Max(p1.Y, p2.Y);
            flexChart.EndUpdate();
        }

        private void DrawReversibleRectangle(Point p1, Point p2)
        {
            // Normalize the rectangle
            var rc = new Rect(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y),
                Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
            var offset = reversibleFrameContainer.TransformToVisual(flexChart).TransformPoint(new Point(0, 0));
            Canvas.SetLeft(rect, rc.X - offset.X);
            Canvas.SetTop(rect, rc.Y - offset.Y);
            rect.Width = rc.Width; rect.Height = rc.Height;
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            flexChart.BeginUpdate();
            // Restore default values for axis limits
            flexChart.AxisX.Min = flexChart.AxisX.Max = double.NaN;
            flexChart.AxisY.Min = flexChart.AxisY.Max = double.NaN;
            flexChart.EndUpdate();
        }

        private void Chart_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            reversibleFrameContainer.Visibility = Visibility.Visible;
            // Start zooming
            zooming = true;
            // Save starting point of selection rectangle
            pointStart = e.Position;
        }

        private void Chart_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Draw new frame
            if (zooming)
            {
                var pos = e.Position;
                DrawReversibleRectangle(pointStart, pos);
            }
        }

        private void Chart_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            PerformZoom(pointStart, e.Position);

            // End zooming
            zooming = false;
            pointStart = new Point();
            rect.Width = 0; rect.Height = 0;
        }

        #region Data

        List<Point> data1 = null;
        List<Point> data2 = null;

        public List<Point> Data1 => data1 == null ? data1 = CreateData(2, 0.16, 0.12, 160) : data1;
        public List<Point> Data2 => data2 == null ? data2 = CreateData(1, 0.1, 0.15, 160) : data2;

        List<Point> CreateData(double a, double xph, double yph, int n)
        {
            var data = new List<Point>(n);

            for (int i = 0; i < n; i++)
                data.Add(new Point(a * Math.Sin(xph * i), a * Math.Cos(yph * i)));

            return data;
        }

        #endregion
    }
}
