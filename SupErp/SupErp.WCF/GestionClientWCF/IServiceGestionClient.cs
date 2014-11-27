using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SupErp.Entities;

namespace SupErp.WCF.GestionClientWCF
{
    [ServiceContract]
    interface IServiceGestionClient
    {
        [OperationContract]
        bool CreateCompany(Company company);

        [OperationContract]
        List<Company> GetCompany(int idCustomer);
    }
}
