using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FlexGridExplorer
{
    public sealed partial class EditingForm : UserControl
    {
        GridCellRange selectedRange;

        public EditingForm()
        {
            this.InitializeComponent();
            Tag = AppResources.EditingDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.HeadersVisibility = GridHeadersVisibility.All;
            grid.IsReadOnly = true;
            grid.MinColumnWidth = 85;
            grid.SelectionMode = GridSelectionMode.Row;
            grid.SelectionChanged += Grid_SelectionChanged;
            grid.CellDoubleTapped += Grid_DoubleClick;
        }

        private void Grid_DoubleClick(object sender, GridInputEventArgs e)
        {
            if (this.selectedRange != null)
            {
                Customer c = grid.Rows[this.selectedRange.Row].DataItem as Customer;
                if (c != null)
                {
                    var form = new EditCustomerForm();
                    form.Customer = c;
                    form.Activate();
                }
            }
        }

        private void Grid_SelectionChanged(object sender, GridCellRangeEventArgs e)
        {
            this.selectedRange = e.CellRange;
        }
    }
}
