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


        //[OperationContract]
        //List<BillQuotationLight> GetListQuotation();
        [TestMethod]
        public void TestGetQuotations()
        {
            Init();
            var lst = new List<BillQuotationLight>();
            lst = clientService.GetListQuotation();
            Assert.AreNotEqual(lst.Count, 0);
        }
        
        //[OperationContract]
        //List<BILL_Transmitter> GetTransmitter();

        [TestMethod]
        public void TestGetTransmitter()
        {
            Init();
            var lst = new List<BILL_Transmitter>();
            lst = clientService.GetTransmitter();
            Assert.AreNotEqual(lst.Count, 0);
        }

        //[OperationContract]
        //List<BillQuotationLight> SearchBillQuotation(string nomClient, string numFact, DateTime? dateDocument, BILL_Status status, int? MontantHTMin, int? MontantHTMax, bool? isBill);

        [TestMethod]
        public void TestSearchBillQuotation()
        {
            Init();
            var lst = new List<BillQuotationLight>();
            lst = clientService.SearchBillQuotation(null, null, null,null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchBillQuotation("Ingésup", null, null, null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchBillQuotation("Ingésup", "000000001", null, null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchBillQuotation(null, "000000001", null, null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchBillQuotation(null, null, new DateTime(2014, 12, 10), null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);

            lst = clientService.SearchBillQuotation(null, null, new DateTime(2014, 12, 10), null, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);
            
            lst = clientService.SearchBillQuotation(null,"000000001", new DateTime(2014, 12, 10), new BILL_Status { Status_Id = 19 }, null, null, null);
            Assert.AreNotEqual(lst.Count, 0);
        }

        //[OperationContract]
        //BillQuotationComplete GetBillQuotation(long billQuotation_id);
        [TestMethod]
        public void TestGetBillQuotation()
        {
            Init();
            var bc = clientService.GetBillQuotation(13);
            Assert.AreNotEqual(bc,null);
        }

        //[OperationContract]
        //List<LineExtended> GetAllLines(long billQuotation_id);
        [TestMethod]
        public void TestGetAllLines()
        {
            Init();
            var bc = clientService.GetAllLines(13);
            Assert.AreNotEqual(bc, null);
        }

        //[OperationContract]
        //bool CreateBillQuotation(BillQuotationComplete billQuotation);
        

        //[OperationContract]
        //bool ModifyBillQuotation(BillQuotationComplete billQuotation);

        //[OperationContract]
        //List<BILL_Status> GetStatus();
        public void TestGetStatus()
        {
            Init();
            var bc = clientService.GetStatus();
            Assert.AreNotEqual(bc, null);
        }
    }
}
