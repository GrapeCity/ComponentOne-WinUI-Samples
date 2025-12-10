using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace FlexGridExplorer
{
    public sealed partial class FullTextFilter : UserControl
    {
        /// <summary>
        /// Specify the fields that should be taken into account for performing filtering
        /// </summary>
        public IEnumerable<string> FilteredFields { get; set; }
        public FullTextFilter()
        {
            this.InitializeComponent();
            Tag = AppResources.FullTextFilterDescription;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
            FilteredFields = null;
        }
    }
}
