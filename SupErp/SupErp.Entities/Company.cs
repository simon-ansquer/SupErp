//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupErp.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        public Company()
        {
            this.BILL_BillQuotation = new HashSet<BILL_BillQuotation>();
            this.Company_Contact = new HashSet<Company_Contact>();
        }
    
        public long id { get; set; }
        public string name { get; set; }
        public string siret { get; set; }
        public string address { get; set; }
        public int postalcode { get; set; }
        public string city { get; set; }
    
        public virtual ICollection<BILL_BillQuotation> BILL_BillQuotation { get; set; }
        public virtual ICollection<Company_Contact> Company_Contact { get; set; }
    }
}