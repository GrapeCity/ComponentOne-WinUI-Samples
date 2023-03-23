using CalendarExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CalendarExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            this.Tag = AppResources.GettingStartedDescription;
            calendar.BoldedDates = new DateTime[] { DateTime.Today.AddDays(3) };
        }
    }
}
