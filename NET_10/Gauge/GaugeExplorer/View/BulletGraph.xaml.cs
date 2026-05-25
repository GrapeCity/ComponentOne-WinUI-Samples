using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class BulletGraph : UserControl
    {
        public BulletGraph()
        {
            InitializeComponent();

            Tag = AppResources.BulletGraphDescription;
            lblBad.Text = AppResources.Bad;
            lblGood.Text = AppResources.Good;
            lblTarget.Text = AppResources.Target;
        }
    }
}
