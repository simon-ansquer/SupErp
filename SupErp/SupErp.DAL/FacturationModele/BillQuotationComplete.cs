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
        private List<BILL_LineBillQuotation> lines { get; set; }

        public BillQuotationComplete(BILL_BillQuotation bill_billQuotation): base(bill_billQuotation)
        {
            var DAL = new FacturationDAL.LineBillQuotationDAL();
            lines = DAL.GetLineBillQuotation(bill_billQuotation.BillQuotation_Id).ToList();
        }
    }
}
