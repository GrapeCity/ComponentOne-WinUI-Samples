using C1.Chart;
using C1.WinUI.Chart;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace FlexChartExplorer.Samples
{
#pragma warning disable 1591
    public partial class ImageExport : UserControl
    {
        int cnt = 5000;
        string[] coef = new string[]
        {
          "AMTMNQQXUYGA",
          "CVQKGHQTPHTE",
          "FIRCDERRPVLD",
          "GIIETPIQRRUL",
          "GLXOESFTTPSV",
          "GXQSNSKEECTX",
        };

        public ImageExport()
        {
            InitializeComponent();
            Loaded += (s, e) => CreateData();
            btnNew.Click += (s, e) => CreateData();
            btnSave.Click += (s, e) => SaveImage();
        }

        async void SaveImage()
        {
            var filePicker = new FileSavePicker();
            
            filePicker.FileTypeChoices.Add("PNG image", new List<string>() { ".png" });
            filePicker.FileTypeChoices.Add("JPEG image", new List<string>() { ".jpg" });
            filePicker.SuggestedFileName = "FlexChart";

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(((App)App.Current).MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            var file = await filePicker.PickSaveFileAsync();
            if (file != null)
            {
                var format = ImageFormat.Png;
                if(file.FileType == ".jpg")
                    format = ImageFormat.Jpeg;

                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await flexChart.SaveImage(stream, format);
                }
            }
        }

        void CreateData()
        {
            flexChart.BeginUpdate();
            flexChart.ItemsSource = DataSource.Create(coef, cnt);
            flexChart.EndUpdate();
        }

        class DataSource
        {
            static Random rnd = new Random();

            public static Point[] Create(string[] coef, int count)
            {
                var points = new Point[count];

                double[] c = StringToCoeff(coef[rnd.Next(coef.Length)]);

                for (int i = 1; i < count; i++)
                    points[i] = Iterate(points[i - 1].X, points[i - 1].Y, c);

                return points;
            }

            static Point Iterate(double x, double y, double[] c)
            {
                double x1 = c[0] + c[1] * x + c[2] * x * x + c[3] * x * y + c[4] * y + c[5] * y * y;
                double y1 = c[6] + c[7] * x + c[8] * x * x + c[9] * x * y + c[10] * y + c[11] * y * y;

                return new Point() { X = x1, Y = y1 };
            }

            static double[] StringToCoeff(string s)
            {
                int len = s.Length;
                double[] c = new double[len];

                for (int i = 0; i < len; i++)
                {
                    c[i] = -1.2 + 0.1 * (s[i] - 'A');
                }

                return c;
            }

        }
    }
}
