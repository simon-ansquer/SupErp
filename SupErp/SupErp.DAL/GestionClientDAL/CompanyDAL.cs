using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.DAL.GestionClientDAL
{
    public class CompanyDAL
    {
        public bool CreateCompany(Company compa)
        {
            using (SUPERPEntities sup = new SUPERPEntities())
            {
                sup.Companies.Add(compa);
                sup.SaveChanges();
            }
            return true;
        }
    }
}
