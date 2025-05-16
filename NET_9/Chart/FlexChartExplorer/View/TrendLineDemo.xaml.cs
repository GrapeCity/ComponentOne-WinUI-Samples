using C1.Chart;
using C1.WinUI.Core;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public sealed partial class TrendLineDemo : UserControl
    {

        public TrendLineDemo()
        {
            this.InitializeComponent();
        }

        void cbFitType_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var fitType = (FitType)cbFitType.SelectedValue;
            cbOrder.IsEnabled = fitType == FitType.Polynom || fitType == FitType.Fourier; 
        }

        #region Data

        DataItem clickedItem;
        List<DataItem> data;
        Random rnd = new();

        public FitType[] FitTypes => new FitType[] { 
            FitType.Linear, FitType.Polynom, FitType.Logarithmic,
            FitType.Exponent, FitType.Power, FitType.Fourier 
        };

        public int[] Orders => new int[] { 1, 2, 3, 4, 5, 6, 7 };

        public class DataItem : INotifyPropertyChanged
        {
            double x;
            double y;

            public double X
            {
                get => x;
                set
                {
                    if (x != value)
                    {
                        x = value;
                        OnPropertyChanged("X");
                    }
                }
            }

            public double Y
            {
                get => y;
                set
                {
                    if (y != value)
                    {
                        y = value;
                        OnPropertyChanged("Y");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<DataItem> Data
        {
            get
            {
                if (data == null)
                {
                    data = new List<DataItem>();
                    for (var i = 1; i <= 8; i++)
                        data.Add(new DataItem() { X = i, Y = rnd.Next(100) });
                }

                return data;
            }
        }

        #endregion

        private void flexChart_Rendered(object sender, C1.WinUI.Chart.RenderEventArgs e)
        {
            UpdateRtb();
        }

        void UpdateRtb()
        {
            rtb.Blocks.Clear();

            if (cbShowEquation.IsChecked == true)
                rtb.Blocks.Add(GetEquationString(trendLine));

            if (cbShowR2.IsChecked == true)
            {
                var paragraph = new Paragraph();
                Run run = new Run() { Text = $"R2={trendLine.GetRegressionStatistics().Rsq:n4}" };
                paragraph.Inlines.Add(run);
                rtb.Blocks.Add(paragraph);
            }
        }

        void UpdateRtbVisiblity(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            rtb.Visibility = cbShowR2.IsChecked == true || cbShowEquation.IsChecked == true? 
                Microsoft.UI.Xaml.Visibility.Visible : Microsoft.UI.Xaml.Visibility.Collapsed;
            UpdateRtb();
        }

        Paragraph GetEquationString(C1.WinUI.Chart.TrendLine trendLine)
        {
            var paragraph = new Paragraph();

            string result = String.Empty;
            var order = trendLine.Order;

            switch (trendLine.FitType)
            {
                case FitType.Linear:
                    result = String.Format("y={1:0.0000}x{0:+0.0000;-0.0000;+0}", trendLine.Coefficients[0], trendLine.Coefficients[1]);
                    break;
                case FitType.Exponent:
                    result = String.Format("y={0:0.0000}e<sup>{1:0.0000}x</sup>", trendLine.Coefficients[0], trendLine.Coefficients[1]);
                    break;
                case FitType.Logarithmic:
                    result = String.Format("y={1:0.0000}ln(x){0:+0.0000;-0.0000;+0}", trendLine.Coefficients[0], trendLine.Coefficients[1]);
                    break;
                case FitType.Power:
                    result = String.Format("y={0:0.0000}x**{1:0.0000}", trendLine.Coefficients[0], trendLine.Coefficients[1]);
                    break;
                case FitType.Polynom:
                    var r = new Run() { Text = "y=" };
                    paragraph.Inlines.Add(r);
                    for (int i = (int)order; i >=2; i--)
                    {
                        r = new Run();
                        r.Text = String.Format("{0:+0.000;-0.0000;+0}x", trendLine.Coefficients[i]);
                        paragraph.Inlines.Add(r);

                        r = new Run();
                        Typography.SetVariants(r, Microsoft.UI.Xaml.FontVariants.Superscript);
                        r.Text = $"{i}";
                        paragraph.Inlines.Add(r);
                    }
                    r = new Run();
                    r.Text = String.Format("{1:+0.0000;-0.0000;+0}x{0:+0.0000;-0.0000;+0}", trendLine.Coefficients[0], trendLine.Coefficients[1]);
                    paragraph.Inlines.Add(r);
                    break;
                case FitType.Fourier:
                    result = String.Format("{0:+0.0000;-0.0000;+0}", trendLine.Coefficients[0]);
                    for (int i = 2, a = 1; i <= (int)order; i++, a = i % 2 == 0 ? a + 1 : a)
                        result += String.Format("{0:+0.000;-0.0000;+0}{2}({1}x)", trendLine.Coefficients[i - 1], a == 1 ? "" : a.ToString(), (i) % 2 == 0 ? "cos" : "sin");
                    result = result.Remove(0, 1).Insert(0, "y=");
                    break;
            }

            if (!string.IsNullOrEmpty(result))
                paragraph.Inlines.Add(new Run() { Text = result });

            return paragraph;
        }

        #region Drag
        C1DragHelper dragHelper;


        private void flexChart_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (dragHelper == null)
            {
                dragHelper = new C1DragHelper(flexChart, C1DragHelperMode.TranslateY, captureElementOnPointerPressed:false);
                dragHelper.DragStarted += HandleDragStarted;
                dragHelper.DragDelta += HandleDragDelta;
                dragHelper.DragCompleted += HandleDragCompleted;
            }
        }

        void HandleDragStarted(object sender, C1DragStartedEventArgs e)
        {
            var ht = flexChart.Series[0].HitTest(e.GetPosition(flexChart));
            if (ht.Distance <=5 && ht.X != null && ht.Y != null)
            {
                clickedItem = ht.Item as DataItem;
            }
            else
                clickedItem = null;
        }

        void HandleDragDelta(object sender, C1DragDeltaEventArgs e)
        {
            if (clickedItem != null)
            {
                var newY = flexChart.PointToData(e.GetPosition(flexChart)).Y;
                if(double.IsFinite(newY))
                    clickedItem.Y = newY;
            }
        }

        void HandleDragCompleted(object sender, C1DragCompletedEventArgs e)
        {
            clickedItem = null;
        }

        #endregion
    }
}
