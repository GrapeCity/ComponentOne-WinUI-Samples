using C1.WinUI.Core;
using C1.WinUI.Gauge;
using C1.WinUI.Input;
using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class AutomaticScaling : UserControl
    {
        public AutomaticScaling()
        {
            InitializeComponent();

            Tag = AppResources.AutomaticScalingDescription;
            lblStartAngle.Text = AppResources.StartAngle;
            lblSweepAngle.Text = AppResources.SweepAngle;
            lblReversed.Text = AppResources.Reversed;
            DataContext = new SampleViewModel() { Max = 200, Value = 60, ShowText = GaugeTextVisibility.All };
        }

        private void NumericBox_ValueChanged(object sender, PropertyChangedEventArgs<double> e)
        {
            if (sender == null) return;

            var nb = (C1NumericBox)sender;

            if (nb?.Value == null)
                return;

            double val = nb.Value;

            if (val < nb.Minimum) nb.Value = nb.Minimum;
            else if (val > nb.Maximum) nb.Value = nb.Maximum;
        }
    }
}
