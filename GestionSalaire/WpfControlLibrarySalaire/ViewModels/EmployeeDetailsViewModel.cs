using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using System.Collections.ObjectModel;
using WpfControlLibrarySalaire.ServiceSalaire;
using WpfControlLibrarySalaire.Views;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeDetailsViewModel : BaseViewModel
    {
        private User _employee;
        public User Employee
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

        private ObservableCollection<Status> _listStatus;

        public ObservableCollection<Status> ListStatus
        {
            get { return _listStatus; }
            set
            {
                if (_listStatus == value) return;
                _listStatus = value;
                RaisePropertyChanged(() => ListStatus);
            }
        }

        private string _inputPrimeName;
        public string InputPrimeName
        {
            get { return _inputPrimeName; }
            set
            {
                _inputPrimeName = value;
                _addPrimeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _inputPrimePrice;
        public string InputPrimePrice
        {
            get { return _inputPrimePrice; }
            set
            {
                _inputPrimePrice = value;
                _addPrimeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private readonly DelegateCommand<object> _addPrimeClickCommand;
        public DelegateCommand<object> ButtonAddPrimeClickCommand
        {
            get { return _addPrimeClickCommand; }
        }

        public EmployeeDetailsViewModel(User employee)
        {
            Employee = employee;
            InitializeStatus();
            _addPrimeClickCommand = new DelegateCommand<object>(
                OnAddPrimeButtonClick
            );
        }
        

        private async void InitializeStatus()
        {
            var status = await ServiceSalaire.GetStateAsync();
            if (ServiceSalaire != null) ListStatus = new ObservableCollection<Status>((IEnumerable<Status>)status);
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
                return new DelegateCommand<User>(OnHistoryClick);
            }
        }
        #endregion

        #region Command Handlers
        
        private void OnPreviousClick()
        {
            Switcher.Switch(new EmployeesList());
        }

        private void OnHistoryClick(User employee)
        {
            var employeeHistory = new EmployeeHistory(new EmployeeHistoryViewModel(employee));
            Switcher.Switch(employeeHistory);
        }

        private void OnAddPrimeButtonClick(object values)
        {
            var input = InputPrimeName + InputPrimePrice;
            //ServiceSalaire.addPrime(Employee.Id, new Prime());
        }
        #endregion

    }
}
