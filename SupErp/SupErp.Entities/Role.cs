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
    [KnownType(typeof(RoleModule))]
    [KnownType(typeof(User))]
    
    public partial class Role
    {
        public Role()
        {
            this.RoleModules = new HashSet<RoleModule>();
            this.Users = new HashSet<User>();
        }
    
    [DataMember]
        public long Id { get; set; }
    [DataMember]
        public string Label { get; set; }
    
    [DataMember]
        public virtual ICollection<RoleModule> RoleModules { get; set; }
    [DataMember]
        public virtual ICollection<User> Users { get; set; }
    }
}
