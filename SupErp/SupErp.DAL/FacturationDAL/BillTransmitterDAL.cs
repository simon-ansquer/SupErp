using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillTransmitterDAL
    {
        #region Read
        public List<BILL_Transmitter> GetBillTrans()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Transmitter.ToList();
            }
        }
        #endregion

        #region Create
        public BILL_Transmitter CreateBillTrans(BILL_Transmitter billVTransToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var t = context.BILL_Transmitter.Add(billVTransToAdd);
                context.SaveChanges();
                return t;
            }
        }
        #endregion

        #region Edit
        public BILL_Transmitter EditBillTrans(BILL_Transmitter billTransToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var t = context.BILL_Transmitter.Find(billTransToEdit.Transmitter_Id);
                t = billTransToEdit;
                context.SaveChanges();
                return t;
            }
        }
        #endregion

        #region Delete
        public bool DeleteBillTrans(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    var t = context.BILL_Transmitter.Find(id);
                    context.BILL_Transmitter.Remove(t);
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
