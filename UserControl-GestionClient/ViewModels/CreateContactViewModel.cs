using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.Models;

namespace UserControl_GestionClient.ViewModels
{
    class CreateContactViewModel
    {
        public Company_Contact CreateContact { get; set; }
        public ICommand AddCustomer { get { return new DelegateCommand(AddNewCustomer); } }
        private void AddNewCustomer()
        {

        }
    }
}
