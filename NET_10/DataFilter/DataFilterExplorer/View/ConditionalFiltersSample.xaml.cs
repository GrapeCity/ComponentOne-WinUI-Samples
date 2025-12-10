using C1.DataCollection;
using C1.WinUI.Core;
using DataFilterExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Data;
using System.IO;
using System.Linq;

namespace DataFilterExplorer
{
    /// <summary>
    /// Interaction logic for ConditionalFiltersSample.xaml
    /// </summary>
    public partial class ConditionalFiltersSample : UserControl
    {
        private readonly string _fileName = "ConditionalFilterExpressions.xml";

        private DataTable _carsTable;
        public ConditionalFiltersSample()
        {
            InitializeComponent();
            Tag = AppResources.ConditionalFiltersDescription;
            //Get Cars list
            _carsTable = DataProvider.GetCarTable();
            var data = new C1DataCollection<Car>(DataProvider.GetCarDataCollection(_carsTable).ToList());
            c1DataFilter.ItemsSource = data;
            flexGrid.ItemsSource = data;
            _fileName = Windows.Storage.UserDataPaths.GetDefault().LocalAppData + "/Temp/ConditionalFilterExpressions.xml";
            if (File.Exists(_fileName))
            {
                c1DataFilter.LoadFilterExpression(_fileName);
            }
        }

        private async void BtnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            await c1DataFilter.ApplyFilterAsync();
        }

        private void BtnSaveFilter_Click(object sender, RoutedEventArgs e)
        {
            c1DataFilter.SaveFilterExpression(_fileName);
        }

        private void CbAutoApply_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (c1DataFilter != null)
            {
                c1DataFilter.AutoApply = cbAutoApply.IsChecked == true;
            }
        }

        private void flexGrid_Loaded(object sender, RoutedEventArgs e)
        {
            flexGrid.ColumnOptionsIconTemplate = C1IconTemplate.ThreeDotsVertical;
        }
    }
}
