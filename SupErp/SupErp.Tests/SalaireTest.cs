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
                long state = ((int)(me.Status_id == null ? 1 : me.Status_id) + 1) % stateCount + 1;

                Assert.IsTrue(clientService.UpdateUserState(me.Id, state));

                lst = clientService.SearchUser("del");
                me = lst[0];

                Assert.AreEqual(state, me.Status_id);
            }
        }


        [TestMethod]
        public void TestAddPrime()
        {
            List<User> lst = clientService.SearchUser("del");



            if (lst.Count >= 1)
            {
                User me = lst[0];

                Prime prime = new Prime()
                {
                    User_id = me.Id,
                    Price = 2000,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Label = "Yolo prime"
                };

                Assert.IsTrue(clientService.addPrime(me.Id,prime));

                lst = clientService.SearchUser("del");
                me = lst[0];

                int nbPrime = clientService.GetPrimesByUserId(me.Id).Count;

                Assert.AreEqual(me.Primes.Count,nbPrime);
            }
        }

        [TestMethod]
        public void TestAddAbsence()
        {
            List<User> lst = clientService.SearchUser("del");

           

            if (lst.Count >= 1)
            {
                User me = lst[0];
                int count = me.Absences.Count;
                Absence absence = new Absence()
                {
                    User_id = me.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };

                Assert.IsTrue(clientService.addAbsence(me.Id, absence));

                lst = clientService.SearchUser("del");
                me = lst[0];


                Assert.AreEqual(me.Absences.Count, count+1);
            }
        }

        [TestMethod]
        public void TestGetUsersById()
        {
            User user = clientService.GetUserById(1);
            Assert.IsNotNull(user);

        }

        [TestMethod]
        public void TestGetAbsenceType()
        {
            List<AbsenceType> lst = clientService.GetAbsenceTypes();
            Assert.IsNotNull(lst);
            Assert.IsTrue(lst.Count >= 1);
        }
    }
}
