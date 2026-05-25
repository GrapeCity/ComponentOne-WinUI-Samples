using C1.DataCollection;
using C1.WinUI.Core;
using C1.WinUI.Grid;
using C1.WinUI.Input;
using FlexGridExplorer.Resources;
using GrapeCity.Documents.Excel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.Storage.Pickers;

namespace FlexGridExplorer
{
    /// <summary>
    /// Interaction logic for GridExport.xaml
    /// </summary>
    public partial class ExcelExport : UserControl
    {
        private Dictionary<C1CheckBox, GridRowColRangesOptions> RowOptions { get; set; }
        private Dictionary<C1CheckBox, GridRowColRangesOptions> ColumnOptions { get; set; }

        private C1DataCollection<ExtendedClass> DataCollection { get; set; }

        private List<string> ValidColumns { get; set; }

        private List<string> Groupings { get; set; }

        private bool IsGroupingClicked { get; set; }

        public ExcelExport()
        {
            InitializeComponent();
            Tag = AppResources.ExcelExportDescription;

            #region Binding

            var data = Customer.GetCustomerList(100);
            var actualData = data.Select(data => new ExtendedClass()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                City = data.City,
                CountryId = data.CountryId,
                Email = data.Email,
                PostalCode = data.PostalCode,
                Active = data.Active,
                LastOrderDate = data.LastOrderDate,
                OrderTotal = data.OrderTotal,
                SampleHyperlink = "https://media-exp1.licdn.com/dms/image/C560BAQE2UbhekqtLAg/company-logo_200_200/0/1519856432286?e=2159024400&v=beta&t=HP2cnfQvlv4mYMh2ouJShTcs4bsKyMQErk2u_kLDjtM",
                SampleHyperlinkContent = $"Hyperlink - {data.Id}",
                SampledImage = "https://media-exp1.licdn.com/dms/image/C560BAQE2UbhekqtLAg/company-logo_200_200/0/1519856432286?e=2159024400&v=beta&t=HP2cnfQvlv4mYMh2ouJShTcs4bsKyMQErk2u_kLDjtM"
            }).ToList();
            DataCollection = new C1DataCollection<ExtendedClass>(actualData);

            grid.ItemsSource = DataCollection;
            Dictionary<int, string> dct = new Dictionary<int, string>();
            foreach (var country in Customer.GetCountries())
            {
                dct[dct.Count] = country.Value;
            }
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = dct, SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.Columns["CountryID"].AllowMerging = true;
            grid.MinColumnWidth = 85;

            #endregion

            #region Themes

            //var styles = Resources.MergedDictionaries[0].Keys.Cast<string>().OrderBy(s => s).ToList();
            //styles.Insert(0, "Classic");
            //styles = styles.OrderBy(x => x).ToList();
            //Themes.ItemsSource = styles;
            //Themes.SelectedIndex = styles.IndexOf("Classic");
            //Themes.IsEditable = false;

            #endregion

            #region Option(s)

            RowOptions = new Dictionary<C1CheckBox, GridRowColRangesOptions>() {
                { checkRowsVisibleOnly, GridRowColRangesOptions.VisibleOnly },
                { checkRowsRenderFrozen, GridRowColRangesOptions.RenderFrozen },
                { checkRowsSelectedOnly, GridRowColRangesOptions.SelectedOnly },
                { checkRowsExcludeRange, GridRowColRangesOptions.ExludeRange },
                { checkRowsExcludeEmpty, GridRowColRangesOptions.ExcludeEmpty },
                { checkRowsRenderGroups, GridRowColRangesOptions.RenderGroups } };
            ColumnOptions = new Dictionary<C1CheckBox, GridRowColRangesOptions>() {
                { checkColumnsVisibleOnly, GridRowColRangesOptions.VisibleOnly },
                { checkColumnsRenderFrozen, GridRowColRangesOptions.RenderFrozen },
                { checkColumnsSelectedOnly,GridRowColRangesOptions.SelectedOnly },
                { checkColumnsExcludeRange, GridRowColRangesOptions.ExludeRange } };

            #endregion

            #region Grouping

            var columns = grid.Columns.Select(x => x.ColumnName).ToList();
            Grouping_0.ItemsSource = columns;
            Groupings = new List<string>();
            // Last Order Date Field is mapped into 2 different columns with
            // different formats.
            ValidColumns = columns.Distinct().ToList();

            #endregion
        }

        public class ExtendedClass : Customer
        {
            public string SampleHyperlink { get; set; }
            public string SampleHyperlinkContent { get; set; }
            public string SampledImage { get; set; }
        }


        private async void OnExportClick(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(((App)Application.Current).m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            savePicker.DefaultFileExtension = ".xlsx";
            savePicker.FileTypeChoices.Add("Excel Workbook (*.xlsx;*.xlsm)", new List<string>() { ".xlsx", ".xlsm" });
            savePicker.FileTypeChoices.Add("Comma Separated Values (*.csv)", new List<string>() { ".csv" });
            savePicker.FileTypeChoices.Add("HTML File (*.htm;*.html)", new List<string>() { ".htm", ".html" });
            savePicker.FileTypeChoices.Add("PDF Files (*.pdf)", new List<string>() { ".pdf" });
            try
            {
                var file = await savePicker.PickSaveFileAsync();
                if (file == null)
                    return;
                var ext = System.IO.Path.GetExtension(file.Name).ToLower();

                var rowOptions = RowOptions.Keys.ToList().Where(check => check.IsChecked ?? false).Select(check => RowOptions[check]).ToList();
                var rows = GridRowColRangesOptions.None;
                if (rowOptions.Count > 0)
                {
                    rows |= rowOptions.Aggregate((x, y) => x | y);
                }

                var columnOptions = ColumnOptions.Keys.ToList().Where(check => check.IsChecked ?? false).Select(check => ColumnOptions[check]).ToList();
                var columns = GridRowColRangesOptions.None;
                if (columnOptions.Count > 0)
                {
                    columns |= columnOptions.Aggregate((x, y) => x | y);
                }

                var headers = GridHeadersVisibility.None;
                if (checkHeadersAll.IsChecked ?? false)
                {
                    headers = GridHeadersVisibility.All;
                }
                else if (checkHeadersColumn.IsChecked ?? false)
                {
                    headers = GridHeadersVisibility.Column;
                }
                else if (checkHeadersRow.IsChecked ?? false)
                {
                    headers = GridHeadersVisibility.Row;
                }
                try
                {
                    switch (ext)
                    {
                        case ".csv":
                            {
                                await grid.SaveAsync(file.Path,
                                    "FlexGrid Sheet",
                                    SaveFileFormat.Csv,
                                    new GridRowColRanges(txtRowRanges.Text, rows),
                                    new GridRowColRanges(txtColRanges.Text, columns),
                                    headers,
                                    merged: checkMerged.IsChecked ?? false,
                                    formatted: checkFormatted.IsChecked ?? false,
                                    renderImages: checkRenderImages.IsChecked ?? false);
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
                                break;
                            }
                        case ".htm" or ".html":
                            {
                                await grid.SaveAsync(file.Path,
                                    "FlexGrid Sheet",
                                    SaveFileFormat.Html,
                                    new GridRowColRanges(txtRowRanges.Text, rows),
                                    new GridRowColRanges(txtColRanges.Text, columns),
                                    headers,
                                    merged: checkMerged.IsChecked ?? false,
                                    formatted: checkFormatted.IsChecked ?? false,
                                    renderImages: checkRenderImages.IsChecked ?? false);
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
                                break;
                            }
                        case ".pdf":
                            {
                                await grid.SaveAsync(file.Path,
                                    "FlexGrid Sheet",
                                    SaveFileFormat.Pdf,
                                    new GridRowColRanges(txtRowRanges.Text, rows),
                                    new GridRowColRanges(txtColRanges.Text, columns),
                                    headers,
                                    merged: checkMerged.IsChecked ?? false,
                                    formatted: checkFormatted.IsChecked ?? false,
                                    renderImages: checkRenderImages.IsChecked ?? false);
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
                                break;
                            }
                        case ".xlsx":
                            {
                                await grid.SaveAsync(file.Path,
                                    "FlexGrid Sheet",
                                    SaveFileFormat.Xlsx,
                                    new GridRowColRanges(txtRowRanges.Text, rows),
                                    new GridRowColRanges(txtColRanges.Text, columns),
                                    headers,
                                    merged: checkMerged.IsChecked ?? false,
                                    formatted: checkFormatted.IsChecked ?? false,
                                    renderImages: checkRenderImages.IsChecked ?? false);
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
                                break;
                            }
                        case ".xlsm":
                            {
                                await grid.SaveAsync(file.Path,
                                    "FlexGrid Sheet",
                                    SaveFileFormat.Xlsm,
                                    new GridRowColRanges(txtRowRanges.Text, rows),
                                    new GridRowColRanges(txtColRanges.Text, columns),
                                    headers,
                                    merged: checkMerged.IsChecked ?? false,
                                    formatted: checkFormatted.IsChecked ?? false,
                                    renderImages: checkRenderImages.IsChecked ?? false);
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = file.Path, UseShellExecute = true });
                                break;
                            }
                        default:
                            var dialog = new ContentDialog()
                            {
                                Title = "Export Type not supported",
                                PrimaryButtonText = AppResources.OK,
                            };
                            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
                            {
                                dialog.XamlRoot = grid.XamlRoot;
                            }
                            var result = await dialog.ShowAsync();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var dialog = new ContentDialog()
                    {
                        Title = ex.Message,
                        PrimaryButtonText = AppResources.OK,
                    };
                    if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
                    {
                        dialog.XamlRoot = grid.XamlRoot;
                    }
                    var result = await dialog.ShowAsync();
                }
            }
            catch { }
        }

        #region Header Option Event(s)

        private void CheckHeadersAll_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (checkHeadersAll.IsChecked ?? false)
            {
                checkHeadersRow.IsChecked = true;
                checkHeadersRow.IsEnabled = false;
                checkHeadersColumn.IsChecked = true;
                checkHeadersColumn.IsEnabled = false;
            }
            else
            {
                checkHeadersRow.IsChecked = false;
                checkHeadersRow.IsEnabled = true;
                checkHeadersColumn.IsChecked = false;
                checkHeadersColumn.IsEnabled = true;
            }
        }

        private void CheckHeadersRow_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if ((checkHeadersRow.IsChecked ?? false) &&
                (checkHeadersColumn.IsChecked ?? false))
            {
                checkHeadersAll.IsChecked = true;
                checkHeadersAll.IsEnabled = false;
            }
            else if (!checkHeadersAll.IsEnabled)
            {
                checkHeadersAll.IsChecked = false;
                checkHeadersAll.IsEnabled = true;
            }
        }

        #endregion

        #region Playground Event(s)

        #region Grouping Event(s)

        private void Grouping_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            IsGroupingClicked = true;
        }

        private void AddGroup_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var parent = (sender as Button).Parent as StackPanel;
            var parentChildren = parent.Children;
            if (parentChildren.Count >= 3)
            {
                if (parentChildren.Count == 3)
                {
                    var newButton = new Button() { Content = "X", MinWidth = 20 };
                    ToolTipService.SetToolTip(newButton, "Remove group");
                    newButton.Click += RemoveGroup_Click;
                    parentChildren.Add(newButton);
                }
                parent.Children.RemoveAt(2);
            }

            var newStack = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
            var textBlock = new TextBlock() { Text = AppResources.GroupByLabel, VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center, Margin = new Thickness(0, 0, 5, 0) };
            var comboBox = new ComboBox() { Width = 200, Margin = new Thickness(0, 0, 5, 0) };
            var items = new List<string>(ValidColumns);
            items.RemoveAll(x => Groupings.Contains(x));
            comboBox.ItemsSource = items;
            comboBox.SelectionChanged += Grouping_0_SelectedItemChanged;
            comboBox.KeyDown += Grouping_KeyDown;
            comboBox.PointerPressed += Grouping_PointerPressed;
            var addButton = new Button()
            {
                MinWidth = 20,
                Content = "+",
                Margin = new Thickness(0, 0, 5, 0),
                IsEnabled = false,
            };
            ToolTipService.SetToolTip(addButton, "Add another group");
            addButton.Click += AddGroup_Click;

            var removeButton = new Button() { Content = "X", MinWidth = 20 };
            ToolTipService.SetToolTip(removeButton, "Remove group");
            removeButton.Click += RemoveGroup_Click;

            newStack.Children.Add(textBlock);
            newStack.Children.Add(comboBox);
            if (GroupingStack.Children.Count < ValidColumns.Count)
            {
                newStack.Children.Add(addButton);
            }
            newStack.Children.Add(removeButton);
            GroupingStack.Children.Add(newStack);
        }

        private async void RemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            var stackPanel = (sender as Button).Parent as StackPanel;
            var stackCombo = stackPanel.Children[1] as ComboBox;
            var selectedValue = stackCombo.SelectedValue?.ToString();

            var indexOfGroup = Groupings.IndexOf(selectedValue);
            var groups = DataCollection.GroupDescriptions.ToList();
            if (indexOfGroup >= 0 && indexOfGroup < groups.Count)
            {
                groups.RemoveAt(indexOfGroup);
                await DataCollection.GroupAsync(groups.ToArray());
            }

            var parentChildren = GroupingStack.Children;
            var indexOf = parentChildren.IndexOf(stackPanel);
            var properIndex = indexOf - 1;
            if (properIndex < Groupings.Count)
            {
                Groupings.RemoveAt(properIndex);
            }
            parentChildren.Remove(stackPanel);

            for (int i = 1; i < parentChildren.Count; i++)
            {
                var child = parentChildren[i] as StackPanel;
                var childCombo = child.Children[1] as ComboBox;
                var items = new List<string>(childCombo.ItemsSource as IEnumerable<string>);
                if (!string.IsNullOrEmpty(selectedValue))
                {
                    items.Add(selectedValue);
                }

                var childComboSelectedValue = childCombo.SelectedValue;
                childCombo.ClearValue(ItemsControl.ItemsSourceProperty);
                var orderedItems = ValidColumns.Intersect(items);
                childCombo.ItemsSource = orderedItems;
                childCombo.SelectedValue = childComboSelectedValue;
            }

            var lastStack = parentChildren[parentChildren.Count - 1] as StackPanel;
            var textBlock = lastStack.Children[0];
            var comboBox = lastStack.Children[1] as ComboBox;
            var comboBoxSelectedValue = comboBox.SelectedValue?.ToString();
            lastStack.Children.Clear();
            lastStack.Children.Add(textBlock);
            lastStack.Children.Add(comboBox);

            var addButton = new Button()
            {
                MinWidth = 20,
                Content = "+",
                Margin = new Thickness(0, 0, 5, 0),
                IsEnabled = !string.IsNullOrEmpty(comboBoxSelectedValue),
            };
            ToolTipService.SetToolTip(addButton, "Add another group");
            addButton.Click += AddGroup_Click;
            lastStack.Children.Add(addButton);

            if (parentChildren.Count > 2)
            {
                var removeButton = new Button() { Content = "X", MinWidth = 20 };
                ToolTipService.SetToolTip(removeButton, "Remove group");
                removeButton.Click += RemoveGroup_Click;
                lastStack.Children.Add(removeButton);
            }
        }

        private async void GroupingClear_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Groupings.Clear();
            await DataCollection.GroupAsync();
            for (int i = 1; i < GroupingStack.Children.Count;)
            {
                GroupingStack.Children.RemoveAt(i);
            }

            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0),
            };

            var textBlock = new TextBlock()
            {
                Text = AppResources.GroupByLabel,
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 5, 0),
            };
            stackPanel.Children.Add(textBlock);

            var comboBox = new ComboBox()
            {
                Width = 200,
                Margin = new Thickness(0, 0, 5, 0),
            };

            var columns = grid.Columns.Select(x => x.ColumnName).ToList();
            Groupings = new List<string>();
            ValidColumns = columns;

            comboBox.ItemsSource = columns;
            comboBox.SelectionChanged += Grouping_0_SelectedItemChanged;
            comboBox.KeyDown += Grouping_KeyDown;
            comboBox.PointerPressed += Grouping_PointerPressed;
            stackPanel.Children.Add(comboBox);

            var button = new Button()
            {
                MinWidth = 20,
                Margin = new Thickness(0, 0, 5, 0),
                IsEnabled = false,
                Content = "+",
            };
            ToolTipService.SetToolTip(button, "Add another group");
            button.Click += AddGroup_Click;
            stackPanel.Children.Add(button);

            GroupingStack.Children.Add(stackPanel);
        }


        private void Grouping_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Back)
            {
                IsGroupingClicked = true;
            }
        }

        #endregion

        #endregion

        private async void Grouping_0_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                IsGroupingClicked = false;
                var comboBox = sender as ComboBox;
                var stackPanel = comboBox.Parent as StackPanel;
                var selectedValue = comboBox.SelectedValue?.ToString();
                var addButton = stackPanel.Children[2] as Button;

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    if (Groupings.Contains(selectedValue))
                    {
                        var dialog = new ContentDialog()
                        {
                            Title = "This grouping already exists",
                            PrimaryButtonText = AppResources.OK,
                            SecondaryButtonText = AppResources.Cancel,
                        };
                        if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
                        {
                            dialog.XamlRoot = grid.XamlRoot;
                        }
                        var result = await dialog.ShowAsync();
                        comboBox.SelectedItem = null;
                        return;
                    }
                    var children = GroupingStack.Children;
                    var indexOf = children.IndexOf(stackPanel);
                    if (Groupings.Count == indexOf - 1)
                    {
                        Groupings.Add(selectedValue);
                        var groups = DataCollection.GroupDescriptions.ToList();
                        groups.Add(new GroupDescription(selectedValue));
                        await DataCollection.GroupAsync(groups.ToArray());
                    }
                    else
                    {
                        Groupings[indexOf - 1] = selectedValue;
                        var groups = DataCollection.GroupDescriptions.ToList();
                        groups[indexOf - 1] = new GroupDescription(selectedValue);
                        await DataCollection.GroupAsync(groups.ToArray());
                    }
                    addButton.IsEnabled = true;


                    for (int i = 1; i < children.Count; i++)
                    {
                        var child = children[i] as StackPanel;
                        if (child != stackPanel)
                        {
                            var childCombo = child.Children[1] as ComboBox;
                            var items = new List<string>(childCombo.ItemsSource as IEnumerable<string>);
                            items.Remove(selectedValue);
                            //if (e.RemovedItems.Count > 0)
                            //    items.Add(e.OldValue.ToString());

                            var childComboSelectedValue = childCombo.SelectedValue;
                            childCombo.ClearValue(ItemsControl.ItemsSourceProperty);
                            var orderedItems = ValidColumns.Intersect(items);
                            childCombo.ItemsSource = orderedItems;
                            childCombo.SelectedValue = childComboSelectedValue;
                        }
                    }
                }
                else
                {
                    addButton.IsEnabled = false;
                }
            }
        }


        private void Themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    if (e.AddedItems?.Count > 0)
            //    {
            //        string newTheme = (string)e.AddedItems[0];
            //        if ("Classic".Equals(newTheme))
            //        {
            //            grid.Style = FlexGrid.ClassicStyle;
            //        }
            //        else if (e.AddedItems[0] == null || !Resources.MergedDictionaries[0].Contains(newTheme))
            //        {
            //            // clear style
            //            grid.ClearValue(StyleProperty);
            //        }
            //        else
            //        {
            //            grid.Style = Resources.MergedDictionaries[0][newTheme] as Style;
            //        }
            //    }

            //    else
            //    {
            //        // reset to default theme.
            //        grid.ClearValue(StyleProperty);
            //    }
        }

    }
}
