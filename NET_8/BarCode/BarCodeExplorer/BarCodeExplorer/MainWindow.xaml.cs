using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarCodeExplorer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            root.DataContext = new SampleDataSource();
            themes.Items.Add("Default");
            themes.Items.Add("Light");
            themes.Items.Add("Dark");
        }

        private void OnRootLoaded(object sender, RoutedEventArgs e)
        {
            themes.SelectedIndex = (int)(root.XamlRoot.Content as FrameworkElement).RequestedTheme;
        }

        private void lbSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1) return;
            SampleItem selectedItem = e.AddedItems[0] as SampleItem;
            grid.DataContext = selectedItem;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (root.XamlRoot.Content as FrameworkElement).RequestedTheme = (ElementTheme)themes.SelectedIndex;
            //App.Current.RequestedTheme = themes.SelectedIndex == 0 ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }
    }
}
