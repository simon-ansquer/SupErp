using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillLineBillQuotationDAL
    {
          #region Read

        public IEnumerable<BILL_LineBillQuotation> GetLineBillQuotation(BILL_BillQuotation billQuotation)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_LineBillQuotation.Where(line => line.BILL_BillQuotation == billQuotation).ToList();
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

        public bool DeleteLineBillQuotation(BILL_LineBillQuotation lineBillQuotationToDelete)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.BILL_LineBillQuotation.Remove(lineBillQuotationToDelete);
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
