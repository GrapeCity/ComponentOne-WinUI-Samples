using C1.Document;
using C1.Report;
using C1.WinUI.Viewer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Core;
using static C1.Report.DesignStrings;
using Uri = System.Uri;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FlexViewer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, IPrintUIHandler
    {
        public MainWindow()
        {
            this.InitializeComponent();
            flexViewer.SetPrintHandler(this);
            LoadPdf();
        }

        private void LeftNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }


        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "pdf":
                    ComboBox.Visibility = Visibility.Collapsed;
                    LoadPdf();
                    break;

                case "flexreport":
                    ComboBox.Visibility = Visibility.Visible;
                    string flexReportPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\FlexCommonTasks.flxr";
                    string[] ReportList = FlexReport.GetReportList(flexReportPath);
                    ComboBox.ItemsSource = ReportList;
                    ComboBox.SelectedIndex = 0;
                    break;

                case "reportservice":
                    ComboBox.Visibility = Visibility.Collapsed;
                    LoadSSRS();
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox.SelectedItem != null)
            {
                var flexReport = new FlexReport();
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\FlexCommonTasks.flxr";
                flexReport.Load(path, ComboBox.SelectedItem.ToString());
                flexReport.Generate();
                flexViewer.DocumentSource = flexReport;
            }
        }

        private void LoadPdf()
        {
            var pdfDocumentSource = new C1PdfDocumentSource();
            string projectPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = projectPath + @"\DefaultDocument.pdf";
            pdfDocumentSource.LoadFromFile(path);
            flexViewer.DocumentSource = pdfDocumentSource;
        }

        private void LoadSSRS()
        {
            var c1SsrsDocumentSource1 = new C1SSRSDocumentSource();
            c1SsrsDocumentSource1.Language = new System.Globalization.CultureInfo("en-US");
            c1SsrsDocumentSource1.Credential = new NetworkCredential("ssrs_demo", "bjqveS5gh89BH1Q", "");
            c1SsrsDocumentSource1.DocumentLocation = new SSRSReportLocation("http://ssrs.componentone.com:8000/ReportServer", "/AdventureWorks/Employee Sales Summary Detail");
            c1SsrsDocumentSource1.Generate();
            flexViewer.DocumentSource = c1SsrsDocumentSource1;
        }

        private void ClickableLabelButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://developer.mescius.com/componentone/winui-controls");
            Windows.System.Launcher.LaunchUriAsync(uri);
        }

        async Task IPrintUIHandler.ShowPrintUI()
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            if (hwnd == IntPtr.Zero)
                throw new InvalidOperationException("No valid window handle found for printing.");

            await flexViewer.Pane.ShowPrintUI(hwnd);
        }
    }
}
