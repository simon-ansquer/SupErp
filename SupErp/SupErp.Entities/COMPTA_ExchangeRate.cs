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
    
    public partial class COMPTA_ExchangeRate
    {
    [DataMember]
        public long id { get; set; }
    [DataMember]
        public Nullable<System.DateTime> updatedDate { get; set; }
    [DataMember]
        public Nullable<double> EURO_USD { get; set; }
    [DataMember]
        public Nullable<double> EURO_GBP { get; set; }
    [DataMember]
        public Nullable<double> EURO_AUD { get; set; }
    [DataMember]
        public Nullable<double> EURO_ZAR { get; set; }
    [DataMember]
        public Nullable<double> USD_EURO { get; set; }
    }
}