using Microsoft.UI.Xaml.Controls;

namespace MenuExplorer
{
    /// <summary>
    /// Interaction logic for CustomMenuAppearance.xaml
    /// </summary>
    public partial class CustomMenuAppearance : UserControl
    {
        public CustomMenuAppearance()
        {
            InitializeComponent();
            Tag = Properties.Resources.CustomMenuAppearanceDesc;
        }
    }
}
