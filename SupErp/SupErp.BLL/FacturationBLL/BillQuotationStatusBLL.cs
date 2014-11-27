using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;

namespace SupErp.BLL.FacturationBLL
{
    public class BillQuotationStatusBLL
    {
        
        private static BillQuotationStatusDAL billQuotationStatusDAL { get; set; }


        #region Read

        public BILL_BillQuotationStatus GetCurrentStatus(BILL_BillQuotation billQuotation)
        {
            return billQuotationStatusDAL.GetCurrentStatusBillQuotation(billQuotation);
        }
            
        #endregion
         
    }
}
