using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AboutDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(About model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new AboutDao();                  
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new AdminDao().GetByID(session.UserName);
                    model.CreatedBy = entity.UserName;
                    model.CreatedDate = DateTime.Now;
                    int id = dao.Insert(model);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "About");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new AboutDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.About about)
        {
            try
            {
                if (ModelState.IsValid)
                {                 
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new AdminDao().GetByID(session.UserName);
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