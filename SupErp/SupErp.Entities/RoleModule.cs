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
    
    public partial class RoleModule
    {
        public long Id { get; set; }
        public long Module_id { get; set; }
        public long Role_id { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Role Role { get; set; }
    }
}