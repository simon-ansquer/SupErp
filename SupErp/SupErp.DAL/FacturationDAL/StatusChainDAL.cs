using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class StatusChainDAL
    {
        #region Read

        public List<BILL_Status> GetStatusChain(long status_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var res = context.BILL_StatusChain.Include("BILL_Status").Include("BILL_Status1").Where(s => s.Status_Id == status_id).Select(s => s.BILL_Status1).ToList();
                return res;
            }
        }


        #endregion

        
    }
}
