using InputExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace InputExplorer
{
    /// <summary>
    /// Interaction logic for InputView.xaml
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        public RangeSlider()
        {
            InitializeComponent();
            Tag = AppResources.RangeSlider;
        }
    }
}
