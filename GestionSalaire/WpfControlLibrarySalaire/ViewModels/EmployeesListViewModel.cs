using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;
using WpfControlLibrarySalaire.Models;
using WpfControlLibrarySalaire.ServiceSalaire;


namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeesListViewModel : BaseViewModel
    {
        public EmployeesListViewModel()
        {
            InitializeUsers();            
        }

        private async void InitializeUsers()
        {
            try
            {
                var employees = await serviceSalaire.GetUserAsync();
                Employees = new ObservableCollection<ServiceSalaire.User>(employees);
                //EmployeesView();
            }
            catch(Exception)
            {
                serviceSalaire.Close();
            }
        }

        private void EmployeesView()
        {
            foreach (ServiceSalaire.User employee in Employees)
            {
                
            }
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
