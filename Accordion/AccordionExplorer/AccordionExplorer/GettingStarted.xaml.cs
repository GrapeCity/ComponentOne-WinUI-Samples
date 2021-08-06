using C1.WinUI.Accordion;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AccordionExplorer
{
    public sealed partial class GettingStarted : UserControl
    {
        public GettingStarted()
        {
            DataContext = this;
            this.InitializeComponent();
            expandModesCombo.ItemsSource = new List<ExpandMode> { ExpandMode.One, ExpandMode.Collapsible, ExpandMode.Any };
            expandModesCombo.SelectedIndex = 0;
            expandDirectionCombo.ItemsSource = new List<ExpandDirection> { ExpandDirection.Down, ExpandDirection.Up, ExpandDirection.Left, ExpandDirection.Right };
            expandDirectionCombo.SelectedIndex = 0;
        }

        private void OnExpandModesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.FirstOrDefault() is ExpandMode expandMode)
                accordion.ExpandMode = expandMode;
        }

        private void OnExpandDirectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.FirstOrDefault() is ExpandDirection expandDirection)
                accordion.ExpandDirection = expandDirection;

        }
    }
}
