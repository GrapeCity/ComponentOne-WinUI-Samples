using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace FlexGridExplorer
{
    /// <summary>
    /// Represents a grid cell that has an image and some text.
    /// </summary>
    public abstract class ImageCell : StackPanel
    {
        ImageSource _imgSrc;

        public ImageCell()
        {
            if (_imgSrc == null)
            {
                _imgSrc = GetImageSource(GetImageResourceName());
            }

            Orientation = Orientation.Horizontal;

            var img = new Image();
            img.Source = _imgSrc;
            img.Width = 25;
            img.Height = 15;
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Stretch = Stretch.None;
            Children.Add(img);

            TextBlock = new TextBlock();
            TextBlock.VerticalAlignment = VerticalAlignment.Center;
            Children.Add(TextBlock);
        }

        public TextBlock TextBlock { get; private set; }

        //public virtual GridRow Row
        //{
        //    set { BindCell(value.DataItem); }
        //}
        //void BindCell(object dataItem)
        //{
        //    var binding = new Binding("Name");
        //    binding.Source = dataItem;
        //    TextBlock.SetBinding(TextBlock.TextProperty, binding);
        //}
        public abstract string GetImageResourceName();

        public static ImageSource GetImageSource(string imageName)
        {
            var bmp = new BitmapImage();
            bmp.CreateOptions = BitmapCreateOptions.None;
            bmp.UriSource = new Uri($"ms-appx:///Images/{imageName}");
            return bmp;
        }
    }

    /// <summary>
    /// Represents a grid cell that is bound to a song name.
    /// </summary>
    public class SongCell : ImageCell
    {
        public override string GetImageResourceName()
        {
            return "Song.png";
        }
    }

    /// <summary>
    /// Represents a grid cell that is bound to an artist.
    /// </summary>
    public class ArtistCell : ImageCell
    {
        public override string GetImageResourceName()
        {
            return "Artist.png";
        }
    }

    /// <summary>
    /// Represents a grid cell that is bound to an album.
    /// </summary>
    public class AlbumCell : ImageCell
    {
        public override string GetImageResourceName()
        {
            return "Album.png";
        }
    }
}
