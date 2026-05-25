using C1.DataCollection;
using C1.WinUI.Core;
using C1.WinUI.ListView;
using C1.WinUI.Menu;
using ListViewExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ListViewExplorer
{
    public sealed partial class CustomTemplate : UserControl
    {
        public CustomTemplate()
        {
            InitializeComponent();

            Tag = AppResources.CustomTemplateDescription;
            listView.ItemsSource = Person.Generate(1000000);

            //cbSelectionMode.SelectedIndex = 0;
            cbShowCheckBox.IsChecked = true;

            listView.Orientation = Orientation.Vertical;
            listView.ItemHeight = 72;
            //cbOrientation.SelectedIndex = 1;

        }

        private void cbShowCheckBox_Click(object sender, RoutedEventArgs e)
        {
            listView.ShowCheckBoxes = cbShowCheckBox.IsChecked == true ? true : false;
        }

        private void cbShowSelectAll_Click(object sender, RoutedEventArgs e)
        {
            listView.ShowSelectAll = cbShowSelectAll.IsChecked == true ? true : false;
        }

        private void OnItemTapped(object sender, C1TappedEventArgs e)
        {
            if (e.IsRightTapped)
            {
                var menu = new C1ContextMenu();
                var removeMenuItem = new C1MenuItem() { IconTemplate = C1IconTemplate.Delete, Header = "Remove item" };
                removeMenuItem.Click += OnRemoveMenuItem;
                menu.Items.Add(removeMenuItem);
                menu.Show(sender as FrameworkElement);
            }
        }

        private async void OnRemoveMenuItem(object sender, SourcedEventArgs e)
        {
            if (listView.SelectedIndex >= 0)
                await listView.DataCollection.RemoveAsync(listView.SelectedIndex);
        }

        private void OnSelectionModeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView != null)
                listView.SelectionMode = Enum.Parse<C1SelectionMode>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

        private void OnOrientationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView != null)
                listView.Orientation = Enum.Parse<Orientation>((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

    }
}
