//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfServiceLibrarySalary.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BILL_Status
    {
        public BILL_Status()
        {
            this.BILL_BillQuotationStatus = new HashSet<BILL_BillQuotationStatus>();
        }
    
        public long Status_Id { get; set; }
        public string Libel { get; set; }
    
        public virtual ICollection<BILL_BillQuotationStatus> BILL_BillQuotationStatus { get; set; }
    }
}