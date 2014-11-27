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
    }
}
