using C1.DataCollection;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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

            var styles = Resources.Keys.Cast<string>().OrderBy(s => s).ToList();
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
