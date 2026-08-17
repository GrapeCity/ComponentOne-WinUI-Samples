using C1.WinUI.Core;
using ListViewExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ListViewExplorer
{
    public partial class Unbound : UserControl
    {
        public Unbound()
        {
            InitializeComponent();

            Tag = AppResources.UnbounDesc;

            for (int i = 1; i < 100; i++)
            {
                listView.Items.Add(new C1.WinUI.ListView.ListViewItem { Content = "Item " + i });
            }

            listView.ShowCheckBoxes = true;

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
                listView.SelectionMode = Enum.Parse<C1SelectionMode>((e.AddedItems[0] as ComboBoxItem).Content.ToString()!);
        }

        private void cbOrientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView != null)
                listView.Orientation = Enum.Parse<Orientation>((e.AddedItems[0] as ComboBoxItem).Content.ToString()!);
        }
    }
}
