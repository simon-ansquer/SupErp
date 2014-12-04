using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class StatusDAL
    {
        #region Read

        public List<BILL_Status> GetStatus()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Status.ToList();
            }
        }

        public BILL_Status GetStatusById(int id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Status.SingleOrDefault(s => s.Status_Id == id);
            }
        }
        
        #endregion

        #region Create

        public BILL_Status CreateStatus(BILL_Status billStatusToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var s = context.BILL_Status.Add(billStatusToAdd);
                context.SaveChanges();
                return s;
            }
        }

        #endregion

        #region Edit

        public BILL_Status EditBillQuotation(BILL_Status billStatusToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var b = context.BILL_Status.Find(billStatusToEdit.Status_Id);
                b = billStatusToEdit;
                context.SaveChanges();
                return b;
            }
        }

        #endregion

        #region Delete

        public bool DeleteStatus(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    var l = context.BILL_Status.Find(id);
                    context.BILL_Status.Remove(l);
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
