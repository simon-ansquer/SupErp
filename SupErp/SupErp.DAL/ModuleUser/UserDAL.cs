using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.DAL.ModuleUser
{
    public class UserDAL
    {
        public User GetUserById(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities())
            {
                return context.Users.Find(id);
            }
        }
    }
}
