//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Theme_18_HomeWork
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accaunts
    {
        public int id { get; set; }
        public System.DateTime OpenDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int TypeId { get; set; }
        public float Rates { get; set; }
        public decimal Balans { get; set; }
        public int OwnerId { get; set; }
        public bool Capitalisation { get; set; }
        public int RatesTypeid { get; set; }
    
        public virtual AccauntType AccauntType { get; set; }
        public virtual ratesType ratesType { get; set; }
    }
}
