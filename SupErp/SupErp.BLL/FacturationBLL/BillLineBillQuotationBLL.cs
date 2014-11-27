using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;

namespace SupErp.BLL.FacturationBLL
{
    public class BillLineBillQuotationBLL
    {
        private static BillLineBillQuotationDAL DAL { get; set; }


        #region Read

        public IEnumerable<BILL_LineBillQuotation> GetLineBillQuotation(BILL_BillQuotation billQuotation)
        {
            return DAL.GetLineBillQuotation(billQuotation);
        }

        #endregion

        #region Create

        public BILL_LineBillQuotation CreateLineBillQuotation(BILL_LineBillQuotation billLineToAdd)
        {
            return DAL.CreateLineBillQuotation(billLineToAdd);
        }

        #endregion

        #region Edit

        public BILL_LineBillQuotation EditLineBillQuotation(BILL_LineBillQuotation LineBillQuotationToEdit)
        {
           return DAL.EditLineBillQuotation(LineBillQuotationToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteLineBillQuotation(BILL_LineBillQuotation lineBillQuotationToDelete)
        {
            return DAL.DeleteLineBillQuotation(lineBillQuotationToDelete);
        }

        #endregion
    }
}
