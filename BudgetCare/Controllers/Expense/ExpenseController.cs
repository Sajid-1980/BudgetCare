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

namespace BudgetCare.Controllers.Category.Expense
{
     public class ExpenseController : BaseController
    {
        DBProjectEntities2 db = new DBProjectEntities2();
        // GET: Expense
        public ActionResult Expense()
        {
            var data = db.tbl_Expense.ToList();

            return View(data);

        }


        public JsonResult getallExp()
        {


            List<ExpViewModel> list = new List<ExpViewModel>();

            var emp = db.tbl_Expense.ToList();

            foreach (var user in emp)
            {
                ExpViewModel userProfile = new ExpViewModel();
                userProfile.expId = user.expId;
                userProfile.amount = user.amount;
                userProfile.date = user.date;
                userProfile.catryTitle = user.tbl_Caterory.catryTitle;
                userProfile.description = user.description;
                
                list.Add(userProfile);
            }

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateExp()
        {
            List<tbl_Caterory> CountryList = db.tbl_Caterory.ToList();
            ViewBag.CountryList = new SelectList(CountryList, "catryID", "catryTitle");
            return View();
        }




        [HttpPost]
        public JsonResult SaveExp(ExpViewModel cat)
        {
            var userlogin = GetUserProfile();


            try
            {

                tbl_Expense obj = new tbl_Expense();
              
                obj.amount = cat.amount;
                obj.date = cat.date;
                obj.description = cat.description;
                obj.catid = cat.catid;
                obj.userId = userlogin.userId;
                db.tbl_Expense.Add(obj);
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }



            var result = "Successfully Saved";
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult EditExp(int id)
        {


            ViewBag.cat = new SelectList(db.tbl_Caterory.ToList(), "catryID", "catryTitle");

            var user = db.tbl_Expense.Find(id);

            return View(user);

        }

        [HttpPost]

        public ActionResult EditExp(tbl_Expense user)
        {
            if (user.expId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            tbl_Expense usr = db.tbl_Expense.Where(x => x.expId == user.expId).FirstOrDefault();
            if (usr == null)
            {
                return HttpNotFound();
            }
            else
            {
                usr.expId = user.expId;
                usr.amount = user.amount;
                usr.date = user.date;
                usr.catid = user.catid;


                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Expense", "Expense");


            }

        }

        public JsonResult DeleteExp(int id)
        {
            BaseResponseModel obj = new BaseResponseModel();


            var or = db.tbl_Expense.Find(id);

            db.tbl_Expense.Remove(or);
            db.SaveChanges();



            return Json(obj, JsonRequestBehavior.AllowGet);


        }

    }
}