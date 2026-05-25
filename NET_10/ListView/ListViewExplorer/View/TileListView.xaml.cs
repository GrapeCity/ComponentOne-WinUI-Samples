using C1.WinUI.Core;
using C1.WinUI.ListView;
using ListViewExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ListViewExplorer
{
    public partial class TileListView : UserControl
    {
        public TileListView()
        {
            InitializeComponent();

            Tag = AppResources.TileListViewTag;

            _list1.ItemsSource = Person.Generate(1000000);


            cbShowCheckBox.IsChecked = true;
            cbShowSelectAll.IsChecked = false;
            cbSelectionMode.SelectedIndex = 1;
            cb.SelectedIndex = 0;
            _list1.ShowCheckBoxes = true;
            _list1.ShowSelectAll = false;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_list1 != null)
                _list1.Orientation = Enum.Parse<Orientation>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

        private void cbShowCheckBox_Click(object sender, RoutedEventArgs e)
        {
            _list1.ShowCheckBoxes = cbShowCheckBox.IsChecked == true ? true : false;
        }

        private void cbShowSelectAll_Click(object sender, RoutedEventArgs e)
        {
            _list1.ShowSelectAll = cbShowSelectAll.IsChecked == true ? true : false;
        }

        private void cbSelectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_list1 != null)
                _list1.SelectionMode = Enum.Parse<C1SelectionMode>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }
    }
}
