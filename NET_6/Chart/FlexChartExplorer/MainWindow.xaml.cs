// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FlexChartExplorer.Data;
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

namespace FlexChartExplorer
{
#pragma warning disable 1591
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            Title = "WinUI FlexChart Explorer";
        }

        private void samples_Loaded(object sender, RoutedEventArgs e)
        {
            var list = SamplesList.GetSamples();
            samples.ItemsSource = list;
            samples.ItemInvoked += Samples_ItemInvoked;

            sampleGrid.DataContext = samples.SelectedItem = list[0];
        }

        private void Samples_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            sampleGrid.DataContext = args.InvokedItem;
        }

        private void sampleGrid_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {

        }
    }
}
