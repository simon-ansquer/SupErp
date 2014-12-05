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
            this.DataContext = viewModel;
        }
    }
}
