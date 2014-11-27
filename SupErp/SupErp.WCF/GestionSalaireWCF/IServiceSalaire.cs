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
        List<User> SearchUser(string query);

        [OperationContract]
        User GetUserById(long userID);

        [OperationContract]
        bool UpdateUserSalaryById(long idUser, decimal newSalaryNet);

        [OperationContract]
        List<Status> GetState();

        [OperationContract]
        bool UpdateUserState(long idUser, long idState);
    }
}
