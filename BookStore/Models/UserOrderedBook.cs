//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserOrderedBook
    {
        public int orderId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> bookId { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
