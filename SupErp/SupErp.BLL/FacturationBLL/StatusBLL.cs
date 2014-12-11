using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;

namespace SupErp.BLL.FacturationBLL
{
    public class StatusBLL
    {
        private static readonly Lazy<StatusDAL> LazyStatusDAL = new Lazy<StatusDAL>(() => new StatusDAL());
        private static StatusDAL DAL { get { return LazyStatusDAL.Value; } }

        #region Read

        public List<BILL_Status> GetStatus()
        {
            return DAL.GetStatus();
        }

        #endregion

    }
}
