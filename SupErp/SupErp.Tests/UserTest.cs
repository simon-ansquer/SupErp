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

        private User CreateTestUser()
        {
            User newUser = new User();
            newUser.Firstname = "Eliott";
            newUser.Lastname = "Lujan";
            newUser.Passwordhash = "password";
            newUser.Zip_code = "33000";
            newUser.Address = "179 rue Camille Godard";
            newUser.City = "Bordeaux";
            newUser.Date_arrival = new DateTime(2010, 09, 01);
            newUser.Email = "eliott.lujan@gmail.com";

            return userService.CreateUser(newUser);
        }

        private Role CreateTestRole()
        {
            Role newRole = new Role();
            newRole.Label = "Role de test";

            RoleModule rm = userService.GetModules().First().RoleModules.First();
            newRole.RoleModules.Add(new RoleModule() {
                  Module = rm.Module,
                  Module_id = rm.Module_id,
                  Role = rm.Role,
                  Role_id = rm.Role_id
                });

            return userService.CreateRole(newRole);
        }

        [TestMethod]
        public void TestLogin()
        {
            Assert.IsNotNull(userService.Login("bricejantieu@gmail.com", "azerty"));
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
            Assert.IsNotNull(userService.GetRoleByUserId(2));
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
            Assert.IsNotNull(userService.GetRoleById(1));
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
            Assert.IsNotNull(userService.CreateUser(CreateTestUser()));
        }

        [TestMethod]
        public void TestCreateUserNull()
        {
            Assert.IsNull(userService.CreateUser(null));
        }

        [TestMethod]
        public void TestCreateRole()
        {
            Assert.IsNotNull(userService.CreateRole(CreateTestRole()));
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
            Role editRole = (userService.GetRoles().ToList()).Last();
            editRole.Label += " - Test de modification";

            List<Module> modules = userService.GetModules().ToList();
            RoleModule roleModule = new RoleModule();
            roleModule.Role_id = editRole.Id;
            roleModule.Module_id = modules[1].Id;

            editRole.RoleModules.Add(roleModule);

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
            CreateTestUser();
            User user = userService.GetUsers().ToList().Last();
            Assert.IsTrue(userService.DeleteUser((int) user.Id));
        }

        [TestMethod]
        public void TestDeleteUserException()
        {
            Assert.IsFalse(userService.DeleteUser(-1));
        }

        [TestMethod]
        public void TestDeleteRole()
        {
            CreateTestRole();
            Role role = userService.GetRoles().ToList().Last();
            Assert.IsTrue(userService.DeleteRole((int) role.Id));
        }

        [TestMethod]
        public void TestDeleteRoleException()
        {
            Assert.IsFalse(userService.DeleteRole(-1));
        }
    }
}
