using Microsoft.UI.Xaml.Controls;

namespace InputExplorer
{
    /// <summary>
    /// Interaction logic for InputView.xaml
    /// </summary>
    public partial class InputView : UserControl
    {
        public InputView()
        {
            InitializeComponent();
            Tag = Properties.Resources.Input;
        }

        private void C1Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            c1Button.Content = "Clicked";
        }

        private void OnAutoComplete(object sender, C1.WinUI.Input.TextBoxAutoCompleteEventArgs e)
        {
            e.AutoComplete = "AutoCompleted";
        }
    }
}
