using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.DAL.FacturationDAL;

namespace SupErp.BLL.FacturationBLL
{
    public class BillTransmitterBLL
    {
        private static BillTransmitterDAL billTransDAL { get; set; }

        #region Read
        public List<BILL_Transmitter> GetBillTrans()
        {
            return billTransDAL.GetBillTrans();
        }
        #endregion

        #region Create
        public BILL_Transmitter CreateBillTrans(BILL_Transmitter billTransToAdd)
        {
            return billTransDAL.CreateBillTrans(billTransToAdd);
        }
        #endregion

        #region Edit
        public BILL_Transmitter EditBillTrans(BILL_Transmitter billTransToEdit)
        {
            return billTransDAL.EditBillTrans(billTransToEdit);
        }
        #endregion

        #region Delete
        public bool DeleteBillTrans(BILL_Transmitter billTransToDelete)
        {
            return billTransDAL.DeleteBillTrans(billTransToDelete);
        }
        #endregion
    }
}
