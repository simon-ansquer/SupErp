//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
