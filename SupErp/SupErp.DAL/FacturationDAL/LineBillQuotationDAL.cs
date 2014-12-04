using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class LineBillQuotationDAL
    {
          #region Read

        public IEnumerable<BILL_LineBillQuotation> GetLineBillQuotation(long billQuotation_id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_LineBillQuotation.Where(line => line.BillQuotation_Id == billQuotation_id).ToList();
            }
        }

        public IEnumerable<BILL_Product> productsIncludedInBill(long billquotation_id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_LineBillQuotation.Where(line => line.BillQuotation_Id == billquotation_id).Select(l => l.BILL_Product);
            }
        }

        #endregion

        #region Create

        public BILL_LineBillQuotation CreateLineBillQuotation(BILL_LineBillQuotation billLineToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var s = context.BILL_LineBillQuotation.Add(billLineToAdd);
                context.SaveChanges();
                return s;
            }
        }

        #endregion

        #region Edit

        public BILL_LineBillQuotation EditLineBillQuotation(BILL_LineBillQuotation LineBillQuotationToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var l = context.BILL_LineBillQuotation.Find(LineBillQuotationToEdit.LineBillQuotation_Id);
                l = LineBillQuotationToEdit;
                context.SaveChanges();
                return l;
            }
        }

        #endregion

        #region Delete

        public bool DeleteLineBillQuotation(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    var l = context.BILL_LineBillQuotation.Find(id);
                    context.BILL_LineBillQuotation.Remove(l);
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
