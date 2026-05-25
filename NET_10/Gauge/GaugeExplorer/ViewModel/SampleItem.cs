using Microsoft.UI.Xaml.Controls;
using System;

namespace GaugeExplorer
{
    /// <summary>
    /// Represents a sample.
    /// </summary>
    public class SampleItem
    {
        private Lazy<UserControl> _sample;

        /// <summary>
        /// Initializes a new instance of <see cref="SampleItem"/>.
        /// </summary>
        /// <param name="title">The title of the sample.</param>
        /// <param name="getSample">The function that creates the sample.</param>
        public SampleItem(string title, Func<UserControl> getSample)
        {
            Title = title;
            _sample = new System.Lazy<UserControl>(getSample);
        }

        /// <summary>
        /// Gets the sample title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description of the sample.
        /// </summary>
        public string Description => Sample?.Tag?.ToString() ?? "";

        /// <summary>
        /// Gets the actual sample.
        /// </summary>
        public UserControl Sample
        {
            get
            {
                return _sample.Value;
            }
        }
    }
}
