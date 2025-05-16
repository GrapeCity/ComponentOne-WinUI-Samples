using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class VirtualMode : UserControl
    {
        public VirtualMode()
        {
            this.InitializeComponent();
            Tag = AppResources.VirtualModeDescription;

            grid.ItemsSource = new VirtualModeDataCollection();
        }
    }
}
