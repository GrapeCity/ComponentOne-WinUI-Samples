using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace DataFilterExplorer
{
    public class CountInStore
    {
        public Car Car { get; set; }
        public int Count { get; set; }
        public Store Store { get; set; }
        public Color Color { get; set; }

        public SolidColorBrush BrushColor
        {
            get
            {
                if (Color == null)
                {
                    return new SolidColorBrush();
                }

                return new SolidColorBrush(Color);
            }
        }
    }
}
