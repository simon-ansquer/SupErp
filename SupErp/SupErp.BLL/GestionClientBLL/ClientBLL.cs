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

        public Company GetCompany(int idCompany)
        {
            return companyDAL.GetCompany(idCompany);
        }

        public int CreateCompany(Company company)
        {
            return companyDAL.CreateCompany(company);
        }
        public List<Company> GetListCompany ()
        {
            return companyDAL.GetListCompany();
        }

        public bool EditCompany(Company company)
        {
            return companyDAL.EditCompany(company);
        }

        public bool DeleteCompany(int id)
        {
            return companyDAL.DeleteCompany(id);
        }
    }
}
