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
        public int CreateCompany(Company compa)
        {
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                sup.Companies.Add(compa);
                sup.SaveChanges();

                Company cont = sup.Companies.OrderByDescending(p => p.id).First();
                return (int)cont.id;
            }
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

        public bool EditCompany(Company company)
        {
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                Company contactBdd = sup.Companies.Where(p => p.id == company.id).FirstOrDefault();
                if (contactBdd != null)
                {
                    sup.Entry(contactBdd).CurrentValues.SetValues(company);
                    sup.SaveChanges();
                }

            }
            return true;
        }

        public bool DeleteCompany(int id)
        {
           using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                Company contact = sup.Companies.Where(p => p.id == id).FirstOrDefault();
                sup.Entry(contact).State = System.Data.Entity.EntityState.Deleted;
                sup.SaveChanges();
            }
            return true;
        }
    }


}
