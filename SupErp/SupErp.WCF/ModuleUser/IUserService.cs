using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Services;

namespace SupErp.WCF
{
    [ServiceContract]
    public interface IUserService
    {

        #region Authentication

        [OperationContract]
        User Login(string email, string password);

        #endregion

        #region Read

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        IEnumerable<User> GetUsers();

        [OperationContract]
        IEnumerable<Role> GetRoles();

        [OperationContract]
        IEnumerable<Module> GetModules();

        [OperationContract]
        Role GetRoleByUserId(int userId);

        #endregion

        #region Create

        [OperationContract]
        User CreateUser(User userToAdd);

        [OperationContract]
        Role CreateRole(Role roleToAdd);

        #endregion

        #region Edit

        [OperationContract]
        User EditUser(User userToEdit);

        [OperationContract]
        Role EditRole(Role roleToEdit);

        #endregion

        #region Delete

        [OperationContract]
        bool DeleteUser(int userId);

        [OperationContract]
        bool DeleteRole(int roleId);

        #endregion
    }
}