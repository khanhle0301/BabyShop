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
    public class PostController : BaseController
    {
        // GET: Admin/Post
        public ActionResult Index()
        {           
            var result = new PostDao().ListAllPaging();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post model)
        {
            if (ModelState.IsValid)
            {
                model.Metatitle = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.CreatedBy = entity.UserName;
                var result = new PostDao().Insert(model);
                if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "Post");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
            SetViewBag();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new PostDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.CategoryID);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                    var entity = new AdminDao().GetByID(session.UserName);
                    model.UpdatedBy = entity.UserName;
                    var result = new PostDao().Update(model);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Post");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại");
                    }
                }
            }
            SetViewBag(model.ID);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var result = new PostDao().ViewDetail(id);
            return View(result);
        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new PostCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new PostDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}