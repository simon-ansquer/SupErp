using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationModele
{
    public class BillQuotationComplete : BillQuotationLight
    {
        public List<BILL_LineBillQuotation> lines { get; private set; }

        public BillQuotationComplete(BILL_BillQuotation bill_billQuotation): base(bill_billQuotation)
        {
            var DAL = new FacturationDAL.LineBillQuotationDAL();
            lines = DAL.GetLineBillQuotation(bill_billQuotation.BillQuotation_Id).ToList();
        }
    }
}
