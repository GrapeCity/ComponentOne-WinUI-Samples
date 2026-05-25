using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class UsingRanges : UserControl
    {
        public UsingRanges()
        {
            InitializeComponent();

            lblShowRanges.Text = AppResources.ShowRanges;
            lblValue.Text = AppResources.Value;
            Tag = AppResources.UsingRangesDescription;

            DataContext = new SampleViewModel() { Value = 25, ShowRanges = true };

            linearGauge.Pointer.Thickness = 0.5;
            radialGauge.Pointer.Thickness = 0.5;
        }
    }
}
