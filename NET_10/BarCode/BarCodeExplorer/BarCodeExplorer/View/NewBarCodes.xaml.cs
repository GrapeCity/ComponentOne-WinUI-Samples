using C1.BarCode;
using System;
using System.Collections.Generic;
using BarCodeExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace BarCodeExplorer
{
    /// <summary>
    /// Interaction logic for NewBarCodes.xaml
    /// </summary>
    public partial class NewBarCodes : UserControl
    {
        List<CodeType> _barcodes = new List<CodeType>
        {
            CodeType.Bc412, CodeType.Code11, CodeType.HIBCCode128, CodeType.HIBCCode39, CodeType.Iata25, CodeType.IntelligentMailPackage, CodeType.ISBN, CodeType.ISMN, CodeType.ISSN, CodeType.ITF14, CodeType.MicroQRCode, CodeType.Pharmacode, CodeType.Plessey, CodeType.PZN, CodeType.SSCC18, CodeType.Telepen
        };

        public NewBarCodes()
        {
            InitializeComponent();
            Tag = AppResources.NewBarcodesDesc;
            Loaded += NewBarcode_Loaded;
        }

        private void NewBarcode_Loaded(object sender, RoutedEventArgs e)
        {
            BarcodeType.ItemsSource = _barcodes;
            BarcodeType.SelectedIndex = 0;
        }

        private void BarcodeType_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BarcodeType.SelectedItem == null)
                return;
            barCode.Text = "";
            CodeType codetype = (CodeType)BarcodeType.SelectedItem;
            barCode.CodeType = codetype;
            // Change the available text for selected barcode type
            switch (codetype)
            {
                case CodeType.HIBCCode128:
                case CodeType.HIBCCode39:
                    BarcodeText.Text = @"A123PROD78905/0123456789DATA";
                    break;
                case CodeType.IntelligentMailPackage:
                    BarcodeText.Text = "9212391234567812345671";
                    break;
                case CodeType.PZN:
                    BarcodeText.Text = "01234562";
                    break;
                case CodeType.Pharmacode:
                    BarcodeText.Text = "131070";
                    break;
                case CodeType.SSCC18:
                    BarcodeText.Text = "1234t5+678912345678";
                    break;
                case CodeType.Bc412:
                    BarcodeText.Text = "AQ1557";
                    break;
                case CodeType.MicroQRCode:
                    BarcodeText.Text = "12345";
                    break;
                case CodeType.Iata25:
                    BarcodeText.Text = "0123456789";
                    break;
                case CodeType.ITF14:
                    BarcodeText.Text = "1234567891011";
                    break;
                default:
                    BarcodeText.Text = "9790123456785";
                    break;
            }
            barCode.Text = BarcodeText.Text;
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(BarcodeText.Text))
            {
                var messageDialog = new MessageDialog("Please type something in Data part!");
                await messageDialog.ShowAsync();
                return;
            }
            barCode.Text = BarcodeText.Text;
        }
    }
}
