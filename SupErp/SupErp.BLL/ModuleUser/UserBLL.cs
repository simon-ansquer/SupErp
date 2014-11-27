using SupErp.DAL.ModuleUser;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.ModuleUser
{
    public class UserBLL
    {
        //Conservation de l'instance au sein de la BLL, qui sera chargée à sa première utilisation
        private static readonly Lazy<UserDAL> lazyUserDAL = new Lazy<UserDAL>(() => new UserDAL());
        private static UserDAL userDAL { get { return lazyUserDAL.Value; } }

        #region Authentication

        public User Login(string email, string password)
        {
            return userDAL.Login(email, password);
        }

        #endregion

        #region Read

        public User GetUserById(int id)
        {
            return userDAL.GetUserById(id);
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

        #region Create

        public User CreateUser(User userToAdd)
        {
            return userDAL.CreateUser(userToAdd);
        }

        public Role CreateRole(Role roleToAdd)
        {
            return userDAL.CreateRole(roleToAdd);
        }

        #endregion

        #region Edit

        public User EditUser(User userToEdit)
        {
            return userDAL.EditUser(userToEdit);
        }

        public Role EditRole(Role roleToEdit)
        {
            return userDAL.EditRole(roleToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteUser(int userId)
        {
            return userDAL.DeleteUser(userId);
        }

        public bool DeleteRole(int roleId)
        {
            return userDAL.DeleteRole(roleId);
        }

        #endregion
    }
}
