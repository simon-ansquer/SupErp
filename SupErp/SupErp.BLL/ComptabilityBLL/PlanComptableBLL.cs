using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.BLL.ComptabilityBLL.BllObject;
using SupErp.DAL.GestionComptabilityDAL;


namespace SupErp.BLL.ComptabilityBLL
{
    public class PlanComptableBLL
    {
        private static readonly Lazy<ComptabilityDAL> lazyUserDAL = new Lazy<ComptabilityDAL>(() => new ComptabilityDAL());
        private static ComptabilityDAL userDAL { get { return lazyUserDAL.Value; } }


        public IEnumerable<ClassOfAccount> GetPlanComptable()
        {
            

            return null;
        }
    }
}
