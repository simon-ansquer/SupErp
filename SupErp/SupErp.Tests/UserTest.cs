using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupErp.WCF;
using SupErp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SupErp.Tests
{
    [TestClass]
    public class UserTest
    {

        public UserService userService;

        [TestInitialize]
        public void TestInitialize()
        {
            userService = new UserService();
        }

        [TestMethod]
        public void TestLogin()
        {
            Assert.IsNotNull(userService.Login("", ""));
        }

        [TestMethod]
        public void TestLoginError()
        {
            Assert.IsNull(userService.Login(null, null));
        }

        [TestMethod]
        public void TestGetUsers()
        {
            Assert.IsTrue(userService.GetUsers().ToList().Count > 0);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            Assert.IsNotNull(userService.GetUserById(0));
        }

        [TestMethod]
        public void TestGetUserByIdError()
        {
            Assert.IsNull(userService.GetUserById(-1));
        }

        [TestMethod]
        public void TestGetRoleByUserId()
        {
            Assert.IsNotNull(userService.GetRoleByUserId(0));
        }

        [TestMethod]
        public void TestGetRoleByUserIdError()
        {
            Assert.IsNull(userService.GetRoleByUserId(-1));
        }

        [TestMethod]
        public void TestGetRoles()
        {
            Assert.IsTrue(userService.GetRoles().ToList().Count > 0);
        }

        [TestMethod]
        public void TestGetRoleById()
        {
            Assert.IsNotNull(userService.GetRoleById(0));
        }

        [TestMethod]
        public void TestGetRoleByIdError()
        {
            Assert.IsNull(userService.GetRoleById(-1));
        }

        [TestMethod]
        public void TestGetModules()
        {
            Assert.IsTrue(userService.GetModules().ToList().Count > 0);
        }

        [TestMethod]
        public void TestCreateUser()
        {
            User newUser = new User();
            newUser.Firstname = "Lucas";
            newUser.Lastname = "Libis";
            newUser.Passwordhash = "password";
            newUser.Zip_code = "33000";
            newUser.Address = "1 rue du Palace";
            newUser.City = "Bordeaux";
            newUser.Date_arrival = new DateTime(2010, 09, 01);
            newUser.Email = "libis.lucas@ingesup.com";

            Assert.IsNotNull(userService.CreateUser(newUser));
        }

        [TestMethod]
        public void TestCreateUserNull()
        {
            Assert.IsNull(userService.CreateUser(null));
        }

        [TestMethod]
        public void TestCreateRole()
        {
            Role newRole = new Role();
            newRole.Label = "Role de test";

            Assert.IsNotNull(userService.CreateRole(newRole));
        }

        [TestMethod]
        public void TestCreateRoleNull()
        {
            Assert.IsNull(userService.CreateRole(null));
        }

        [TestMethod]
        public void TestEditUser()
        {
            User editUser = userService.GetUserById(0);
            editUser.Firstname += " - Text de modification";

            Assert.IsNotNull(userService.EditUser(editUser));
        }

        [TestMethod]
        public void TestEditUserNull()
        {
            Assert.IsNull(userService.EditUser(null));
        }

        [TestMethod]
        public void TestEditRole()
        {
            Role editRole = (userService.GetRoles().ToList())[0];
            editRole.Label += " - Test de modification";

            Assert.IsNotNull(userService.EditRole(editRole));
        }

        [TestMethod]
        public void TestEditRoleNull()
        {
            Assert.IsNull(userService.EditRole(null));
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Assert.IsTrue(userService.DeleteUser((int) userService.GetUserById(1).Id));
        }

        [TestMethod]
        public void TestDeleteUserException()
        {
            Assert.IsFalse(userService.DeleteUser(-1));
        }

        [TestMethod]
        public void TestDeleteRole()
        {
            Assert.IsTrue(userService.DeleteRole((int) (userService.GetRoles().ToList())[0].Id));
        }

        [TestMethod]
        public void TestDeleteRoleException()
        {
            Assert.IsFalse(userService.DeleteRole(-1));
        }
    }
}
