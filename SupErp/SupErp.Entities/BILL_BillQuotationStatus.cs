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
    [KnownType(typeof(BILL_BillQuotation))]
    [KnownType(typeof(BILL_Status))]
    
    public partial class BILL_BillQuotationStatus
    {
    [DataMember]
        public long BillQuotationStatus_Id { get; set; }
    [DataMember]
        public System.DateTime DateAdvancement { get; set; }
    [DataMember]
        public long Status_Id { get; set; }
    [DataMember]
        public long BillQuotation_Id { get; set; }
    
    [DataMember]
        public virtual BILL_BillQuotation BILL_BillQuotation { get; set; }
    [DataMember]
        public virtual BILL_Status BILL_Status { get; set; }
    }
}
