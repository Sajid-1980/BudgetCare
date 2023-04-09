using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetCare.ViewModel
{
    public class ImageViewModel
    {
        public string catryTitle { get; set; }
        public string catDesp { get; set; }
        public  HttpPostedFileWrapper catImg { get; set; }
    }
}