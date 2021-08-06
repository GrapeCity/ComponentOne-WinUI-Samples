using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;
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
                var dialog = new MessageDialog(AppResources.EditConfirmationQuestion, AppResources.EditConfirmationQuestionTitle);
                dialog.Commands.Add(new UICommand(AppResources.OK));
                dialog.Commands.Add(new UICommand(AppResources.Cancel, cmd=>
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }));
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;
                await dialog.ShowAsync();
            }
        }
    }
}
