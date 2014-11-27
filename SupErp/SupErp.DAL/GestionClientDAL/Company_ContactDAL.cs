using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.DAL.GestionClientDAL
{
    public class Company_ContactDAL
    {
        public bool CreateCompany_Contact(Company_Contact contact)
        {
            using (SUPERPEntities sup = new SUPERPEntities())
            {
                sup.Company_Contact.Add(contact);
                sup.SaveChanges();
            }
            return true;
        }

        public Company_Contact GetCompany_Contact(int idContact)
        {
            Company_Contact contact;
            using (SUPERPEntities sup = new SUPERPEntities())
            {
                contact = sup.Company_Contact.Where(o => o.id == idContact).FirstOrDefault(); ;
            }
            return contact;
        }

        public List<Company_Contact> GetListCompanyContact()
        {
            List<Company_Contact> lcontact;
            using (SUPERPEntities sup = new SUPERPEntities())
            {
                lcontact = sup.Company_Contact.ToList(); ;
            }
            return lcontact;
        }

        public List<Company_Contact> GetListCompanyContact(int idCompany)
        {
            List<Company_Contact> lcontact;
            using (SUPERPEntities sup = new SUPERPEntities())
            {
                lcontact = sup.Company_Contact.Where(o => o.company_id == idCompany).ToList(); ;
            }
            return lcontact;
        }
    }
}
