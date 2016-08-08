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
    public class PostCategoryController : BaseController
    {
        // GET: Admin/PostCategory
        public ActionResult Index()
        {          
            var result = new PostCategoryDao().ListAllPaging();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = new PostCategoryDao().ViewDetail(id);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostCategory model)
        {
            if (ModelState.IsValid)
            {
                model.Metatitle = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.CreatedBy = entity.UserName;
                var result = new PostCategoryDao().Insert(model);
                if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "PostCategory");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new PostCategoryDao();
            var result = dao.ViewDetail(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(PostCategory model)
        {
            if (ModelState.IsValid)
            {
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.UpdatedBy = entity.UserName;
                var result = new PostCategoryDao().Update(model);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "PostCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new PostCategoryDao().ViewDetail(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = new PostCategoryDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "PostCategory");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var postCate = new PostCategoryDao().ViewDetail(id);
                return View(postCate);
            }           
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new PostCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}