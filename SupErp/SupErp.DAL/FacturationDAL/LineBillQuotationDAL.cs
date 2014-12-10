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
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_LineBillQuotation.Where(line => line.BillQuotation_Id == billQuotation_id).ToList();
            }
        }

        public IEnumerable<BILL_Product> productsIncludedInBill(long billquotation_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_LineBillQuotation.Where(line => line.BillQuotation_Id == billquotation_id).Select(l => l.BILL_Product);
            }
        }

        #endregion

        #region Create

        public List<BILL_LineBillQuotation> CreateLineBillQuotation(List<BILL_LineBillQuotation> billLineToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var listLine = new List<BILL_LineBillQuotation>();
                foreach(var line in billLineToAdd)
                {
                    var s = context.BILL_LineBillQuotation.Add(line);
                    listLine.Add(line);
                }
                context.SaveChanges();
                return listLine;
            }
        }

        #endregion

        #region Edit

        public List<BILL_LineBillQuotation> EditLineBillQuotation(List<BILL_LineBillQuotation> LineBillQuotationToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var listLine = new List<BILL_LineBillQuotation>();
                foreach (var line in LineBillQuotationToEdit)
                {
                    var l = context.BILL_LineBillQuotation.Find(line.LineBillQuotation_Id);
                    l = line;
                    listLine.Add(l);
                }
                context.SaveChanges();
                return listLine;
            }
        }

        #endregion

        #region Delete

        public bool DeleteLineBillQuotation(List<long> listID)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    foreach(var id in listID)
                    {
                        var l = context.BILL_LineBillQuotation.Find(id);
                        context.BILL_LineBillQuotation.Remove(l);
                    }
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
