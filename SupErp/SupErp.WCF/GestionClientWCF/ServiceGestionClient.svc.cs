using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SupErp.BLL.GestionClientBLL;
using SupErp.Entities;
using System.Collections.Generic;

namespace SupErp.WCF.GestionClientWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceGestionClient : IServiceGestionClient
    {

        //////////////////////////////
        //  COMPANY
        //////////////////////////////

        private static readonly Lazy<ClientBLL> LazyClientBLL = new Lazy<ClientBLL>(() => new ClientBLL());
        private static ClientBLL clientBLL { get { return LazyClientBLL.Value; } }
        
        public bool CreateCompany(Company company)
        {
            return clientBLL.CreateCompany(company);
        }

        public Company GetCompany(int idCompany)
        {
            return clientBLL.GetCompany(idCompany);
        }

        public List<Company> GetListCompany()
        {
            return clientBLL.GetListCompany();
        }

        public bool EditCompany(Company company)
        {
            return clientBLL.EditCompany(company);
        }


        //////////////////////////////
        //  COMPANY_CONTACT
        //////////////////////////////

        private static readonly Lazy<Company_ContactBLL> LazyContactBLL = new Lazy<Company_ContactBLL>(() => new Company_ContactBLL());
        private static Company_ContactBLL ContactBLL { get { return LazyContactBLL.Value; } }

        public bool CreateCompany_Contact(Company_Contact contact)
        {
            return ContactBLL.CreateCompany_Contact(contact);
        }

        public Company_Contact GetCompany_Contact(int idContact)
        {
            return ContactBLL.GetCompany_Contact(idContact);
        }

        public List<Company_Contact> GetListCompany_Contact()
        {
            return ContactBLL.GetListCompany_Contact();
        }

        public List<Company_Contact> GetListCompany_ContactById(int idCompany)
        {
            return ContactBLL.GetListCompany_Contact(idCompany);
        }

        public bool EditCompany_Contact(Company_Contact contact)
        {
            return ContactBLL.EditCompany_Contact(contact);
        }
    }
}
