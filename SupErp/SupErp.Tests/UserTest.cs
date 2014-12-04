using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupErp.WCF.ModuleUser;
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
        public void TestGetRoleByUserId()
        {
            Assert.IsNotNull(userService.GetRoleByUserId(0));
        }

        [TestMethod]
        public void TestGetRoles()
        {
            Assert.IsTrue(userService.GetRoles().ToList().Count > 0);
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
        public void TestCreateRole()
        {
            Role newRole = new Role();
            newRole.Label = "Role de test";

            Assert.IsNotNull(userService.CreateRole(newRole));
        }

        [TestMethod]
        public void TestEditUser()
        {
            User editUser = userService.GetUserById(0);
            editUser.Firstname += " - Test de modification";

            Assert.IsNotNull(userService.EditUser(editUser));
        }

        [TestMethod]
        public void TestEditRole()
        {
            Role editRole = (userService.GetRoles().ToList())[0];
            editRole.Label += " - Test de modification";

            Assert.IsNotNull(userService.EditRole(editRole));
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Assert.IsTrue(userService.DeleteUser((int) userService.GetUserById(1).Id));
        }

        [TestMethod]
        public void TestDeleteRole()
        {
            Assert.IsTrue(userService.DeleteRole((int) (userService.GetRoles().ToList())[0].Id));
        }
    }
}
