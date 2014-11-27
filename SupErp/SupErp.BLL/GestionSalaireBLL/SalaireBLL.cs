using SupErp.DAL.GestionSalaireDAL;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.GestionSalaireBLL
{
    public class SalaireBLL
    {

        private static readonly Lazy<GestionSalaireDAL> LazySalaireDAL = new Lazy<GestionSalaireDAL>(() => new GestionSalaireDAL());
        private static GestionSalaireDAL salaireDAL { get{ return LazySalaireDAL.Value; } }


        // récupération des salariés
        // recherche des salariés

        public List<User> getUsers(){
            return salaireDAL.GetUsers(null);
        }

        public List<User> getUsers(string query){
            return salaireDAL.GetUsers(query);
        }

        public User getUser(long userID)
        {
            return salaireDAL.GetUserById(userID);
        }









    }
}
