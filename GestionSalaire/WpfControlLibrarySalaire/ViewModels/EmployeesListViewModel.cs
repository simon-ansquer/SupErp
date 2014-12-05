using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.ServiceSalaire;
using WpfControlLibrarySalaire.Views;


namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeesListViewModel : BaseViewModel
    {
        private readonly DelegateCommand<string> _searchClickCommand;
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
                Employees = new ObservableCollection<User>((IEnumerable<User>) employees);
            }
            catch (Exception)
            {
                ServiceSalaire.Close();
            }
        }

        #region DelegateCommand
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
                    s => OnGeneratePDFClick()
                    );
            }
        }

        public ICommand DetailsCommand
        {
            get
            {
                return new DelegateCommand<int>(OnDetailsClick);
            }
        }

        public ICommand PdfCommand
        {
            get { return new DelegateCommand<int>(OnPdfClick); }
        }
        #endregion

        #region Command Handlers
        private void OnGeneratePDFClick()
        {
            MessageBox.Show("PDF");
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

        private void OnDetailsClick(int index)
        {
            var employeeDetails = new EmployeeDetails(new EmployeeDetailsViewModel(Employees[index]));
            Switcher.Switch(employeeDetails);
        }

        private void OnPdfClick(int index)
        {
            
        }
        #endregion
    }
}
