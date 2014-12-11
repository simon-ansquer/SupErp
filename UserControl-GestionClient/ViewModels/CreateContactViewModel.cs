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
    public class CreateContactViewModel
    {
        public Contact NewContact {get; set;}
        public Dictionary<long, string> ListEntreprise { get; set; }
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
            ListEntreprise = new Dictionary<long,string>();
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                List<ClientServiceGestionClient.Company> companies = ws.GetListCompany().ToList();
                foreach(ClientServiceGestionClient.Company c in companies)
                {
                    ListEntreprise.Add(c.id, c.name);
                }
            }
            NewContact = new Contact();
            AddCustomer = new DelegateCommand(new Action<object>(AddNewCustomer));
        }
        private void AddNewCustomer(object contact)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {                
                ClientServiceGestionClient.Company_Contact cont = new ClientServiceGestionClient.Company_Contact { email = NewContact.email, firstname = NewContact.email, gender = NewContact.gender, lastname = NewContact.lastname, phone = NewContact.phone, company_id = NewContact.company_id };
                ws.CreateCompany_Contact(cont);
            }
        }
    }
}