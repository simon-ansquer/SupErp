using SupErp.DAL.GestionClientDAL;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.GestionClientBLL
{
    public class ClientBLL
    {
        private static readonly Lazy<CompanyDAL> LazyCompanyDAL = new Lazy<CompanyDAL>(() => new CompanyDAL());
        private static CompanyDAL companyDAL { get { return LazyCompanyDAL.Value; } }

        public List<Company> GetCompany(int idCustomer)
        {
            return null;
        }

        public bool CreateCompany(Company company)
        {
            return companyDAL.CreateCompany(company);
        }
    }
}
