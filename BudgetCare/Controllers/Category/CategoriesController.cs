using BudgetCare.Controllers.Base;
using BudgetCare.Models;
using BudgetCare.ViewModel;
using BudgetCare.ViewModel.Resonse_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BudgetCare.Controllers
{
    
    public class CategoriesController : BaseController
    {
        DBProjectEntities2 db = new DBProjectEntities2();
        // GET: Categories
        public ActionResult Index()
        {
            var data = db.tbl_Caterory.ToList();
            return View(data);
        }
        

        public JsonResult getallRecord()
        {


            List<RecordViewModel> list = new List<RecordViewModel>();

            var emp = db.tbl_Caterory.ToList();

            foreach (var user in emp)
            {
                RecordViewModel userProfile = new RecordViewModel();
                userProfile.catryID = user.catryID;
                userProfile.catryTitle = user.catryTitle;
                userProfile.catDesp = user.catDesp;
                userProfile.imgUrl = user.imgUrl;

                list.Add(userProfile);
            }

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Create()
        {
            
            return View();
        }


        
        [HttpPost]
        public JsonResult SaveData(ImageViewModel cat)
        {
            var loogedInUser = GetUserProfile();
            if(cat.catryTitle !=null && cat.catDesp !=null && cat.catImg != null)
            { 
                string fileName = Path.GetFileNameWithoutExtension(cat.catImg.FileName);
                string extension = Path.GetExtension(cat.catImg.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;

             
                cat.catImg.SaveAs(Path.Combine(Server.MapPath("~/images/"), fileName));


                try
                {
                    tbl_Caterory obj = new tbl_Caterory();
                    obj.imgUrl = fileName;
                    obj.catDesp = cat.catDesp;
                    obj.catryTitle = cat.catryTitle;
                    obj.userID = loogedInUser.userId;

                    db.tbl_Caterory.Add(obj);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                   
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
              
            }
            var result = "Successfully Saved";
            return Json(result , JsonRequestBehavior.AllowGet);
        }

         


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var user = db.tbl_Caterory.Find(id);

            return View(user);

        }

        [HttpPost]

        public ActionResult Edit(tbl_Caterory user )
        {

            if (user.catryID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Caterory tbl_user = db.tbl_Caterory.Where(x => x.catryID == user.catryID).FirstOrDefault();
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            else
            {
                tbl_user.catryTitle = user.catryTitle;
                tbl_user.catDesp = user.catDesp;
                 



                db.Entry(tbl_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Categories", new { });


            }

        }


        public JsonResult Delete(int id)
        {
             



            try
            {

                //var incomes = db.tbl_Income.Where(x => x.catid == id).ToList();
                //foreach (var item in incomes) {
                //    db.tbl_Income.Remove(item);
                //    db.SaveChanges();
                //}

                //var expense = db.tbl_Expense.Where(x => x.catid == id).ToList();
                //foreach (var item in expense)
                //{
                //    db.tbl_Expense.Remove(item);
                //    db.SaveChanges();
                //}

                var or = db.tbl_Caterory.Find(id);
               
                db.tbl_Caterory.Remove(or);
                db.SaveChanges();
                 
                 
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }

            var result = "Successfully Delete";
            return Json(result, JsonRequestBehavior.AllowGet);



        }


    }
}