using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        private readonly Regex _regex;
        private string _inputPrimePrice;
        public string InputPrimePrice
        {
            get { return _inputPrimePrice; }
            set
            {
                _inputPrimePrice = !_regex.IsMatch(value) ? value : _inputPrimePrice;
                _addPrimeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _inputPrimeEnd;
        public DateTime InputPrimeEnd
        {
            get { return _inputPrimeEnd; }
            set
            {
                _inputPrimeEnd = value;
                _addPrimeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _inputAbsenceStart;
        public DateTime InputAbsenceStart
        {
            get { return _inputAbsenceStart; }
            set
            {
                _inputAbsenceStart = value;
                _addAbscenceClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _inputAbsenceEnd;
        public DateTime InputAbsenceEnd
        {
            get { return _inputAbsenceEnd; }
            set
            {
                _inputAbsenceEnd = value;
                _addAbscenceClickCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<AbsenceType> _absenceTypes;

        public ObservableCollection<AbsenceType> AbsenceTypes
        {
            get { return _absenceTypes; }
            set
            {
                _absenceTypes = value;
                RaisePropertyChanged(() => AbsenceTypes);
            }
        }

        private AbsenceType _absenceType;
        public AbsenceType AbsenceType
        {
            get { return _absenceType; }
            set
            {
                _absenceType = value;
                RaisePropertyChanged(() => AbsenceType);
            }
        }
        #endregion

        public EmployeeDetailsViewModel(User employee)
        {
            Employee = employee;
            UserStatus = employee.Status;
            if(UserStatus == null)
                UserStatus = new Status {id = -1};
            else
                UserStatus.id -= 1;
            if(Employee.Salaries.Count == 0)
                Employee.Salaries.Add(new Salary());
            InitializeStatus();
            InitializeAbsenceTypes();
            InitializePrimes();
            _addPrimeClickCommand = new DelegateCommand<string>(
                OnAddPrimeButtonClick/*,
                s => !string.IsNullOrEmpty(InputPrimeName) && !string.IsNullOrEmpty(InputPrimePrice)*/
            );
            _addAbscenceClickCommand = new DelegateCommand<string>(OnAddAbscenceButtonClick);
            _regex = new Regex(@"[^0-9.,]+");
            InputPrimeEnd = DateTime.Today;
            InputAbsenceStart = DateTime.Now;
            InputAbsenceEnd = DateTime.Now;
        }

        private async void InitializePrimes()
        {
            try
            {
                var primes = ServiceSalaire.GetPrimesByUserId(Employee.Id);
                Employee.Primes = primes;
            }
            catch (Exception) { }
        }

        private async void InitializeStatus()
        {
            try
            {
                var status = await ServiceSalaire.GetStateAsync();
                if (ServiceSalaire != null) ListStatus = new ObservableCollection<Status>((IEnumerable<Status>) status);
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur est survenue,\nveuillez réessayer", "Erreur", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void InitializeAbsenceTypes()
        {
            try
            {
                var listAbsencetypes = await ServiceSalaire.GetAbsenceTypesAsync();
                AbsenceTypes = new ObservableCollection<AbsenceType>(listAbsencetypes);
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur est survenue,\nveuillez réessayer", "Erreur", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
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
                decimal primePrice = decimal.Parse(InputPrimePrice);
                if (InputPrimeName == string.Empty || InputPrimeEnd < DateTime.Now)
                    throw new ArgumentNullException();
                var newPrime = new Prime
                {
                    Label = InputPrimeName,
                    Price = primePrice,
                    StartDate = DateTime.Now,
                    EndDate = InputPrimeEnd
                };
                var userId = Employee.Id;
                ServiceSalaire.addPrime(userId, newPrime);
                // Update the employee
                Employee = ServiceSalaire.GetUserById(userId);
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
            catch (Exception)
            {
                MessageBox.Show("Le service n'est pas disponible pour le moment", "Service non disponible",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnAddAbscenceButtonClick(string s)
        {
            try
            {
                if(AbsenceType == null || InputAbsenceStart > InputAbsenceEnd)
                    throw new ArgumentNullException();
                var newAbsence = new Absence
                {
                    StartDate = InputAbsenceStart,
                    EndDate = InputAbsenceEnd,
                    AbsenceType_id = AbsenceType.id
                };
                var userId = Employee.Id;
                ServiceSalaire.addAbsence(userId, newAbsence);
                Employee = ServiceSalaire.GetUserById(userId);
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
                var currentUserStatus = Employee.Status != null ? Employee.Status.Label : string.Empty;
                if (UserStatus.Label != currentUserStatus)
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
