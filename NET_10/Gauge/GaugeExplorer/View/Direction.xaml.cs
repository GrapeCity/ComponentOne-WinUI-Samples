using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class Direction : UserControl
    {
        public Direction()
        {
            InitializeComponent();
        
            Tag = AppResources.DirectionDescription;
            lblDir.Text = AppResources.Direction;
            DataContext = new SampleViewModel() { Value = 80 };
        }
    }
}
