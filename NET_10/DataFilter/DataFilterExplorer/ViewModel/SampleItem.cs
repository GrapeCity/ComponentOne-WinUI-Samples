using Microsoft.UI.Xaml.Controls;
using System;

namespace DataFilterExplorer
{
    public class SampleItem
    {
        public SampleItem(string name, string title, Lazy<Control> sample)
        {
            Name = name;
            Title = title;
            Sample = sample;
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description => Sample.Value.Tag?.ToString();
        public Lazy<Control> Sample { get; set; }
    }
}
