using WpfControlLibrarySalaire.ViewModels;

namespace WpfControlLibrarySalaire.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeeHistory.xaml
    /// </summary>
    public partial class EmployeeHistory
    {
        public EmployeeHistory(EmployeeHistoryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
