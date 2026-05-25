using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class DisplayingValues : UserControl
    {
        public DisplayingValues()
        {
            InitializeComponent();

            this.lblShowText.Text = AppResources.ShowText;
            this.lblValue.Text = AppResources.Value;
            Tag = AppResources.DisplayingValuesDescription;
            DataContext = new SampleViewModel() { Max = 1, Value = .25, Step = .01, Format = "P0", ShowRanges = false };
        }
    }
}
