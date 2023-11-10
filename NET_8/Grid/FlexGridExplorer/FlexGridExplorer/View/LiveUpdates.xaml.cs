using C1.WinUI.Grid;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public sealed partial class LiveUpdates : UserControl
    {
        private Random _rand = new Random();
        private ObservableCollection<Customer> _customers;

        public LiveUpdates()
        {
            this.InitializeComponent();
            Tag = AppResources.LiveUpdatesDescription;

            grid.AutoGeneratingColumn += OnAutoGeneratingColumn;
            _customers = Customer.GetCustomerList(10);
            grid.ItemsSource = _customers;

            SimulateChanges();
        }

        private void OnAutoGeneratingColumn(object sender, GridAutoGeneratingColumnEventArgs e)
        {
            var columns = new string[] { "FirstName", "LastName", "Country", "City" };
            if (!columns.Contains(e.Property.Name))
                e.Cancel = true;
            e.Column.Width = new GridLength(1, GridUnitType.Star);
            e.Column.MinWidth = double.NaN;
        }

        private async void SimulateChanges()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(800));
            GenerateRandomChange();
            SimulateChanges();
        }

        private void GenerateRandomChange()
        {
            switch (_rand.Next(4))
            {
                case 0:
                    _customers.Insert(_rand.Next(_customers.Count + 1), new Customer(_rand.Next()));
                    break;
                case 1:
                    if (_customers.Count > 0)
                        _customers.RemoveAt(_rand.Next(_customers.Count));
                    break;
                case 2:
                    if (_customers.Count > 0)
                        _customers.Move(_rand.Next(_customers.Count), _rand.Next(_customers.Count));
                    break;
                case 3:
                    if (_customers.Count > 0)
                        _customers[_rand.Next(_customers.Count)] = new Customer(_rand.Next());
                    break;
            }
        }
    }
}
