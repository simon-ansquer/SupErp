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
        public DetailCustomerViewModel(Company comp)
        {
            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                List<ClientServiceGestionClient.Company_Contact> lstcont = ws.GetListCompany_ContactById(Convert.ToInt32(comp.id)).ToList();
                foreach(ClientServiceGestionClient.Company_Contact c in lstcont)
                {
                    comp.Company_Contact.Add(new Contact{ company_id = c.company_id, email = c.email, firstname = c.firstname, lastname = c.lastname, gender = c.gender, id = c.id, phone = c.phone});
                }
                Customer = comp;
            }
            DeleteCustomer = new DelegateCommand(new Action<object>(DeleteNewCustomer));
        }

        private void DeleteNewCustomer(object obj)
        {

        }
    }
}
