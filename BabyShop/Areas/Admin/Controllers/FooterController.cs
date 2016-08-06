using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var model = new FooterDao().ListAll();
            return View(model);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new FooterDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Footer model)
        {
            try
            {
                if (ModelState.IsValid)
                {                  
                    int id = new FooterDao().Insert(model);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "Footer");
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
            var user = new FooterDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Footer Footer)
        {
            try
            {
                if (ModelState.IsValid)
                {                  
                    var result = new FooterDao().Update(Footer);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Footer");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(Footer);
            }
            catch
            { return View(); }
        }

    }
}