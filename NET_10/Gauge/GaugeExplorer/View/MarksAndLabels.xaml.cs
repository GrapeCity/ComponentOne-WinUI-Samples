using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace GaugeExplorer
{
    public sealed partial class MarksAndLabels : UserControl
    {
        public MarksAndLabels()
        {
            InitializeComponent();

            Tag = AppResources.MarksAndLabelsDescription;
        }
    }
}
