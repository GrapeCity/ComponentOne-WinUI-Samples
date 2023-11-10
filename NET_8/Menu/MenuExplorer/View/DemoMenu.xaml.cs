using C1.WinUI.Menu;
using MenuExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    public partial class DemoMenu : UserControl
    {
        public DemoMenu()
        {
            InitializeComponent();
            Tag = AppResources.DemoMenuDesc;
        }
    }
}
