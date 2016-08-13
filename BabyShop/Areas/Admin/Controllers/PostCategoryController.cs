using BabyShop.Common;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class PostCategoryController : BaseController
    {
        // GET: Admin/PostCategory
        [HasCredential(RoleID = "VIEW_POSTCATEGORY")]
        public ActionResult Index()
        {
            var result = new PostCategoryDao().ListAllPaging();
            return View(result);
        }

        [HasCredential(RoleID = "DETAIL_POSTCATEGORY")]
        public ActionResult Details(int id)
        {
            var result = new PostCategoryDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "ADD_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_POSTCATEGORY")]
        [HttpPost]
        public ActionResult Create(PostCategory model)
        {
            if (ModelState.IsValid)
            {
                model.Metatitle = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new UserDao().GetByID(session.UserName);
                model.CreatedBy = entity.Name;
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

        [HasCredential(RoleID = "EDIT_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new PostCategoryDao();
            var result = dao.ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_POSTCATEGORY")]
        [HttpPost]
        public ActionResult Edit(PostCategory model)
        {
            if (ModelState.IsValid)
            {
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new UserDao().GetByID(session.UserName);
                model.UpdatedBy = entity.Name;
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

        [HasCredential(RoleID = "DELETE_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new PostCategoryDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_POSTCATEGORY")]
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

        [HasCredential(RoleID = "CHANGESTATUS_POSTCATEGORY")]
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