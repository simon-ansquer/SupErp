using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.Models;
using UserControl_GestionClient.Views;

namespace UserControl_GestionClient.ViewModels
{
    public class CreateCompanieViewModel
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

        public CreateCompanieViewModel()
        {
            NewContact = new Company_Contact();
            AddCustomer = new DelegateCommand(new Action<object>(AddNewCustomer));
        }
        private void AddNewCustomer(object contact)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                ClientServiceGestionClient.Company comp = new ClientServiceGestionClient.Company { address = NewContact.address, city = NewContact.city, name = NewContact.name, postalcode = NewContact.postalcode, siret = NewContact.siret };
                
                int idComp = ws.CreateCompany(comp);
                ClientServiceGestionClient.Company_Contact cont = new ClientServiceGestionClient.Company_Contact { email = NewContact.email, firstname = NewContact.email, gender = NewContact.gender, lastname = NewContact.lastname, phone = NewContact.phone, company_id = idComp };
                ws.CreateCompany_Contact(cont);
            }
            Switcher.Switch(new AccueilGestionClient());
        }
    }
}