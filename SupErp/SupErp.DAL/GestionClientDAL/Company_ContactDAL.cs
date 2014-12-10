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
        public int CreateCompany_Contact(Company_Contact contact)
        {
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                sup.Company_Contact.Add(contact);
                sup.SaveChanges();
                
                    Company_Contact cont= sup.Company_Contact.OrderByDescending(p => p.id).First();
                    return (int)cont.id;
            }
        }

        public Company_Contact GetCompany_Contact(int idContact)
        {
            Company_Contact contact;
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                contact = sup.Company_Contact.Where(o => o.id == idContact).FirstOrDefault(); ;
            }
            return contact;
        }

        public List<Company_Contact> GetListCompanyContact()
        {
            List<Company_Contact> lcontact;
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                lcontact = sup.Company_Contact.ToList(); ;
            }
            return lcontact;
        }

        public List<Company_Contact> GetListCompanyContact(int idCompany)
        {
            List<Company_Contact> lcontact;
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                lcontact = sup.Company_Contact.Where(o => o.company_id == idCompany).ToList(); ;
            }
            return lcontact;
        }

        public bool EditCompany_Contact(Company_Contact contact)
        {
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                Company_Contact contactBdd = sup.Company_Contact.Where(p => p.id == contact.id).FirstOrDefault();
                if (contactBdd != null)
                {
                    sup.Entry(contactBdd).CurrentValues.SetValues(contact);
                    sup.SaveChanges();  
                }

            }
            return true;

        }

        public bool DeleteCompany_Contact(int id)
        {
            using (SUPERPEntities sup = new SUPERPEntities(false))
            {
                Company_Contact contact = sup.Company_Contact.Where(p => p.id == id).FirstOrDefault();
                sup.Entry(contact).State = System.Data.Entity.EntityState.Deleted;
                sup.SaveChanges();
            }
            return true;
        }
    }
}
