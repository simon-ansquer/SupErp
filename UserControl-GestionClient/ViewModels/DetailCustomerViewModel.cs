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
    public class DetailCustomerViewModel
    {
        public Company Customer {get; set; }
        public ICommand DeleteCustomer { get; set; }
        public DetailCustomerViewModel(int id)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                ClientServiceGestionClient.Company comp = ws.GetCompany(id);
                List<Contact> lstcont = new List<Contact>();
                foreach(ClientServiceGestionClient.Company_Contact c in comp.Company_Contact)
                {
                    lstcont.Add(new Contact{ company_id = c.company_id, email = c.email, firstname = c.firstname, lastname = c.lastname, gender = c.gender, id = c.id, phone = c.phone});
                }
                Customer = new Company { address = comp.address, id = comp.id, city = comp.city, name = comp.name, siret = comp.siret, postalcode = comp.postalcode, Company_Contact = lstcont};
            }
            DeleteCustomer = new DelegateCommand(new Action<object>(DeleteNewCustomer));
        }

        private void DeleteNewCustomer(object obj)
        {

        }
    }
}
