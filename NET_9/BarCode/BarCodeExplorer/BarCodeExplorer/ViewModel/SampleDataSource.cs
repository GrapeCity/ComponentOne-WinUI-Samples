using BarCodeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace BarCodeExplorer
{
    public class SampleDataSource
    {
        private ObservableCollection<SampleItem> _allItems = new ObservableCollection<SampleItem>();

        public SampleDataSource()
        {
            _allItems.Add(new SampleItem(AppResources.DemoTitle,
                AppResources.DemoTitle,
                new System.Lazy<UserControl>(() => new Demo())));
            _ = _allItems[0].Sample.Value; //Force first page is loaded immediately

            _allItems.Add(new SampleItem(AppResources.NewBarcodesTitle,
                AppResources.NewBarcodesTitle,
                new System.Lazy<UserControl>(() => new NewBarCodes())));
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get { return _allItems; }
        }
    }
}
