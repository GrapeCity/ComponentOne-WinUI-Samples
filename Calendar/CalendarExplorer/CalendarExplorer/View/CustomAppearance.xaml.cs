using Microsoft.UI.Xaml.Controls;

namespace CalendarExplorer
{
    public sealed partial class CustomAppearance : UserControl
    {
        public CustomAppearance()
        {
            InitializeComponent();

            calendar.ViewModeAnimation.ScaleFactor = 4;
            calendar.ViewModeAnimation.AnimationMode = C1.WinUI.Calendar.CalendarViewModeAnimationMode.ZoomOutIn;
        }
    }
}
