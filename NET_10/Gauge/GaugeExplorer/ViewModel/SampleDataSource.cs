using GaugeExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace GaugeExplorer
{
    public class SampleDataSource
    {
        private ObservableCollection<SampleItem> _allItems = new ObservableCollection<SampleItem>();

        public SampleDataSource()
        {
            _allItems.Add(new SampleItem(AppResources.GettingStartedTitle, () => new GettingStarted()));
            _allItems.Add(new SampleItem(AppResources.DisplayingValuesTitle, () => new DisplayingValues()));
            _allItems.Add(new SampleItem(AppResources.UsingRangesTitle, () => new UsingRanges()));
            _allItems.Add(new SampleItem(AppResources.AutomaticScalingTitle, () => new AutomaticScaling()));
            _allItems.Add(new SampleItem(AppResources.DirectionTitle, () => new Direction()));
            _allItems.Add(new SampleItem(AppResources.BulletGraphTitle, () => new BulletGraph()));
            _allItems.Add(new SampleItem(AppResources.MarksAndLabelsTitle, () => new MarksAndLabels()));
            _allItems.Add(new SampleItem(AppResources.PointerTitle, () => new Pointer()));
            _ = _allItems[0].Sample; //Force first page is loaded immediately
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get { return _allItems; }
        }
    }
}
