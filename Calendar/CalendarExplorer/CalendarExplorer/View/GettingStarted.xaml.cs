using CalendarExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace CalendarExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            this.Tag = AppResources.GettingStartedDescription;
        }
    }
}
