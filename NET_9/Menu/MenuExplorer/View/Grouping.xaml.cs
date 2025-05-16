using C1.WinUI.Menu;
using MenuExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    public partial class Grouping : UserControl
    {
        public Grouping()
        {
            InitializeComponent();
            Tag = AppResources.GroupingDesc;
        }
    }
}
