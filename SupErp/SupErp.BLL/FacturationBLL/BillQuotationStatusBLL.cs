using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;

namespace SupErp.BLL.FacturationBLL
{
    public class BillQuotationStatusBLL
    {
        private static readonly Lazy<BillQuotationStatusDAL> LazyStatusDAL = new Lazy<BillQuotationStatusDAL>(() => new BillQuotationStatusDAL());
        private static BillQuotationStatusDAL DAL { get { return LazyStatusDAL.Value; } }


        #region Create
        public BILL_BillQuotationStatus CreateBillQuotationStatus(BILL_BillQuotationStatus billQuotationStatusToAdd)
        {
            return DAL.CreateBillQuotationStatus(billQuotationStatusToAdd);
        }

        #endregion


    }
}
