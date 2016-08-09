using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        public ActionResult Index()
        {
            var result = new UserGroupDao().ListAll();
            return View(result);
        }

        [HttpGet]
        public JsonResult CheckID(string id)
        {
            var result = new UserGroupDao().CheckID(id);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserGroup model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool id = new UserGroupDao().Insert(model);
                    if (id)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "UserGroup");
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
        public ActionResult Edit(string id)
        {
            var user = new UserGroupDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserGroup UserGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new UserGroupDao().Update(UserGroup);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "UserGroup");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(UserGroup);
            }
            catch
            { return View(); }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var result = new UserGroupDao().ViewDetail(id);
            return View(result);
        }
     
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var result = new UserGroupDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "UserGroup");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = new UserGroupDao().ViewDetail(id);
                return View(model);
            }

        }
    }
}