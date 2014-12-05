using System;
using System.Collections.Generic;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.Models;
using WpfControlLibrarySalaire.Views;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        private ServiceSalaire.User _employee;

        public ServiceSalaire.User Employee
        {
            get { return _employee; }
        }

        public EmployeeHistoryViewModel(ServiceSalaire.User employee)
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
