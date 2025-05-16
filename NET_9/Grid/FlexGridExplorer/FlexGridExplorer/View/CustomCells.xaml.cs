using C1.DataCollection;
using FlexGridExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;

namespace FlexGridExplorer
{
    public sealed partial class CustomCells : UserControl
    {
        public CustomCells()
        {
            this.InitializeComponent();
            Tag = AppResources.CustomCellsDescription;
            BindITunesGrid();
        }

        void BindITunesGrid()
        {
            var songs = MediaLibrary.Load();
            var view = new C1DataCollection<Song>(songs);
            _ = view.GroupAsync("Artist", "Album");
            var fg = _flexiTunes;
            fg.Columns["Duration"].ValueConverter = new SongDurationConverter();
            fg.Columns["Size"].ValueConverter = new SongSizeConverter();
            fg.ItemsSource = view;

        }

        // converter for artist/album groups
        public class GroupHeaderConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string culture)
            {
                // return group name only (no counts)
                var group = value as IDataCollectionGroup<object, object>;
                if (group != null && targetType == typeof(string))
                {
                    return group.Group;
                }

                // default
                return value;
            }
            public object ConvertBack(object value, Type targetType, object parameter, string culture)
            {
                throw new NotImplementedException();
            }
        }

        // converter for song durations (stored in milliseconds)
        class SongDurationConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string culture)
            {
                var ts = TimeSpan.FromMilliseconds((long)value);
                return ts.Hours == 0
                    ? string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds)
                    : string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            }
            public object ConvertBack(object value, Type targetType, object parameter, string culture)
            {
                throw new NotImplementedException();
            }
        }

        // converter for song sizes (return x.xx MB)
        class SongSizeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string culture)
            {
                return string.Format("{0:n2} MB", (long)value / 1024.0 / 1024.0);
            }
            public object ConvertBack(object value, Type targetType, object parameter, string culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
