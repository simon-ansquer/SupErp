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

        public List<BILL_BillQuotationStatus> GetStatusByBillQuotation(long billQuotation_id)
        {
            return DAL.GetBillQuotationStatusByBillQuotation(billQuotation_id);
        }

        public BILL_BillQuotationStatus GetCurrentStatus(long billQuotation_id)
        {
            return DAL.GetCurrentStatusBillQuotation(billQuotation_id);
        }

        public List<BILL_BillQuotation> GetBillQuotationStatusByStatus(long status_id)
        {
            return DAL.GetBillQuotationByStatus(status_id);
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

        public bool DeleteBillQuotationStatus(long id)
        {
            return DAL.DeleteBillQuotationStatus(id);
        }

        #endregion

    }
}
