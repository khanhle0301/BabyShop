using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index()
        {
            var model = new AboutDao().ListAll();
            return View(model);
        }      

        public ActionResult Detail(int id)
        {
            var model = new AboutDao().ViewDetail(id);
            return View(model);
        }    

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new AboutDao().ViewDetail(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(About about)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var session = (AdminLogin)Session[Constants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    about.UpdatedBy = entity.UserName;
                    var result = new AboutDao().Update(about);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "About");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(about);
            }
            catch
            { return View(); }
        }     
    }
}