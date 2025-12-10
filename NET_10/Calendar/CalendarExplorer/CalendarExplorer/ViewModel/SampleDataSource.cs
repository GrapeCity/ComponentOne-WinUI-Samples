using CalendarExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace CalendarExplorer
{
    public class SampleDataSource
    {
        private ObservableCollection<SampleItem> _allItems = new ObservableCollection<SampleItem>();

        public SampleDataSource()
        {
            _allItems.Add(new SampleItem(AppResources.GettingStartedTitle,
                AppResources.GettingStartedTitle,
                new System.Lazy<UserControl>(() => new GettingStarted())));
            _ = _allItems[0].Sample.Value; //Force first page is loaded immediately

            _allItems.Add(new SampleItem(AppResources.VerticalOrientationTitle,
                AppResources.VerticalOrientationTitle,
                new System.Lazy<UserControl>(() => new VerticalOrientation())));
            _allItems.Add(new SampleItem(AppResources.CustomDayTitle,
                AppResources.CustomDayTitle,
                new System.Lazy<UserControl>(() => new CustomDay())));

            _allItems.Add(new SampleItem(AppResources.CustomAppearanceTitle,
                AppResources.CustomAppearanceTitle,
                new System.Lazy<UserControl>(() => new CustomAppearance())));
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get { return _allItems; }
        }
    }
}
