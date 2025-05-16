using FlexChartExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public interface ISampleItem
    {
        string Name { get; }
        string Title { get; }
        string Description { get; }
        UserControl Sample { get; }
    }

    public class SampleGroup : ISampleItem
    {
        public string Name { get; set; }
        public string Title => Children?[0].Title;
        public string Description => Children?[0].Description;
        public UserControl Sample => Children?[0].Sample;

        public List<ISampleItem> Children { get; set;}

        public string Glyph => "\uF5ED";
    }

    public class SampleItem<T> : ISampleItem where T : UserControl, new()
    {
        private UserControl sample;

        string TypeName => typeof(T).Name;

        public string Name
        { 
            get 
            {
                var name = AppResources.ResourceManager.GetString(TypeName + "Title");
                return string.IsNullOrEmpty(name) ? TypeName : name;
            }
        }
        public string Title
        {
            get
            {
                var name = AppResources.ResourceManager.GetString(TypeName + "Title");
                return string.IsNullOrEmpty(name) ? TypeName : name;
            }
        }
        public string Header
        {
            get
            {
                var header = AppResources.ResourceManager.GetString(TypeName + "Header");
                return string.IsNullOrEmpty(header) ? Title : header;
            }
        }

        public string Description => AppResources.ResourceManager.GetString(typeof(T).Name + "Description");
        public UserControl Sample 
        {
            get
            {
                if(sample == null)
                    sample = new T();
                return sample;
            }
        }

        public List<ISampleItem> Children => null;
        public string Glyph => "\uE9F9";
    }
}
