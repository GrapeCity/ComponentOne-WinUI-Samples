using Microsoft.UI.Xaml.Controls;
using System;

namespace CalendarExplorer
{
    public class SampleItem
    {
        public SampleItem(string name, string title, Lazy<UserControl> sample)
        {
            Name = name;
            Title = title;
            Sample = sample;
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description => Sample.Value.Tag?.ToString();
        public Lazy<UserControl> Sample { get; set; }
    }
}
