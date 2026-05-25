using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class Pointer : UserControl
    {
        public Pointer()
        {
            InitializeComponent();
            Tag = AppResources.PointerDescription;
        }
    }
}
