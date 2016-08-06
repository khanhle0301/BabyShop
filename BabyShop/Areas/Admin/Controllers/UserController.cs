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
    public class UserController : BaseController
    {
        // GET: Admin/User        

        public ActionResult Index()
        {
            var result = new UserDao().ListAll();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var model = new UserDao().ViewDetail(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new UserDao().Update(user);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(user);
            }
            catch
            { return View(); }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}