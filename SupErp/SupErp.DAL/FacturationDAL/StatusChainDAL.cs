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
                return context.BILL_StatusChain.Where(s => s.StatusChain_Id == status_id).Select(s => s.BILL_Status1).ToList();
            }
        }


        #endregion

        
    }
}
