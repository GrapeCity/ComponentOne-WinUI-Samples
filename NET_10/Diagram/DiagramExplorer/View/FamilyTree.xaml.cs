using C1.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class FamilyTree : UserControl, IDiagramHolder
    {
        public FamilyTree()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "family-tree.mermaid");
        }

        public FlexDiagram Diagram => diagram;
    }
}
