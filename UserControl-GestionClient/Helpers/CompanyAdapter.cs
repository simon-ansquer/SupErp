using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControl_GestionClient.Models;
using UserControl_GestionClient.ClientServiceGestionClient;

namespace UserControl_GestionClient.Helpers
{
    public static class CompanyAdapter
    {

        public static Models.Company ToCompany(this ClientServiceGestionClient.Company comp)
        {
            return new Models.Company()
            {
                id = comp.id,
                name = comp.name,
                siret = comp.siret,
                address = comp.address,
                postalcode = comp.postalcode,
                city = comp.city
            };
        }

        public static IEnumerable<Models.Company> ToCompanies(this IEnumerable<ClientServiceGestionClient.Company> comps)
        {
            foreach (var c in comps)
            {
                yield return c.ToCompany();
            }
        }
    }
}
