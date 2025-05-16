using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Specialized;
using System.Linq;

namespace FlexGridExplorer
{
    public sealed partial class Financial : UserControl
    {
        FinancialDataList _financialData;

        public Financial()
        {
            this.InitializeComponent();
            Tag = AppResources.FinancialDescription;
            PopulateFinancialGrid();
        }

        void PopulateFinancialGrid()
        {
            _financialData = FinancialData.GetFinancialData();
            _flexFinancial.ItemsSource = _financialData;
            _flexFinancial.FrozenColumns = 1;
            _flexFinancial.Columns[0].AllowDragging = false;

            // show company info
            UpdateCompanyStatus();
            _flexFinancial.DataCollection.CollectionChanged += financial_CollectionChanged;

            UpdateCellFactory();
        }

        void financial_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCompanyStatus();
        }

        // update song status
        void UpdateCompanyStatus()
        {
            var companies = _flexFinancial.DataCollection.OfType<FinancialData>();
            _txtCompanies.Text = string.Format("{0:n0} companies.",
                (from c in companies select c.Symbol).Distinct().Count());
        }

        // control update frequency
        void _chkAutoUpdate_Click(object sender, RoutedEventArgs e)
        {
            _financialData.AutoUpdate = ((CheckBox)sender).IsChecked.Value;
        }
        void _cmbUpdateInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_financialData != null)
            {
                var cmb = sender as ComboBox;
                var val = ((ComboBoxItem)cmb.SelectedItem).Content as string;
                val = val.Split(' ')[0].Trim();
                _financialData.UpdateInterval = int.Parse(val);
            }
        }
        void _cmbBatchSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_financialData != null)
            {
                var cmb = sender as ComboBox;
                var val = ((ComboBoxItem)cmb.SelectedItem).Content as string;
                val = val.Split(' ')[0].Trim();
                _financialData.BatchSize = int.Parse(val);
            }
        }
        void _chkOwnerDrawFinancial_Click(object sender, RoutedEventArgs e)
        {
            UpdateCellFactory();
        }

        private void UpdateCellFactory()
        {
            _flexFinancial.CellFactory = _chkOwnerDrawFinancial.IsChecked.Value
                ? new FinancialCellFactory()
                : null;
        }
    }
}
