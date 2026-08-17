using C1.WinUI.Gauge;
using GaugeExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace GaugeExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        private Storyboard? _storyboard;

        public GettingStarted()
        {
            InitializeComponent();

            Tag = AppResources.GettingStartedDescription;
            DataContext = new SampleViewModel() { Value = 25, ShowText = GaugeTextVisibility.None, IsReadOnly = false };
            _storyboard = Resources["AnimateGauges"] as Storyboard;
            _storyboard?.Begin();
            IsAnimating = true;
        }

        private bool IsAnimating { get; set; }

        private void StartAnimation()
        {
            AnimationButton.Content = AppResources.PauseAnimationLabel;
            _storyboard?.Resume();
            IsAnimating = true;
        }
        private void StopAnimation()
        {
            AnimationButton.Content = AppResources.ResumeAnimationLabel;
            _storyboard?.Pause();
            IsAnimating = false;
        }

        private void OnAnimationButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsAnimating)
                StopAnimation();
            else
                StartAnimation();
        }
    }
}
