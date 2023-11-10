using BarCodeExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BarCodeExplorer
{
    /// <summary>
    /// Interaction logic for AccordionSample.xaml
    /// </summary>
    public partial class Demo : UserControl
    {
        public Demo()
        {
            InitializeComponent();
            Tag = AppResources.DemoDesc;
        }
    }
}
