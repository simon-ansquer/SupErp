using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupErp.WCF;
using SupErp.Entities;
using SupErp.WCF.FacturationWCF;
using SupErp.DAL.FacturationModele;

namespace SupErp.Tests
{
    [TestClass]
    public class FacturationTest
    {
        private FacturationService clientService;


        [TestInitialize]
        public void Init()
        {
            clientService = new FacturationService();
        }

        [TestMethod]
        public void TestGetQuotations()
        {
            Init();
            var lst = new List<BillQuotationLight>();
            lst = clientService.GetListQuotation();
            Assert.AreNotEqual(lst.Count, 0);
        }
        
    }
}
