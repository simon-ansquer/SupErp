using SupErp.BLL.GestionSalaireBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SupErp.WCF.GestionSalaireWCF
{
    
    public class ServiceSalaire : IServiceSalaire
    {

        private static readonly Lazy<SalaireBLL> LazySalaireBLL = new Lazy<SalaireBLL>(() => new SalaireBLL());
        private static SalaireBLL salaireBLL { get{ return LazySalaireBLL.Value; } }

        public List<Entities.User> GetUser()
        {
            return salaireBLL.getUsers();
        }

        public List<Entities.User> GetUser(string query)
        {
            return salaireBLL.getUsers(query);
        }

        public Entities.User GetUserById(long userID)
        {
            return salaireBLL.getUser(userID);
        }
    }
}
