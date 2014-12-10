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

        public Company_Contact NewContact {get; set;}
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        private ICommand addCustomer;

        public ICommand AddCustomer
        {
            get
            {
                return addCustomer;
            }
            set
            {
                addCustomer = value;
            }
        }

        public CreateContactViewModel()
        {
            NewContact = new Company_Contact();
            AddCustomer = new DelegateCommand(new Action<object>(AddNewCustomer));
        }
        private void AddNewCustomer(object contact)
        {
            Company_Contact c = this.NewContact;
            //this.Thestring = (string)contact;
        }
    }
}
