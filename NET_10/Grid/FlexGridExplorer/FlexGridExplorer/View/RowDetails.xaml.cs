using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class RowDetails : UserControl
    {
        public RowDetails()
        {
            this.InitializeComponent();
            Tag = AppResources.RowDetailsDescription;
            lblMode.Text = AppResources.DetailsVisibiltyMode;

            showItemsPicker.Items.Add(GridDetailVisibilityMode.ExpandMultiple.ToString());
            showItemsPicker.Items.Add(GridDetailVisibilityMode.ExpandSingle.ToString());
            showItemsPicker.Items.Add(GridDetailVisibilityMode.Selection.ToString());
            showItemsPicker.SelectionChanged += (s, e) =>
            {
                switch (showItemsPicker.SelectedIndex)
                {
                    case 0:
                        details.DetailVisibilityMode = GridDetailVisibilityMode.ExpandMultiple;
                        break;
                    case 1:
                        details.DetailVisibilityMode = GridDetailVisibilityMode.ExpandSingle;
                        break;
                    case 2:
                        details.DetailVisibilityMode = GridDetailVisibilityMode.Selection;
                        break;
                }
            };
            showItemsPicker.SelectedIndex = 1;

            grid.ItemsSource = Customer.GetCustomerList(1000);
            grid.MinColumnWidth = 85;
        }

        private async void OnDetailLoading(object sender, GridDetailLoadingEventArgs<object> e)
        {
            var deferral = e.GetDeferral();
            try
            {
                //Simulates an operation that bring the details data.
                await System.Threading.Tasks.Task.Delay(1000);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
