//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseSolution.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class REVIEWS
    {
        public int ID { get; set; }
        public Nullable<int> ORDER_ID { get; set; }
        public Nullable<int> RATING { get; set; }
        public string NOTES { get; set; }
    
        public virtual ORDERS ORDERS { get; set; }
    }
}
