using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace FlexGridExplorer
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

            _allItems.Add(new SampleItem(AppResources.ColumnDefinitionTitle,
                AppResources.ColumnDefinitionTitle,
                new System.Lazy<UserControl>(() => new ColumnDefinitions())));
            _allItems.Add(new SampleItem(AppResources.SelectionModesTitle,
                AppResources.SelectionModesTitle,
                new System.Lazy<UserControl>(() => new SelectionModes())));
            _allItems.Add(new SampleItem(AppResources.EditConfirmationTitle,
                AppResources.EditConfirmationTitle,
                new System.Lazy<UserControl>(() => new EditConfirmation())));
            _allItems.Add(new SampleItem(AppResources.EditingTitle,
                AppResources.EditingTitle,
                new System.Lazy<UserControl>(() => new EditingForm())));
            _allItems.Add(new SampleItem(AppResources.ConditionalFormattingTitle,
                AppResources.ConditionalFormattingTitle,
                new System.Lazy<UserControl>(() => new ConditionalFormatting())));
            _allItems.Add(new SampleItem(AppResources.CustomCellsTitle,
                AppResources.CustomCellsTitle,
                new System.Lazy<UserControl>(() => new CustomCells())));
            _allItems.Add(new SampleItem(AppResources.GroupingTitle,
                AppResources.GroupingTitle,
                new System.Lazy<UserControl>(() => new Grouping())));
            _allItems.Add(new SampleItem(AppResources.RowDetailsTitle,
                AppResources.RowDetailsTitle,
                new System.Lazy<UserControl>(() => new RowDetails())));
            //_allItems.Add(new SampleItem(AppResources.FilterTitle,
            //    AppResources.FilterTitle,
            //    AppResources.FilterDescription,
            //    new Filter()));
            _allItems.Add(new SampleItem(AppResources.FullTextFilterTitle,
                AppResources.FullTextFilterTitle,
                new System.Lazy<UserControl>(() => new FullTextFilter())));
            _allItems.Add(new SampleItem(AppResources.FilterRowTitle,
                AppResources.FilterRowTitle,
                new System.Lazy<UserControl>(() => new FilterRow())));
            _allItems.Add(new SampleItem(AppResources.ColumnLayoutTitle,
                AppResources.ColumnLayoutTitle,
                new System.Lazy<UserControl>(() => new ColumnLayout())));
            _allItems.Add(new SampleItem(AppResources.StarSizingTitle,
                AppResources.StarSizingTitle,
                new System.Lazy<UserControl>(() => new StarSizing())));
            _allItems.Add(new SampleItem(AppResources.CellFreezingTitle,
                AppResources.CellFreezingTitle,
                new System.Lazy<UserControl>(() => new CellFreezing())));
            _allItems.Add(new SampleItem(AppResources.CustomMergingTitle,
                AppResources.CustomMergingTitle,
                new System.Lazy<UserControl>(() => new CustomMerging())));
            _allItems.Add(new SampleItem(AppResources.UnboundTitle,
                AppResources.UnboundTitle,
                new System.Lazy<UserControl>(() => new Unbound())));
            _allItems.Add(new SampleItem(AppResources.OnDemandTitle,
                AppResources.OnDemandTitle,
                new System.Lazy<UserControl>(() => new OnDemand())));
            _allItems.Add(new SampleItem(AppResources.CustomAppearanceTitle,
                AppResources.CustomAppearanceTitle,
                new System.Lazy<UserControl>(() => new CustomAppearance())));
            _allItems.Add(new SampleItem(AppResources.NewRowTitle,
                AppResources.NewRowTitle,
                new System.Lazy<UserControl>(() => new NewRow())));
            _allItems.Add(new SampleItem(AppResources.CheckListTitle,
                AppResources.CheckListTitle,
                new System.Lazy<UserControl>(() => new CheckList())));
            _allItems.Add(new SampleItem(AppResources.PagingTitle,
                AppResources.PagingTitle,
                new System.Lazy<UserControl>(() => new Paging())));
            _allItems.Add(new SampleItem(AppResources.FinancialTitle,
                AppResources.FinancialTitle,
                new System.Lazy<UserControl>(() => new Financial())));
            _allItems.Add(new SampleItem(AppResources.LiveUpdatesTitle,
                AppResources.LiveUpdatesTitle,
                new System.Lazy<UserControl>(() => new LiveUpdates())));
            _allItems.Add(new SampleItem(AppResources.CustomSortIconTitle,
                AppResources.CustomSortIconTitle,
                new System.Lazy<UserControl>(() => new CustomSortIcon())));
            _allItems.Add(new SampleItem(AppResources.MouseHoverTitle,
                AppResources.MouseHoverTitle,
                new System.Lazy<UserControl>(() => new MouseHover())));
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get { return _allItems; }
        }
    }
}
