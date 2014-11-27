using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillQuotationDAL
    {
        #region Read

        public List<BILL_BillQuotation> GetBillQuotation()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.ToList();
            }
        }

        public List<BILL_BillQuotation> GetBills()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.Where(b => b.NBill != null).ToList();
            }
        }

        public List<BILL_BillQuotation> GetQuotations()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.Where(b => b.NBill == null).ToList();
            }
        }

        public BILL_BillQuotation GetBillQuotationsById(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.SingleOrDefault(b => b.BillQuotation_Id == id);
            }
        }

        public BILL_BillQuotation GetBillByNum(string numBill)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.SingleOrDefault(b => b.NBill == numBill);
            }
        }

        public BILL_BillQuotation GetBillQuotation(DateTime dateBillQuotation)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_BillQuotation.SingleOrDefault(b => b.DateBillQuotation == dateBillQuotation);
            }
        }

        #endregion

        #region Create

        public BILL_BillQuotation CreateBillQutotation(BILL_BillQuotation billQuotationToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var b = context.BILL_BillQuotation.Add(billQuotationToAdd);
                context.SaveChanges();
                return b;
            }
        }

        #endregion

        #region Edit

        public BILL_BillQuotation EditBillQuotation(BILL_BillQuotation billQuotationToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var b = context.BILL_BillQuotation.Find(billQuotationToEdit.BillQuotation_Id);
                b = billQuotationToEdit;
                context.SaveChanges();
                return b;
            }
        }

        #endregion

        #region Delete

        public bool DeleteBillQuotation(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    var b = context.BILL_BillQuotation.Find(id);
                    context.BILL_BillQuotation.Remove(b);
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
