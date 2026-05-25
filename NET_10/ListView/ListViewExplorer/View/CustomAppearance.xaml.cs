using ListViewExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace ListViewExplorer
{
    public partial class CustomAppearance : UserControl
    {
        public CustomAppearance()
        {
            InitializeComponent();
            Tag = AppResources.CustomAppearanceDescription;
            listView.ItemsSource = Person.Generate(100);
        }
    }
}
