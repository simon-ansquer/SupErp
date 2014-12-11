using SupErp.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SupErp.DAL.FacturationModele
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(BillQuotationLight))]
    [KnownType(typeof(BILL_LineBillQuotation))]
    public class BillQuotationComplete : BillQuotationLight
    {
        [DataMember]
        public List<LineCompleted> lines { get; private set; }

        [DataMember]
        public List<BILL_Status> statusPossible { get; set; }

        public BillQuotationComplete(BILL_BillQuotation bill_billQuotation)
            : base(bill_billQuotation)
        {
            var DALbill = new FacturationDAL.LineBillQuotationDAL();
            lines = DALbill.GetLineBillQuotation(bill_billQuotation.BillQuotation_Id).Select(l => new LineCompleted(l)).ToList();

            var DALstatusChain = new FacturationDAL.StatusChainDAL();
            statusPossible = DALstatusChain.GetStatusChain(bill_billQuotation.BILL_BillQuotationStatus.OrderByDescending(s => s.DateAdvancement).First().Status_Id);
        }
    }
}