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
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    
    [DataContract(IsReference = true)]
    [KnownType(typeof(BILL_BillQuotationStatus))]
    [KnownType(typeof(BILL_StatusChain))]
    
    public partial class BILL_Status
    {
        public BILL_Status()
        {
            this.BILL_BillQuotationStatus = new HashSet<BILL_BillQuotationStatus>();
            this.BILL_StatusChain = new HashSet<BILL_StatusChain>();
            this.BILL_StatusChain1 = new HashSet<BILL_StatusChain>();
        }
    
    [DataMember]
        public long Status_Id { get; set; }
    [DataMember]
        public string Libel { get; set; }
    
    [DataMember]
        public virtual ICollection<BILL_BillQuotationStatus> BILL_BillQuotationStatus { get; set; }
    [DataMember]
        public virtual ICollection<BILL_StatusChain> BILL_StatusChain { get; set; }
    [DataMember]
        public virtual ICollection<BILL_StatusChain> BILL_StatusChain1 { get; set; }
    }
}
