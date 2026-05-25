using C1.Chart;
using C1.WinUI.Diagram;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public sealed partial class ProgrammingLanguages : UserControl, IDiagramHolder
    {
        public ProgrammingLanguages()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "programming-language-tree.mermaid");
        }

        public FlexDiagram Diagram => diagram;
    }
}
