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

        #region Read

        public List<BILL_BillQuotationStatus> GetStatusByBillQuotation(BILL_BillQuotation billQuotation)
        {
            return DAL.GetBillQuotationStatusByBillQuotation(billQuotation);
        }

        public BILL_BillQuotationStatus GetCurrentStatus(BILL_BillQuotation billQuotation)
        {
            return DAL.GetCurrentStatusBillQuotation(billQuotation);
        }

        public List<BILL_BillQuotation> GetBillQuotationStatusByStatus(BILL_Status status)
        {
            return DAL.GetBillQuotationByStatus(status);
        }

        #endregion

        #region Create

        public BILL_BillQuotationStatus CreateBillQuotationStatus(BILL_BillQuotationStatus billQuotationStatusToAdd)
        {
            return DAL.CreateBillQuotationStatus(billQuotationStatusToAdd);
        }

        #endregion

        #region Edit

        public BILL_BillQuotationStatus EditLineBillQuotation(BILL_BillQuotationStatus billQuotationStatusToEdit)
        {
            return DAL.EditLineBillQuotation(billQuotationStatusToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteBillQuotationStatus(BILL_BillQuotationStatus BillQuotationStatusToDelete)
        {
            return DAL.DeleteBillQuotationStatus(BillQuotationStatusToDelete);
        }

        #endregion

    }
}
