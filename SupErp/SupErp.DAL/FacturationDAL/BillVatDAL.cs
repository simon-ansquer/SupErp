using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillVatDAL
    {
        #region Read
        public List<BILL_Vat> GetBillVat()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Vat.ToList();
            }
        }

        public BILL_Vat GetBillVatByID(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Vat.SingleOrDefault(v => v.Vat_Id == id);
            }
        }

        public BILL_Vat GetBillVat(Double rateBillVat)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Vat.SingleOrDefault(v => v.Rate == rateBillVat);
            }
        }

        public BILL_Vat GetBillVat(DateTime dateBillVat)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Vat.SingleOrDefault(c => c.DateVat == dateBillVat);
            }
        }
        #endregion

        #region Create
        public BILL_Vat CreateBillVat(BILL_Vat billVatToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var v = context.BILL_Vat.Add(billVatToAdd);
                context.SaveChanges();
                return v;
            }
        }
        #endregion

        #region Edit
        public BILL_Vat EditBillVat(BILL_Vat billVatToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var v = context.BILL_Vat.Find(billVatToEdit.Vat_Id);
                v = billVatToEdit;
                context.SaveChanges();
                return v;
            }
        }
        #endregion

        #region Delete
        public bool DeleteBillVat(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    var v = context.BILL_Vat.Find(id);
                    context.BILL_Vat.Remove(v);
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
