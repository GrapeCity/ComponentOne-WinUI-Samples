using DiagramExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiagramExplorer
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
            _allItems.Add(new SampleItem<Intro>() { Controls = "Direction,EdgeRouting,Palette" });

            _allItems.Add(new SampleGroup()
            {
                Name = AppResources.Unbound,
                Children = new List<ISampleItem>()
                {
                    new SampleItem<FlexChartFamily>(),
                    new SampleItem<Dynamic>(){ Controls = "Direction,EdgeRouting,Palette" },
                    new SampleItem<Clock>(),
                    new SampleItem<CustomShape>(),
                }
            });

            _allItems.Add(new SampleGroup()
            {
                Name = AppResources.DataBinding,
                Children = new List<ISampleItem>()
                {
                    new SampleItem<OrgChart>() { Controls = "Direction,EdgeRouting,Palette" },
                    new SampleItem<Animals>(),
                    new SampleItem<NodeTemplate>(),
                    new SampleItem<Nested>(),
                }
            });

            _allItems.Add(new SampleGroup()
            {
                Name = AppResources.Interaction,
                Children = new List<ISampleItem>()
                {
                    new SampleItem<HitTesting>() { Controls = "Direction,EdgeRouting,Palette" },
                    new SampleItem<Selection>(),
                    new SampleItem<Tooltips>(),
                    new SampleItem<Collapsible>() { Controls = "Direction,EdgeRouting,Palette" },
                }
            });

            _allItems.Add(new SampleGroup()
            {
                Name = AppResources.UsageScenarios,
                Children = new List<ISampleItem>()
                {
                    new SampleItem<DecisionTree>(),
                    new SampleItem<Literature>(),
                    new SampleItem<FamilyTree>(),
                    new SampleItem<ProgrammingLanguages>()
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
