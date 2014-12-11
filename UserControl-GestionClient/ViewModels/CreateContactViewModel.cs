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
    public class CreateContactViewModel : NotificationObject
    {
        public Contact NewContact {get; set;}
        public Dictionary<long, string> ListEntreprise { get; set; }
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>

        private ICommand addContact;

        public ICommand AddContact
        {
            get
            {
                return addContact;
            }
            set
            {
                addContact = value;
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
            AddContact = new DelegateCommand(new Action<object>(AddNewContact));
        }


        //On a pas réussi à récupérer l'item selectionné, on a mis le company_id est dur
        private void AddNewContact(object contact)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {                
                ClientServiceGestionClient.Company_Contact cont = new ClientServiceGestionClient.Company_Contact { email = NewContact.email, firstname = NewContact.firstname, gender = NewContact.gender, lastname = NewContact.lastname, phone = NewContact.phone, company_id = 3 };
                ws.CreateCompany_Contact(cont);
            }
            Switcher.Switch(new AccueilGestionClient());
        }
    }
}