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

    }
}