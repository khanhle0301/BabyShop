using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var model = new ContactDao().ListAll();
            return View(model);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContactDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = new ContactDao().Insert(model);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "Contact");
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
            var user = new ContactDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Contact Contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new ContactDao().Update(Contact);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Contact");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(Contact);
            }
            catch
            { return View(); }
        }
    }
}