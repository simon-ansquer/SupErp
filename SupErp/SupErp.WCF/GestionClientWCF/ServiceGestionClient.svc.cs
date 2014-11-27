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
        private static readonly Lazy<ClientBLL> LazyClientBLL = new Lazy<ClientBLL>(() => new ClientBLL());
        private static ClientBLL clientBLL { get { return LazyClientBLL.Value; } }
        
        [OperationContract]
        public bool CreateCompany(Company company)
        {
            return clientBLL.CreateCompany(company);
        }

        [OperationContract]
        public Company GetCompany(int idCompany)
        {
            return clientBLL.GetCompany(idCompany);
        }

        [OperationContract]
        public List<Company> GetListCompany()
        {
            return clientBLL.GetListCompany();
        }
    }
}
