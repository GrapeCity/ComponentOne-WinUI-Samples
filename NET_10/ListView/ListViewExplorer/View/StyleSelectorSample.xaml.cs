using C1.WinUI.Core;
using ListViewExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
namespace ListViewExplorer
{
    public partial class StyleSelectorSample : UserControl
    {
        public StyleSelectorSample()
        {
            InitializeComponent();

            Tag = AppResources.StyleSelectorDesc;
            var items = new List<ItemBase>();
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 0)
                    items.Add(new TeamA());
                else
                    items.Add(new TeamB());
            }
            listView.ItemsSource = items;

            cbSelectionMode.SelectedIndex = 1;
            cbShowCheckBox.IsChecked = true;

            listView.Orientation = Orientation.Vertical;
            cbOrientation.SelectedIndex = 1;

        }

        private void cbShowCheckBox_Click(object sender, RoutedEventArgs e)
        {
            listView.ShowCheckBoxes = cbShowCheckBox.IsChecked == true ? true : false;
        }

        private void cbShowSelectAll_Click(object sender, RoutedEventArgs e)
        {
            listView.ShowSelectAll = cbShowSelectAll.IsChecked == true ? true : false;
        }

        private void cbSelectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView != null)
                listView.SelectionMode = Enum.Parse<C1SelectionMode>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

        private void cbOrientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView != null)
                listView.Orientation = Enum.Parse<Orientation>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }
    }

    public class MyStyleSelector : StyleSelector
    {
        Style _teamA;
        Style _teamB;
        private Style TeamA
        {
            get
            {
                return _teamA ?? (_teamA = (Style)Resources["TeamA"]);
            }
        }
        private Style TeamB
        {
            get
            {
                return _teamB ?? (_teamB = (Style)Resources["TeamB"]);
            }
        }

        public ResourceDictionary Resources { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var lvItem = item as C1.WinUI.ListView.ListViewItem;
            if (lvItem.Content is TeamA)
                return TeamA;
            return TeamB;
        }
    }
}
