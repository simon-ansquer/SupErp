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
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceGestionClient : IServiceGestionClient
    {

        //////////////////////////////
        //  COMPANY
        //////////////////////////////

        private static readonly Lazy<ClientBLL> LazyClientBLL = new Lazy<ClientBLL>(() => new ClientBLL());
        private static ClientBLL clientBLL { get { return LazyClientBLL.Value; } }
        
        [OperationContract]
        public bool CreateCompany(Company company)
        {
            return clientBLL.CreateCompany(company);
        }

        [OperationContract]
        public List<Company> GetCompany(int idCustomer)
        {
            return clientBLL.GetCompany(idCustomer);
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

        public List<Company_Contact> GetListCompany_Contact(int idCompany)
        {
            return ContactBLL.GetListCompany_Contact(idCompany);
        }
    }
}
