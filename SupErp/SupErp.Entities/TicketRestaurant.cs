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
    
    public partial class TicketRestaurant
    {
    [DataMember]
        public long id { get; set; }
    [DataMember]
        public string Label { get; set; }
    [DataMember]
        public Nullable<decimal> Value { get; set; }
    [DataMember]
        public Nullable<decimal> PercentageEnterprise { get; set; }
    [DataMember]
        public Nullable<System.DateTime> Date { get; set; }
    }
}
