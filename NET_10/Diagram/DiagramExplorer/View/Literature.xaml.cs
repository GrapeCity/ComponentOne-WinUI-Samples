using C1.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class Literature : UserControl, IDiagramHolder
    {
        public Literature()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "literature.mermaid");
        }

        public FlexDiagram Diagram => diagram;
    }
}
