using System.Text.RegularExpressions;
using System.Windows.Input;
using WpfControlLibrarySalaire.ViewModels;

namespace WpfControlLibrarySalaire.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetails
    {
        public EmployeeDetails(EmployeeDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
