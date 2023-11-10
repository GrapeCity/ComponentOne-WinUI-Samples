using MenuExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    public partial class CustomMenuAppearance : UserControl
    {
        public CustomMenuAppearance()
        {
            InitializeComponent();
            Tag = AppResources.CustomMenuAppearanceDesc;
        }
    }
}
