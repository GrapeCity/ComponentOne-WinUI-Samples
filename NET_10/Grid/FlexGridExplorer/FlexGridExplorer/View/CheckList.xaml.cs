using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class CheckList : UserControl
    {
        public CheckList()
        {
            this.InitializeComponent();
            Tag = AppResources.CheckListDescription;
            grid.ItemsSource = Customer.GetCities();
        }

        private void OnAutoGeneratingColumn(object sender, C1.WinUI.Grid.GridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = new GridLength(1, GridUnitType.Star);
        }
    }
}
