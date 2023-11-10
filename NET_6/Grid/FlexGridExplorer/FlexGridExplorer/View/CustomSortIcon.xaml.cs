using C1.DataCollection;
using C1.WinUI.Core;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;
using Microsoft.UI.Xaml;

namespace FlexGridExplorer
{
    public sealed partial class CustomSortIcon : UserControl
    {
        public CustomSortIcon()
        {
            this.InitializeComponent();
            Tag = AppResources.CustomSortIconDescription;

            foreach (var value in Enum.GetValues(typeof(GridSortIconPosition)))
            {
                sortIconPosition.Items.Add(value.ToString());
            }
            sortIconPosition.SelectedIndex = 1;
            foreach (var value in new string[] { "Custom 1", "Custom 2", nameof(C1IconTemplate.TriangleUp), nameof(C1IconTemplate.TriangleNorth), nameof(C1IconTemplate.ChevronUp), nameof(C1IconTemplate.ArrowUp) })
            {
                sortIconTemplate.Items.Add(value);
            }
            sortIconTemplate.SelectedIndex = 1;
            lblIconPos.Text = AppResources.SortIconPosition;
            lblIconTemplate.Text = AppResources.SortIconTemplate;

            Load();
        }

        private async void Load()
        {
            var data = new C1DataCollection<Customer>(Customer.GetCustomerList(100));
            await data.SortAsync(new SortDescription("FirstName"), new SortDescription("LastName", SortDirection.Descending));
            grid.ItemsSource = data;
        }

        private void SortIconPositionChanged(object sender, SelectionChangedEventArgs e)
        {
            grid.SortIconPosition = (GridSortIconPosition)Enum.Parse(typeof(GridSortIconPosition), (string)sortIconPosition.Items[sortIconPosition.SelectedIndex]);
        }

        private void SortIconTemplateChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (sortIconTemplate.SelectedIndex)
            {
                case 0:
                    grid.SortAscendingIconTemplate = Resources["SortAscendingIcon"] as DataTemplate;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 1:
                    grid.SortAscendingIconTemplate = Resources["Sort2AscendingIcon"] as DataTemplate;
                    grid.SortDescendingIconTemplate = Resources["Sort2DescendingIcon"] as DataTemplate;
                    grid.SortIndeterminateIconTemplate = Resources["Sort2Icon"] as DataTemplate;
                    break;
                case 2:
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 3:
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleNorth;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 4:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ChevronUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 5:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ArrowUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
            }
        }
    }
}
