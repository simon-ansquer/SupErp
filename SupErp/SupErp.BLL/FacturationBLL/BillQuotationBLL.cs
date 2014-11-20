using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.FacturationBLL
{
    public class BillQuotationBLL
    {
        private static SupErp.DAL. userDAL { get; set; }

        #region Read

        public List<BillQuotationBLL> GetBillQuotation()
        {
            return user.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return userDAL.GetUsers();
        }

        public IEnumerable<Role> GetRoles()
        {
            return userDAL.GetRoles();
        }

        public Role GetRoleByUserId(int userId)
        {
            return userDAL.GetRoleByUserId(userId);
        }

        #endregion


    }
}
