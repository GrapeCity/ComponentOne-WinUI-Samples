using InputExplorer.Resources;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace InputExplorer
{
    public class SampleDataSource
    {
        public SampleDataSource()
        {
            AllItems = new ObservableCollection<SampleItem>
            {
                new SampleItem(AppResources.InputTitle,
                                AppResources.InputTitle,
                                new System.Lazy<UserControl>(() => new InputView())),
                new SampleItem(AppResources.RangeSliderTitle,
                                AppResources.RangeSliderTitle,
                                new System.Lazy<UserControl>(() => new RangeSlider())),
                //new SampleItem(AppResources.MaskedDemoTitle,
                //                AppResources.MaskedDemoTitle,
                //                new DemoMaskedTextBox()),
                //new SampleItem(AppResources.TagEditorDemoTitle,
                //                AppResources.TagEditorDemoTitle,
                //                new DemoTagEditor()),

                //new SampleItem(AppResources.CheckListTitle,
                //                AppResources.CheckListTitle,
                //                new C1CheckList()),
                
                //new SampleItem(AppResources.MultiSelect,
                //                AppResources.MultiSelect,
                //                new C1MultiSelect()),

                //new SampleItem(AppResources.ValidationFormTitle,
                //                AppResources.ValidationFormTitle,
                //                new ValidationForm()),
            };
        }

        public ObservableCollection<SampleItem> AllItems
        {
            get;
        }
    }
}
