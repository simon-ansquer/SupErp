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
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }

        public List<BILL_BillQuotation> GetBills()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result= context.BILL_BillQuotation.Where(b => b.NBill != null).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }

        public List<BILL_BillQuotation> GetQuotations()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Where(b => b.NBill == null).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }

        public BILL_BillQuotation GetBillQuotationsById(long id)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result=  context.BILL_BillQuotation.SingleOrDefault(b => b.BillQuotation_Id == id);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
            
        }

        public BILL_BillQuotation GetBillByNum(string numBill)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.SingleOrDefault(b => b.NBill == numBill);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
            
        }

        public BILL_BillQuotation GetBillQuotation(DateTime dateBillQuotation)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.SingleOrDefault(b => b.DateBillQuotation == dateBillQuotation);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }

        #endregion

        #region Create

        public BILL_BillQuotation CreateBillQutotation(BILL_BillQuotation billQuotationToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
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
            using (SUPERPEntities context = new SUPERPEntities(false))
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
            using (SUPERPEntities context = new SUPERPEntities(false))
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
