using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetCare.ViewModel
{
    public class UserViewModel
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string image { get; set; }
        public Nullable<System.DateTime> joingdate { get; set; }
        public string name { get; set; }
    }
}