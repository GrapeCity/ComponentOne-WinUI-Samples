using CalendarExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace CalendarExplorer
{
    public sealed partial class VerticalOrientation : UserControl
    {
        public VerticalOrientation()
        {
            this.InitializeComponent();
            this.Tag = AppResources.VerticalOrientationDescription;
        }
    }
}
