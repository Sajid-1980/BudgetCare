using BudgetCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BudgetCare.Controllers
{
    public class LoginController : Controller
    {
        DBProjectEntities2 db = new DBProjectEntities2();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(tbl_User user)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.tbl_User.Where(model => model.email == user.email && model.password == user.password).FirstOrDefault();
                if (data == null)
                {
                    TempData["Login"] = "<script>alert(' Login Falied')</script>";
                    return View();
                }
                else
                {



                    FormsAuthentication.SetAuthCookie(user.email, false);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

         public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}