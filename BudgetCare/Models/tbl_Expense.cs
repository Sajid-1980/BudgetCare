//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetCare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Expense
    {
        public int expId { get; set; }
        public int userId { get; set; }
        public decimal amount { get; set; }
        public System.DateTime date { get; set; }
        public string description { get; set; }
        public int catid { get; set; }
    
        public virtual tbl_Caterory tbl_Caterory { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}