using C1.WinUI.Core;
using CommunityToolkit.WinUI;
using ListViewExplorer.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ListViewExplorer
{
    public partial class TemplateSelector : UserControl
    {
        public TemplateSelector()
        {
            InitializeComponent();

            Tag = AppResources.TemplateSelectorDesc;
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

    public class ItemTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container)
        {
            var current = container as FrameworkElement;
            var lvItem = item as C1.WinUI.ListView.ListViewItem;

            DataTemplate? template = null;

            try
            {
                while (current != null)
                {
                    if (lvItem?.Content is TeamA)
                    {
                        template = current.TryFindResource("Template1") as DataTemplate;
                    }
                    else if (lvItem?.Content is TeamB)
                    {
                        template = current.TryFindResource("Template2") as DataTemplate;
                    }

                    if (template != null)
                        return template;

                    current = VisualTreeHelper.GetParent(current) as FrameworkElement;
                }
            }
            catch
            {
            }

            return null;
        }
    }

    public class TeamA : ItemBase
    {
        public TeamA()
        {
            Name = Person.GetCustomName("[Team A] ");
        }
    }

    public class TeamB : ItemBase
    {
        public TeamB()
        {
            Name = Person.GetCustomName("[Team B] ");
        }
    }

    public abstract class ItemBase
    {
        public string Name { get; set; }
    }
}
