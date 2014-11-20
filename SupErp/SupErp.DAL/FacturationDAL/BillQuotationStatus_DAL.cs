using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillQuotationStatus_DAL
    {
        #region Read

        public List<BILL_BillQuotationStatus> GetBillQuotationStatusByBillQuotation(BILL_BillQuotation billQuotation)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotationStatus.Where(bqs => bqs.BILL_BillQuotation == billQuotation).ToList();
            }
        }

        public List<BILL_BillQuotationStatus> GetBillQuotationStatusByStatus(BILL_Status status)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotationStatus.Where(bqs => bqs.BILL_Status == status).ToList();
            }
        }

        #endregion

        #region Create

        public BILL_BillQuotationStatus CreateBillQuotationStatus(BILL_BillQuotationStatus billQuotationStatusToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var s = context.BILL_BillQuotationStatus.Add(billQuotationStatusToAdd);
                context.SaveChanges();
                return s;
            }
        }

        #endregion

        #region Edit

        public BILL_BillQuotationStatus EditLineBillQuotation(BILL_BillQuotationStatus BillQuotationStatusToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var bqs = context.BILL_BillQuotationStatus.Find(BillQuotationStatusToEdit.BillQuotationStatus_Id);
                bqs = BillQuotationStatusToEdit;
                context.SaveChanges();
                return bqs;
            }
        }

        #endregion

        #region Delete

        public bool DeleteBillQuotationStatus(BILL_BillQuotationStatus BillQuotationStatusToDelete)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.BILL_BillQuotationStatus.Remove(BillQuotationStatusToDelete);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
