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
    [KnownType(typeof(COMPTA_ChartOfAccounts))]
    
    public partial class COMPTA_ClassOfAccounts
    {
        public COMPTA_ClassOfAccounts()
        {
            this.COMPTA_ChartOfAccounts = new HashSet<COMPTA_ChartOfAccounts>();
        }
    
    [DataMember]
        public long id { get; set; }
    [DataMember]
        public Nullable<long> number { get; set; }
    [DataMember]
        public string name { get; set; }
    
    [DataMember]
        public virtual ICollection<COMPTA_ChartOfAccounts> COMPTA_ChartOfAccounts { get; set; }
    }
}