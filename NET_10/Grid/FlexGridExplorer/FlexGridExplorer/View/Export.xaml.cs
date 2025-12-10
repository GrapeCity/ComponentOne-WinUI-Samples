using C1.DataCollection;
using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Storage.Pickers;

namespace FlexGridExplorer
{
    public partial class Export : UserControl
    {
        public Export()
        {
            InitializeComponent();

            Tag = AppResources.ExportDescription;

            var data = Customer.GetCustomerList(100);
            var actualData = data.Select(data => new ExtendedClass()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                City = data.City,
                CountryId = data.CountryId,
                Email = data.Email,
                PostalCode = data.PostalCode,
                Active = data.Active,
                LastOrderDate = data.LastOrderDate,
                OrderTotal = data.OrderTotal,
                SampleHyperlink = "https://media-exp1.licdn.com/dms/image/C560BAQE2UbhekqtLAg/company-logo_200_200/0/1519856432286?e=2159024400&v=beta&t=HP2cnfQvlv4mYMh2ouJShTcs4bsKyMQErk2u_kLDjtM",
                SampleHyperlinkContent = $"Hyperlink - {data.Id}",
                SampledImage = "https://media-exp1.licdn.com/dms/image/C560BAQE2UbhekqtLAg/company-logo_200_200/0/1519856432286?e=2159024400&v=beta&t=HP2cnfQvlv4mYMh2ouJShTcs4bsKyMQErk2u_kLDjtM"
            }).ToList();
            grid.ItemsSource = actualData;
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = Customer.GetCountries(), SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.Columns["CountryID"].AllowMerging = true;
            grid.MinColumnWidth = 85;
        }


        private async void OnExport(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(((App)Application.Current).m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            savePicker.DefaultFileExtension = ".html";
            savePicker.FileTypeChoices.Add("HTML File (*.htm;*.html)", new List<string>() { ".htm", ".html" });
            savePicker.FileTypeChoices.Add("Comma Separated Values (*.csv)", new List<string>() { ".csv" });
            savePicker.FileTypeChoices.Add("Text File (*.txt)", new List<string>() { ".txt" });
            try
            {
                var file = await savePicker.PickSaveFileAsync();
                if (file == null)
                    return;
                var ext = System.IO.Path.GetExtension(file.Name).ToLower();
                ext = ext == ".htm" ? "ehtm" : ext == ".html" ? "ehtm" : ext;

                var options = GridSaveOptions.None;
                if ((bool)checkFormatted.IsChecked)
                {
                    options |= GridSaveOptions.Formatted;
                }
                if ((bool)checkSaveHeaders.IsChecked)
                {
                    options |= GridSaveOptions.SaveHeaders;
                }
                if ((bool)checkVisibleColumns.IsChecked)
                {
                    options |= GridSaveOptions.VisibleColumns;
                }
                if ((bool)checkVisibleRows.IsChecked)
                {
                    options |= GridSaveOptions.VisibleRows;
                }
                switch (ext)
                {
                    case "ehtm":
                        {
                            grid.Save(file.Path, GridFileFormat.Html, options);
                            break;
                        }
                    case ".csv":
                        {
                            grid.Save(file.Path, GridFileFormat.Csv, options);
                            break;
                        }
                    case ".txt":
                        {
                            grid.Save(file.Path, GridFileFormat.Text, options);
                            break;
                        }
                }
                Process.Start(new ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Content = ex.Message,
                    CloseButtonText = "OK",
                    XamlRoot = XamlRoot
                };
                await dialog.ShowAsync();
            }
        }

    }

    public class ExtendedClass : Customer
    {
        public string SampleHyperlink { get; set; }
        public string SampleHyperlinkContent { get; set; }
        public string SampledImage { get; set; }
    }
}
