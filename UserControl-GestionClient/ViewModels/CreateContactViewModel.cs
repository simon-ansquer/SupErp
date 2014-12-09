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

        private Company_Contact newContact;
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        public Company_Contact NewContact
        {
            get { return this.newContact; }
            set
            {
                this.newContact = value;
            }
        }
        /*private string thestring;
        public string Thestring
        {
            get { return this.thestring; }
            set
            {
                this.thestring = value;
            }
        }*/
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
            AddCustomer = new DelegateCommand(new Action<object>(AddNewCustomer));
        }
        private void AddNewCustomer(object contact)
        {
            Company_Contact c = this.NewContact;
            //this.Thestring = (string)contact;
        }
    }
}
