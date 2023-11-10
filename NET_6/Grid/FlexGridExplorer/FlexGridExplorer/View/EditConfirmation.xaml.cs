using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;

namespace FlexGridExplorer
{
    public sealed partial class EditConfirmation : UserControl
    {
        public EditConfirmation()
        {
            this.InitializeComponent();
            Tag = AppResources.EditConfirmationDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }

        private object _originalValue;

        private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
        {
            _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
        }

        private async void OnCellEditEnded(object sender, GridCellEditEventArgs e)
        {
            var originalValue = _originalValue;
            var currentValue = grid[e.CellRange.Row, e.CellRange.Column];
            if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
            {
                var dialog = new ContentDialog()
                {
                    Content = AppResources.EditConfirmationQuestion,
                    Title = AppResources.EditConfirmationQuestionTitle,
                    PrimaryButtonText = "OK",
                    SecondaryButtonText = "Cancel",
                };
                // Use this code to associate the dialog to the appropriate AppWindow by setting
                // the dialog's XamlRoot to the same XamlRoot as an element that is already present in the AppWindow.
                if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
                {
                    dialog.XamlRoot = grid.XamlRoot;
                }

                var result = await dialog.ShowAsync();
                if (result != ContentDialogResult.Primary)
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }
            }
        }
    }
}
