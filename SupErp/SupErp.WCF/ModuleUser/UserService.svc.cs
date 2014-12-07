using SupErp.BLL.ModuleUser;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SupErp.WCF
{
    public class UserService : IUserService
    {
        //Conservation de l'instance au sein de la BLL, qui sera chargée à sa première utilisation
        private static readonly Lazy<UserBLL> lazyUserBLL = new Lazy<UserBLL>(() => new UserBLL());
        private static UserBLL userBLL { get { return lazyUserBLL.Value; } }

        public User Login(string email, string password)
        {
            return userBLL.Login(email, password);
        }

        public User GetUserById(int id)
        {
            return userBLL.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return userBLL.GetUsers();
        }

        public IEnumerable<Role> GetRoles()
        {
            return userBLL.GetRoles();
        }

        public Role GetRoleById(int roleId)
        {
            return userBLL.GetRoleById(roleId);
        }

        public IEnumerable<Module> GetModules()
        {
            return userBLL.GetModules();
        }

        public Role GetRoleByUserId(int userId)
        {
            return userBLL.GetRoleByUserId(userId);
        }

        public User CreateUser(User userToAdd)
        {
            return userBLL.CreateUser(userToAdd);
        }

        public Role CreateRole(Role roleToAdd)
        {
            return userBLL.CreateRole(roleToAdd);
        }

        public User EditUser(User userToEdit)
        {
            return userBLL.EditUser(userToEdit);
        }

        public Role EditRole(Role roleToEdit)
        {
            return userBLL.EditRole(roleToEdit);
        }

        public bool DeleteUser(int userId)
        {
            return userBLL.DeleteUser(userId);
        }

        public bool DeleteRole(int roleId)
        {
            return userBLL.DeleteRole(roleId);
        }
    }
}
