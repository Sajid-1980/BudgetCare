using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetCare.ViewModel
{
    public class RecordViewModel
    {
        public int catryID { get; set; }
        public string catryTitle { get; set; }
        public string catDesp { get; set; }
        public string imgUrl { get; set; }
        public int userID { get; set; }
        public bool isActive { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }

    }
}