using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using System.Collections.ObjectModel;
using WpfControlLibrarySalaire.Views;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeDetailsViewModel : BaseViewModel
    {
        private ServiceSalaire.User _employee;
        public ServiceSalaire.User Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                if (_employee == value) return;
                _employee = value;
                RaisePropertyChanged(() => Employee);
            }
        }

        private ObservableCollection<ServiceSalaire.Status> _listStatus;

        public ObservableCollection<ServiceSalaire.Status> ListStatus
        {
            get { return _listStatus; }
        } 

        public EmployeeDetailsViewModel(ServiceSalaire.User employee)
        {
            Employee = employee;
            //TODO : _listStatus = ServiceSalaire.GetStatusAsync();
        }


        #region Commands
        public ICommand PreviousCommand
        {
            get
            {
                return new DelegateCommand<string>(
                    s => OnPreviousClick()
                    );
            }
        }

        public ICommand HistoryCommand
        {
            get
            {
                return new DelegateCommand<ServiceSalaire.User>(OnHistoryClick);
            }
        }

        public ICommand PdfCommand
        {
            get { return new DelegateCommand<int>(OnPdfClick); }
        }
        #endregion

        #region Command Handlers
        
        private void OnPreviousClick()
        {
            Switcher.Switch(new EmployeesList());
        }

        private async void OnSearchButtonClick()
        {
            
        }

        private void OnHistoryClick(ServiceSalaire.User employee)
        {
            var employeeHistory = new EmployeeHistory(new EmployeeHistoryViewModel(employee));
            Switcher.Switch(employeeHistory);
        }

        private void OnPdfClick(int index)
        {
            
        }
        #endregion

    }
}
