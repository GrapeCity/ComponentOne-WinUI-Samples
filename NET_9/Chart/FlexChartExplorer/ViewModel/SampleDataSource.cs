using FlexChartExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexChartExplorer
{
    /// <summary>
    /// Sample store.
    /// </summary>
    public class SampleDataSource
    {
        private ObservableCollection<ISampleItem> _allItems = new ObservableCollection<ISampleItem>();

        /// <summary>
        /// Creates an instance of sample data source.
        /// </summary>
        public SampleDataSource()
        {
            _allItems.Add(new SampleItem<Intro>());

            _allItems.Add(new SampleGroup()
            {
                Name = "Basics",
                Children = new List<ISampleItem>()
                {
                    new SampleItem<Binding>(),
                    new SampleItem<SeriesBinding>(),
                    new SampleItem<PieChart>(),
                    new SampleItem<TreeMap>(),
                    new SampleItem<Bubble>(),
                    new SampleItem<FinancialChart>(),
                }
            });

            _allItems.Add(new SampleGroup()
            {
                Name = "Special Charts",
                Children = new List<ISampleItem>()
                {
                    new SampleItem<BoxWhiskerChart>(),
                    new SampleItem<BreakEven>(),
                    new SampleItem<ErrorBars>(),
                    new SampleItem<Funnel>(),
                    new SampleItem<Heatmap>(),
                    new SampleItem<ColumnHeatmap>(),
                    new SampleItem<HistogramChart>(),
                    new SampleItem<Radar>(),    
                    new SampleItem<SunburstChart>(),
                    new SampleItem<WaterfallChart>()
                }
            });

            _allItems.Add(new SampleGroup()
                { 
                    Name = "Axes",
                    Children = new List<ISampleItem>()
                    {
                        new SampleItem<AxisLabels>(),
                        new SampleItem<AxisGroups>(),
                        new SampleItem<LogAxes>(),
                        new SampleItem<TwoYAxes>(),
                        new SampleItem<AxisScrollbar>(),
                        new SampleItem<AxisBreak>()
                    }
                }
            );

            _allItems.Add(new SampleGroup()
            {
                Name = "Interaction",
                Children = new List<ISampleItem>()
                    {
                        new SampleItem<Selection>(),
                        new SampleItem<PieSelection>(),
                        new SampleItem<HitTest>(),
                        new SampleItem<Zoom>(),
                        new SampleItem<LineMarker>(),
                        new SampleItem<RangeSelector>(),
                        new SampleItem<AxisMarkers>(),
                    }
            });

            _allItems.Add(new SampleGroup()
            {
                Name = "Features",
                Children = new List<ISampleItem>()
                    {
                        new SampleItem<AlarmZones>(),
                        new SampleItem<Animation>(),
                        new SampleItem<ImageExport>(),
                        new SampleItem<MultiPie>(),
                        new SampleItem<ExtendedPalettes>(),
                        new SampleItem<DataLabels>(),
                        new SampleItem<CustomDataLabels>(),
                        new SampleItem<Legend>(),
                        new SampleItem<Pareto>(),
                        new SampleItem<TrendLineDemo>()
                    }
            });
        }

        /// <summary>
        /// Gets the all samples.
        /// </summary>
        public ObservableCollection<ISampleItem> AllItems
        {
            get { return _allItems; }
        }
    }
}
