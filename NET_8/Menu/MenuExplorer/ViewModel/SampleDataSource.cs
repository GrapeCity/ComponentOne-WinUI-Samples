using MenuExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace MenuExplorer
{
    public class SampleDataSource
    {
        public SampleDataSource()
        {
            AllItems = new ObservableCollection<SampleItem>
            {
                new SampleItem(AppResources.DemoMenuTitle,
                                AppResources.DemoMenuTitle,
                                new System.Lazy<UserControl>(() => new DemoMenu())),
                new SampleItem(AppResources.CustomMenuAppearanceTitle,
                                AppResources.CustomMenuAppearanceTitle,
                                new System.Lazy<UserControl>(() => new CustomMenuAppearance())),
                //new SampleItem(Properties.Resources.DropDownMenuTitle,
                //                Properties.Resources.DropDownMenuTitle,
                //                new DropDownMenu()),
                //new SampleItem(Properties.Resources.DemoRadialMenuTitle,
                //                Properties.Resources.DemoRadialMenuTitle,
                //                new DemoRadialMenu()),
                //new SampleItem(Properties.Resources.CustomRadialContextMenuTitle,
                //                Properties.Resources.CustomRadialContextMenuTitle,
                //                new CustomRadialContextMenu())
            };
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get;
        }
    }
}
