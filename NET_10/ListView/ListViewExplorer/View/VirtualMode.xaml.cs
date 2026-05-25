using ListViewExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace ListViewExplorer
{
    public partial class VirtualMode : UserControl
    {
        public VirtualMode()
        {
            InitializeComponent();
            Tag = AppResources.VirtualModeDescription;

            var persons = new VirtualModeDataCollection();
            listView.ItemsSource = persons;
        }
    }
}
