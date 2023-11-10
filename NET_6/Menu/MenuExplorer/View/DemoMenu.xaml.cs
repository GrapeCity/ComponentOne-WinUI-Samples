using C1.WinUI.Menu;
using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    /// <summary>
    /// Interaction logic for DemoMenu.xaml
    /// </summary>
    public partial class DemoMenu : UserControl
    {
        public DemoMenu()
        {
            InitializeComponent();
            Tag = Properties.Resources.DemoMenuDesc;
        }
    }
}
