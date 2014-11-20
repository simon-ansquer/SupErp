using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SupErp.WCF.GestionSalaireWCF
{
    
    [ServiceContract]
    public interface IServiceSalaire
    {
        [OperationContract]
        List<User> GetUser();

        [OperationContract]
        List<User> GetUser(string query);

    }
}
