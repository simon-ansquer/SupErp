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
        private static readonly Lazy<BillTransmitterDAL> LazyBillTransDAL = new Lazy<BillTransmitterDAL>(() => new BillTransmitterDAL());
        private static BillTransmitterDAL billTransDAL { get { return LazyBillTransDAL.Value; } }

        #region Read
        public List<BILL_Transmitter> GetBillTrans()
        {
            return billTransDAL.GetBillTrans();
        }
        #endregion

    }
}
