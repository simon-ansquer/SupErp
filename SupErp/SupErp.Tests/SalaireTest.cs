using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupErp.WCF.GestionSalaireWCF;
using System.Collections.Generic;
using SupErp.Entities;

namespace SupErp.Tests
{
    [TestClass]
    public class SalaireTest
    {

        private ServiceSalaire clientService;


        [TestInitialize]
        public void Init()
        {
            clientService = new ServiceSalaire();
        }

        [TestMethod]
        public void TestGetUsers()
        {
            List<User> lst = new List<User>();
            lst = clientService.GetUser();
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchUser("del");
            Assert.AreEqual(lst.Count, 1);

        }

        [TestMethod]
        public void TestUpdateUserSalary()
        {
            List<User> lst = clientService.SearchUser("del");

            if (lst.Count >= 1) {
                User me = lst[0];

                Assert.IsTrue(clientService.UpdateUserSalaryById(me.Id, 1000));

                lst = clientService.SearchUser("del");
                me = lst[0];

                Assert.AreEqual(1000, me.GetCurrentSalary().NetSalary);
            }
        }

        [TestMethod]
        public void TestGetState()
        {
            List<Status> lst = clientService.GetState();

            Assert.AreEqual(4, lst.Count);
        }

        [TestMethod]
        public void TestUpdateUserState()
        {
            List<User> lst = clientService.SearchUser("del");



            if (lst.Count >= 1)
            {
                User me = lst[0];

                int stateCount = clientService.GetState().Count;
                long state = ((int)(me.Status_id == null ? 0 : me.Status_id) + 1) % stateCount;

                Assert.IsTrue(clientService.UpdateUserState(me.Id, state));

                lst = clientService.SearchUser("del");
                me = lst[0];

                Assert.AreEqual(state, me.Status_id);
            }
        }
    }
}
