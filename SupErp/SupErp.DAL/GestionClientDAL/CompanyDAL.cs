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
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                sup.Companies.Add(compa);
                sup.SaveChanges();
            }
            return true;
        }

        public Company GetCompany(int idCompany)
        {
            Company com = new Company();
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                com = sup.Companies.Where(o => o.id == idCompany).FirstOrDefault();
            }
            return com;
        }

        public List<Company> GetListCompany ()
        {
         List<Company> com1 = new List<Company>();
         using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                com1 = sup.Companies.ToList();
            }
            return com1;
        }


        //public Company EditCompany_Contact(Company compa, string idCompany)
        //{
        //    using (SUPERPEntities sup = new SUPERPEntities (false))
        //    {

        //    }

        //}
    }
}
