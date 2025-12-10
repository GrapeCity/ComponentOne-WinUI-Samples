using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using System.Linq;

namespace DataFilterExplorer
{
    public class CarStoreGroup
    {
        public Car Car { get; set; }
        public List<CountInStore> CountInStores { get; set; }

        public int TotalInStores
        {
            get
            {
                return CountInStores == null ? 0 : CountInStores.Sum(x => x.Count);
            }
        }

        public IEnumerable<SolidColorBrush> Colors
        {
            get
            {
                if(CountInStores == null)
                {
                    return new List<SolidColorBrush>();
                }

                var colors = CountInStores.Select(x => x.Color).Distinct().Select(x => new SolidColorBrush(x));
                return colors;
            }
        }
    }
}
