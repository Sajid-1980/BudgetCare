using BudgetCare.Models;
using BudgetCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetCare.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        DBProjectEntities2 db = new DBProjectEntities2();
        // GET: Base
        [NonAction]
        public UserViewModel GetUserProfile()
        {
            UserViewModel loginUser = new UserViewModel();
            var user = db.tbl_User.FirstOrDefault(x => x.email == User.Identity.Name);
            loginUser.userId = user.userId;
            loginUser.name = user.name;
            loginUser.joingdate = user.joingdate;
            loginUser.email = user.email;
            loginUser.contactNo = user.contactNo;
           
            loginUser.image = user.image;
          

            //loginUser.FirebaseToken = user.firebase_token.Count() > 0 ? user.firebase_token.FirstOrDefault().token : null;

            return loginUser;
        }
    }
}