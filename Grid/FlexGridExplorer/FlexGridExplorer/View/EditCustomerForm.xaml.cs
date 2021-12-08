using C1.WinUI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace FlexGridExplorer
{
    public sealed partial class EditCustomerForm : Window
    {
        public EditCustomerForm()
        {
            this.InitializeComponent();

            Activated += OnActivated;
        }

        public Customer Customer { get; internal set; }

        private void OnActivated(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState != WindowActivationState.Deactivated)
            {
                (Content as FrameworkElement).DataContext = Customer;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            entryFirstName.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            entryLastName.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            datePickerLastOrder.GetBindingExpression(DatePicker.DateProperty).UpdateSource();
            entryOrderTotal.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            Close();
        }
    }

    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            DateTime date = (DateTime)value;
            return new DateTimeOffset(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            DateTimeOffset dto = (DateTimeOffset)value;
            return dto.DateTime;
        }
    }
}
