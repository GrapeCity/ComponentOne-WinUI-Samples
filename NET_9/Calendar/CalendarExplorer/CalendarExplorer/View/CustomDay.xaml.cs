using CalendarExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace CalendarExplorer
{
    public sealed partial class CustomDay : UserControl
    {
        public CustomDay()
        {
            InitializeComponent();
            this.Tag = AppResources.CustomDayDescription;
        }
    }
}
