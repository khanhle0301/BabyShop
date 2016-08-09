using BabyShop.Common;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET:  Customer/User        

        public ActionResult Index()
        {
            var result = new CustomerDao().ListAll();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var model = new CustomerDao().ViewDetail(id);
            return View(model);
        }
     
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CustomerDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new CustomerDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}