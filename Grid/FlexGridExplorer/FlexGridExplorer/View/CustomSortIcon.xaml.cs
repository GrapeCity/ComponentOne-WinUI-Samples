using C1.DataCollection;
using C1.WinUI.Core;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;

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
            foreach (var value in new string[] { nameof(C1IconTemplate.TriangleUp), nameof(C1IconTemplate.TriangleNorth), nameof(C1IconTemplate.ChevronUp), nameof(C1IconTemplate.ArrowUp) })
            {
                sortIconTemplate.Items.Add(value);
            }
            sortIconTemplate.SelectedIndex = 2;
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
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleUp;
                    break;
                case 1:
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleNorth;
                    break;
                case 2:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ChevronUp;
                    break;
                case 3:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ArrowUp;
                    break;
            }
        }
    }
}
