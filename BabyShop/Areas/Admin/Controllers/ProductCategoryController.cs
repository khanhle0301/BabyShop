using BabyShop.Areas.Admin.Models;
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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {           
            var result = new ProductCategoryDao().ListAllPaging();
            return View(result);
        }
        
        public ActionResult Details(int id)
        {
            var result = new ProductCategoryDao().ViewDetail(id);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);                
                model.CreatedDate = DateTime.Now;              
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.CreatedBy = entity.UserName;               
                var result = new ProductCategoryDao().Insert(model);
                if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
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
            var dao = new ProductCategoryDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.ParentID);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var cate = new ProductCategory();
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.UpdatedBy = entity.UserName;
                var result = new ProductCategoryDao().Update(model);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(model.ID);
            return View(model);
        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListAllParent(), "ID", "Name", selectedId);
        }

        public ActionResult Delete(int id)
        {
            var result = new ProductCategoryDao().ViewDetail(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dao= new ProductCategoryDao().Delete(id);
            if (dao)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var result = new ProductCategoryDao().ViewDetail(id);
                return View(result);
            }          
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ProductCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}