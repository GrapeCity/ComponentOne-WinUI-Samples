using C1.WinUI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace FlexGridExplorer
{
    public sealed partial class EditCustomerForm : ContentDialog
    {
        public EditCustomerForm()
        {
            this.InitializeComponent();
            DataContext = Customer;
            Loaded += EditCustomerForm_Loaded;
        }

        private void EditCustomerForm_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Customer;
        }

        public Customer Customer { get; internal set; }

 
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            entryFirstName.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            entryLastName.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            datePickerLastOrder.GetBindingExpression(DatePicker.DateProperty).UpdateSource();
            entryOrderTotal.GetBindingExpression(C1TextBox.TextProperty).UpdateSource();
            Hide();
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
