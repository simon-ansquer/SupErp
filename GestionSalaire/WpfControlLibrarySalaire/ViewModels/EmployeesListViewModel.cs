using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfControlLibrarySalaire.Helpers;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeesListViewModel : BaseViewModel
    {
        private List<>

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

        }

        private void OnSearchButtonClick()
        {
            
        } 
        #endregion
    }
}
