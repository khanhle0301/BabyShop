using Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index()
        {           
            var model = new FeedbackDao().ListAll();
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new FeedbackDao().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var result = new FeedbackDao().ViewDetail(id);
            return View(result);
        }
    }
}