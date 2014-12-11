using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupErp.DAL.FacturationDAL
{
    public class BillQuotationStatusDAL
    {
        #region Read

        public List<BILL_BillQuotationStatus> GetBillQuotationStatusByBillQuotation(long billQuotation_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_BillQuotationStatus.Where(bqs => bqs.BillQuotation_Id == billQuotation_id).ToList();
            }
        }

        public BILL_BillQuotationStatus GetCurrentStatusBillQuotation(long billQuotation_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var listStatus = GetBillQuotationStatusByBillQuotation(billQuotation_id).OrderByDescending(x => x.DateAdvancement);
                return listStatus.First();
            }
        }

        public List<BILL_BillQuotation> GetBillQuotationByStatus(long status_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_BillQuotationStatus.Where(bqs => bqs.Status_Id == status_id).Select(s => s.BILL_BillQuotation).ToList();
            }
        }

        #endregion Read

        #region Create

        public BILL_BillQuotationStatus CreateBillQuotationStatus(BILL_BillQuotationStatus billQuotationStatusToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var exist = context.BILL_BillQuotationStatus.SingleOrDefault(x => x.BillQuotation_Id == billQuotationStatusToAdd.BillQuotation_Id
                                                                    && x.BILL_Status.Status_Id == billQuotationStatusToAdd.BILL_Status.Status_Id);
                if (exist != null)
                    return exist;
                else
                {
                    var s = context.BILL_BillQuotationStatus.Add(billQuotationStatusToAdd);
                    context.SaveChanges();
                    return s;
                }
            }
        }

        #endregion Create

        #region Edit

        public BILL_BillQuotationStatus EditLineBillQuotation(BILL_BillQuotationStatus BillQuotationStatusToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var bqs = context.BILL_BillQuotationStatus.Find(BillQuotationStatusToEdit.BillQuotationStatus_Id);
                bqs = BillQuotationStatusToEdit;
                context.SaveChanges();
                return bqs;
            }
        }

        #endregion Edit

        #region Delete

        public bool DeleteBillQuotationStatus(long billQuotationStatus_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    var bqs = context.BILL_BillQuotationStatus.Find(billQuotationStatus_id);
                    context.BILL_BillQuotationStatus.Remove(bqs);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion Delete
    }
}