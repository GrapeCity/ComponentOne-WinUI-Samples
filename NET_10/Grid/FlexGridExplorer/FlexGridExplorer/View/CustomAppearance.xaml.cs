using C1.DataCollection;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FlexGridExplorer
{
    public sealed partial class CustomAppearance : UserControl
    {
        public CustomAppearance()
        {
            this.InitializeComponent();
            Tag = AppResources.CustomAppearanceDescription;

            var styles = Resources.MergedDictionaries[0].Keys.Cast<string>().OrderBy(s => s).ToList();
            Themes.ItemsSource = styles;
            Themes.SelectedIndex = styles.IndexOf("Material");

            PopulateEditGrid();
        }

        private async void PopulateEditGrid()
        {
            // create the data
            var data = new C1DataCollection<Customer>(Customer.GetCustomerList(100));
            await data.SortAsync("Name");
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
            grid.Columns["FirstName"].DataMap = new GridDataMap { ItemsSource = Customer.GetFirstNames() };
            grid.Columns["LastName"].DataMap = new GridDataMap { ItemsSource = Customer.GetLastNames() };

            //grid.Columns.Move(grid.Columns["Name"].Index, 1);
        }

        private void OnThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            grid.Style = Resources[Themes.SelectedValue as string] as Style;
        }
    }
}
