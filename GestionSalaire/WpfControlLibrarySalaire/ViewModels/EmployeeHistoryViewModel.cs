using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.Views;
using WpfControlLibrarySalaire.ServiceSalaire;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        private readonly User _employee;

        public User Employee
        {
            get { return _employee; }
        }

        public EmployeeHistoryViewModel(User employee)
        {
            _employee = employee;
        }

        public EmployeeHistoryViewModel()
        {
            
        }

        public ICommand PreviousCommand
        {
            get
            {
                return new DelegateCommand<string>(
                    s => OnPreviousClick()
                    );
            }
        }

        private void OnPreviousClick()
        {
            Switcher.Switch(new EmployeeDetails(new EmployeeDetailsViewModel(Employee)));
        }
    }

}
