using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index()
        {
            var model = new SlideDao().ListAll();
            return View(model);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Slide model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = new SlideDao().Insert(model);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "Slide");
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
            var user = new SlideDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Slide Slide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new SlideDao().Update(Slide);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(Slide);
            }
            catch
            { return View(); }
        }
    }
}