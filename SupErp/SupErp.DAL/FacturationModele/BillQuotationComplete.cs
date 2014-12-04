using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using System.Runtime.Serialization;

namespace SupErp.DAL.FacturationModele
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(BillQuotationLight))]
    [KnownType(typeof(BILL_LineBillQuotation))]
    public class BillQuotationComplete : BillQuotationLight
    {
        [DataMember]
        public List<BILL_LineBillQuotation> lines { get; private set; }
        public List<BILL_Status> statusPossible { get; set; }
        public BillQuotationComplete(BILL_BillQuotation bill_billQuotation): base(bill_billQuotation)
        {
            var DALbill = new FacturationDAL.LineBillQuotationDAL();
            lines = DALbill.GetLineBillQuotation(bill_billQuotation.BillQuotation_Id).ToList();

            var DALstatusChain = new FacturationDAL.StatusChainDAL();
            statusPossible = DALstatusChain.GetStatusChain(bill_billQuotation.BILL_BillQuotationStatus.OrderByDescending(s => s.DateAdvancement).First().Status_Id);
        }
    }
}
