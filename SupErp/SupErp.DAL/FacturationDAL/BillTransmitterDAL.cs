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

    }
}
