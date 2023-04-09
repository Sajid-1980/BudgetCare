using BudgetCare.Controllers.Base;
using BudgetCare.Models;
using BudgetCare.ViewModel;
using BudgetCare.ViewModel.Resonse_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BudgetCare.Controllers.Income
{
     public class IncomeController : BaseController
    {
        // GET: Income
        DBProjectEntities2 db = new DBProjectEntities2();
        public ActionResult Income()
        {
            var data = db.tbl_Income.ToList();

            return View(data);

        }

        public JsonResult getallIncome()
        {


            List<IncomeViewModel> list = new List<IncomeViewModel>();

            var emp = db.tbl_Income.ToList();

            foreach (var user in emp)
            {
                IncomeViewModel userProfile = new IncomeViewModel();
                userProfile.incId = user.incId;
                userProfile.amount = user.amount;
                userProfile.catryTitle = user.tbl_Caterory.catryTitle;
                userProfile.date = user.date;
                userProfile.catid = user.catid;
                userProfile.description = user.description;

                list.Add(userProfile);
            }

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateIncome()
        {
           
            List<tbl_Caterory> CountryList = db.tbl_Caterory.ToList();
            ViewBag.CountryList = new SelectList(CountryList, "catryID", "catryTitle");
            return View();
        }


         

        [HttpPost]
        public JsonResult SaveIncome(IncomeViewModel cat)
        {
            var loginUser = GetUserProfile();


            try
            {

                tbl_Income obj = new tbl_Income();
                obj.amount = cat.amount;
                obj.date = cat.date;
                obj.description = cat.description;
                obj.catid = cat.catid;
                obj.userId = loginUser.userId;

                db.tbl_Income.Add(obj);
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }



            var result = "Successfully Saved";
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult DeleteIncome(int id)
        {
            BaseResponseModel obj = new BaseResponseModel();


            var or = db.tbl_Income.Find(id);

            db.tbl_Income.Remove(or);
            db.SaveChanges();



            return Json(obj, JsonRequestBehavior.AllowGet);


        }



        [HttpGet]
        public ActionResult EditIncome(int id)
        {


            ViewBag.cat = new SelectList(db.tbl_Caterory.ToList(), "catryID", "catryTitle");

            var user = db.tbl_Income.Find(id);

            return View(user);

        }

        [HttpPost]

        public ActionResult EditIncome(tbl_Income user)
        {

            if (user.incId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Income usr = db.tbl_Income.Where(x => x.incId == user.incId).FirstOrDefault();
            if (usr == null)
            {
                return HttpNotFound();
            }
            else
            {
                usr.incId = user.incId;
                usr.amount = user.amount;
                usr.date = user.date;
                usr.catid = user.catid;
                usr.description = user.description;

                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Income", "Income");


            }

        }

    }
}