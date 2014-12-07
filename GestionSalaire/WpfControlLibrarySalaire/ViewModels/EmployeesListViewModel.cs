using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.ServiceSalaire;
using WpfControlLibrarySalaire.Views;


namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeesListViewModel : BaseViewModel
    {
        private string _inputSearch;
        public string InputSearch
        {
            get { return _inputSearch; }
            set
            {
                _inputSearch = value;
                _searchClickCommand.RaiseCanExecuteChanged();
            }
        }

        public EmployeesListViewModel()
        {
            InitializeUsers();
            _searchClickCommand = new DelegateCommand<string>(
                s => OnSearchButtonClick()
            );

        }

        private async void InitializeUsers()
        {
            try
            {
                var employees = await ServiceSalaire.GetUserAsync();
                
                Employees = new ObservableCollection<User>();
                foreach (var employee in employees)
                {
                    employee.Lastname = employee.Lastname.Trim();
                    employee.Firstname = employee.Firstname.Trim();
                    employee.Salaries.Reverse();
                    employee.Absences.Reverse();
                    Employees.Add(employee);
                }
            }
            catch (Exception)
            {
                ServiceSalaire.Close();
            }
        }

        #region DelegateCommand
        private readonly DelegateCommand<string> _searchClickCommand;
        public DelegateCommand<string> ButtonSearchClickCommand
        {
            get { return _searchClickCommand; }
        }
        #endregion


        #region Commands
        public ICommand GeneratePdfCommand
        {
            get
            {
                return new DelegateCommand<string>(
                    s => OnGeneratePdfClick()
                    );
            }
        }

        public ICommand DetailsCommand
        {
            get
            {
                return new DelegateCommand<User>(OnDetailsClick);
            }
        }

        public ICommand PdfCommand
        {
            get { return new DelegateCommand<User>(OnPdfClick); }
        }
        #endregion

        #region Command Handlers
        private void OnGeneratePdfClick()
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            foreach (User employee in Employees)
                PDFGenerator.generate(employee, fbd.SelectedPath);
        }

        private async void OnSearchButtonClick()
        {
            if (Employees == null) return;
            List<User> employees;
            if (_inputSearch == string.Empty)
                employees = await ServiceSalaire.GetUserAsync();
            else
                employees = await ServiceSalaire.SearchUserAsync(_inputSearch);
            Employees.Clear();
            Employees = new ObservableCollection<User>((IEnumerable<User>)employees);
        }

        private void OnDetailsClick(User userSelected)
        {
            var employeeDetails = new EmployeeDetails(new EmployeeDetailsViewModel(userSelected));
            Switcher.Switch(employeeDetails);
        }

        private void OnPdfClick(User userSelected)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            PDFGenerator.generate(userSelected, fbd.SelectedPath);
        }
        #endregion
    }
}
