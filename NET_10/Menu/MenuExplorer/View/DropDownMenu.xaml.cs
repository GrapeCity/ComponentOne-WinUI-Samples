using MenuExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    public partial class DropDownMenu : UserControl
    {
        public DropDownMenu()
        {
            InitializeComponent();
            Tag = AppResources.DropDownMenuDesc;
        }
    }
}
