using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;

namespace SupErp.BLL.FacturationBLL
{
    public class BillStatusBLL
    {
        private static BillStatusDAL DAL { get; set; }

        #region Read

        public List<BILL_Status> GetStatus()
        {
            return DAL.GetStatus();
        }

        public BILL_Status GetStatusById(int id)
        {
            return DAL.GetStatusById(id);
        }

        #endregion

        #region Create

        public BILL_Status CreateStatus(BILL_Status billStatusToAdd)
        {
            return DAL.CreateStatus(billStatusToAdd);
        }

        #endregion

        #region Edit

        public BILL_Status EditBillQuotation(BILL_Status billStatusToEdit)
        {
            return DAL.EditBillQuotation(billStatusToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteStatus(BILL_Status billStatusToDelete)
        {
            return DAL.DeleteStatus(billStatusToDelete);
        }

        #endregion
    }
}
