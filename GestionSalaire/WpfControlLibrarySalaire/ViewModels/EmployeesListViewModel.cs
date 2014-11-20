using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.Models;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeesListViewModel : BaseViewModel
    {
        private List<User> _employees;
        public List<User> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    RaisePropertyChanged(() => Employees);
                }
            }
        }

        public EmployeesListViewModel()
        {

        }

        #region Commands
        public ICommand SearchCommand { get { return new DelegateCommand(OnSearchButtonClick); } }
        public ICommand GeneratePDFCommand { get { return new DelegateCommand(OnGeneratePDFClick); } } 
        #endregion

        #region Command Handlers
        private void OnGeneratePDFClick()
        {
            MessageBox.Show("PDF");
        }

        private void OnSearchButtonClick()
        {
            MessageBox.Show("Search");
        } 
        #endregion
    }
}
