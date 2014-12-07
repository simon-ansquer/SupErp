using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using System.Collections.ObjectModel;
using WpfControlLibrarySalaire.ServiceSalaire;
using WpfControlLibrarySalaire.Views;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeDetailsViewModel : BaseViewModel
    {
        #region Properties
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

        private DateTime _inputAbscenceStart;
        public DateTime InputAbscenceStart
        {
            get { return _inputAbscenceStart; }
            set
            {
                _inputAbscenceStart = value;
                _addAbscenceClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _inputAbscenceEnd;
        public DateTime InputAbscenceEnd
        {
            get { return _inputAbscenceEnd; }
            set
            {
                _inputAbscenceEnd = value;
                _addAbscenceClickCommand.RaiseCanExecuteChanged();
            }
        }

        private Status _userStatus;

        public Status UserStatus
        {
            get { return _userStatus; }
            set
            {
                _userStatus = value;
                RaisePropertyChanged(() => UserStatus);
            }
        }
        #endregion

        public EmployeeDetailsViewModel(User employee)
        {
            Employee = employee;
            UserStatus = employee.Status;
            if(UserStatus == null)
                UserStatus = new Status(){id = -1};
            else
                UserStatus.id -= 1;
            InitializeStatus();
            _addPrimeClickCommand = new DelegateCommand<string>(
                OnAddPrimeButtonClick
            );
            _addAbscenceClickCommand = new DelegateCommand<string>(OnAddAbscenceButtonClick);

        }

        private async void InitializeStatus()
        {
            var status = await ServiceSalaire.GetStateAsync();
            if (ServiceSalaire != null) ListStatus = new ObservableCollection<Status>((IEnumerable<Status>)status);
        }

        #region Commands
        private readonly DelegateCommand<string> _addAbscenceClickCommand;
        public DelegateCommand<string> ButtonAddAbsenceClickCommand
        {
            get { return _addAbscenceClickCommand; }
        }

        private readonly DelegateCommand<string> _addPrimeClickCommand;
        public DelegateCommand<string> ButtonAddPrimeClickCommand
        {
            get { return _addPrimeClickCommand; }
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

        public ICommand HistoryCommand
        {
            get
            {
                return new DelegateCommand<User>(OnHistoryClick);
            }
        }

        public ICommand SaveCommand
        {
            get { return new DelegateCommand<string>(OnSaveClick); }
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

        private void OnAddPrimeButtonClick(string s)
        {
            try
            {
                int primePrice = int.Parse(InputPrimePrice);
                if (InputPrimeName == string.Empty)
                    throw new ArgumentNullException();
                //ServiceSalaire.addPrime(Employee.Id, new Prime());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Les paramètres sont invalides, recommencer", "Erreur de paramètres",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Merci d'indiquer un nombre pour le montant.", "Erreur de montant", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnAddAbscenceButtonClick(string s)
        {
            try
            {
                var absenceStart = InputAbscenceStart;
                var absenceEnd = InputAbscenceEnd;

                //ServiceSalaire.addPrime(Employee.Id, new Prime());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Les paramètres sont invalides, recommencer", "Erreur de paramètres",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Merci d'indiquer un nombre pour le montant.", "Erreur de montant", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnSaveClick(string s)
        {
            // update the net salary and the status
            decimal newSalary;
            if (!decimal.TryParse(Employee.Salaries[0].NetSalary.ToString(), out newSalary))
            {
                MessageBox.Show("Mauvaise valeur entrée pour le salaire.", "Mauvais salaire", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (UserStatus.id == -1)
            {
                return;
            }
            try
            {
                // check if status is changed
                if (UserStatus.Label != Employee.Status.Label)
                {
                    if (!ServiceSalaire.UpdateUserState(Employee.Id, UserStatus.id))
                    {
                        MessageBox.Show(
                            "Une erreur est survenue pendant la mise à jour du statut,\nVeuillez réessayer.",
                            "Erreur mise à jour statut", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (!ServiceSalaire.UpdateUserSalaryById(Employee.Id, newSalary))
                    MessageBox.Show("Une erreur est survenue pendant la mise à jour du salaire,\nVeuillez réessayer.",
                        "Erreur mise à jour salaire", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Le service n'est pas disponible pour le moment", "Service non disponible",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
