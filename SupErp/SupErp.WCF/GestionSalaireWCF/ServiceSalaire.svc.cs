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

        public List<Entities.User> SearchUser(string query)
        {
            return salaireBLL.getUsers(query);
        }

        public Entities.User GetUserById(long userID)
        {
            return salaireBLL.getUser(userID);
        }


        public bool UpdateUserSalaryById(long idUser, decimal newSalaryNet)
        {
            return salaireBLL.updateUserSalary(idUser, newSalaryNet);
        }


        public List<Entities.Status> GetState()
        {
            return salaireBLL.GetState();
        }


        public bool UpdateUserState(long idUser, long idState)
        {
            return salaireBLL.UpdateUserState(idUser, idState);
        }


        public bool addPrime(long idUser, Entities.Prime prime)
        {
            return salaireBLL.addPrime(idUser, prime);
        }


        public List<Entities.Prime> GetPrimesByUserId(long id)
        {
            return salaireBLL.GetPrimesByUserId(id);
        }


        public bool addAbsence(long idUser, Entities.Absence absence)
        {
            return salaireBLL.addAbsence(idUser, absence);
        }
    }
}
