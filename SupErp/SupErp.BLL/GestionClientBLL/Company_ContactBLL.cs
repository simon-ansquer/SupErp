using SupErp.DAL.GestionClientDAL;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.GestionClientBLL
{
    public class Company_ContactBLL
    {
        private static readonly Lazy<Company_ContactDAL> LazyContactDAL = new Lazy<Company_ContactDAL>(() => new Company_ContactDAL());
        private static Company_ContactDAL contactDAL { get { return LazyContactDAL.Value; } }

        public Company_Contact GetCompany_Contact(int idContact)
        {
            return contactDAL.GetCompany_Contact(idContact);
        }

        public int CreateCompany_Contact(Company_Contact contact)
        {
            return contactDAL.CreateCompany_Contact(contact);
        }

        public List<Company_Contact> GetListCompany_Contact()
        {
            return contactDAL.GetListCompanyContact();
        }

        public List<Company_Contact> GetListCompany_Contact(int idCompany)
        {
            return contactDAL.GetListCompanyContact(idCompany);
        }

        public bool EditCompany_Contact(Company_Contact contact)
        {
            return contactDAL.EditCompany_Contact(contact);
        }

        public bool DeleteCompany_Contact(int id)
        {
            return contactDAL.DeleteCompany_Contact(id);
        }
    }
}
