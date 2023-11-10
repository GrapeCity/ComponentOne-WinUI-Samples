using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InputExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            DataContext = new SampleDataSource();
            Loaded += MenuExplorerView_Loaded;
        }

        private void MenuExplorerView_Loaded(object sender, RoutedEventArgs e)
        {
            lbSamples.SelectedItem = lbSamples.Items[0];
        }
    }
}
