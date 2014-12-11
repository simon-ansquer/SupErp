using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.Models;

namespace UserControl_GestionClient.ViewModels
{
    public class DetailCustomerViewModel : NotificationObject
    {
        private int id;

        public ICommand DeleteCustomer { get; set; }

        public DetailCustomerViewModel(int id)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                List<ClientServiceGestionClient.Company_Contact> lstcont = ws.GetListCompany_ContactById(id).ToList();
                Company comp = ws.GetCompany(id).ToCompany();
                List<Contact> listContacts = new List<Contact>();
                foreach (ClientServiceGestionClient.Company_Contact c in lstcont)
                {
                    listContacts.Add(new Contact { company_id = c.company_id, email = c.email, firstname = c.firstname, lastname = c.lastname, gender = c.gender, id = c.id, phone = c.phone });
                }
                comp.Company_Contact = listContacts;
                Customer = comp;
            }
            DeleteCustomer = new DelegateCommand(new Action<object>(DeleteNewCustomer));
        }

        private void DeleteNewCustomer(object obj)
        {

        }

        private Models.Company _customer;

        public Models.Company Customer
        {
            get { return _customer; }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    RaisePropertyChanged(() => Customer);
                }
            }
        }

    }
}
