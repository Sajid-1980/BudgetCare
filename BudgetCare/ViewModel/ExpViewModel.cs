using BudgetCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetCare.ViewModel
{
    public class ExpViewModel
    {
        public int expId { get; set; }
        public int userId { get; set; }
        public decimal amount { get; set; }
        public System.DateTime date { get; set; }
        public string description { get; set; }
        public int catid { get; set; }
        public string catryTitle {get;set;}
        public virtual tbl_Caterory tbl_Caterory { get; set; }
    }
}