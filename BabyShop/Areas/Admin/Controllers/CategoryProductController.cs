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
    public class CategoryProductController : BaseController
    {
        // GET: Admin/CategoryProduct
        public ActionResult Index()
        {           
            var result = new CategoryProductDao().ListAllPaging();
            return View(result);
        }
        
        public ActionResult Details(int id)
        {
            var result = new CategoryProductDao().ViewDetail(id);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryProductModel model)
        {
            if (ModelState.IsValid)
            {
                var cateProduct = new CategoryProduct();
                cateProduct.Name = model.Name;
                cateProduct.MetaTitle = StringHelper.ToUnsignString(model.Name);
                cateProduct.ParentID = model.ParentID;
                cateProduct.CreatedDate = DateTime.Now;
                cateProduct.DisplayOrder = model.DisplayOrder == null ? 1 : model.DisplayOrder;
                var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                cateProduct.CreatedBy = entity.UserName;
                cateProduct.Status = model.Status;
                var result = new CategoryProductDao().Insert(cateProduct);
                if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "CategoryProduct");
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
            var dao = new CategoryProductDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.ParentID);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(CategoryProduct model)
        {
            if (ModelState.IsValid)
            {
                var cate = new CategoryProduct();
                var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                var entity = new AdminDao().GetByID(session.UserName);
                model.UpdatedBy = entity.UserName;
                var result = new CategoryProductDao().Update(model);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "CategoryProduct");
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
            var dao = new CategoryProductDao();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryProductDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult CateNameExists(string name)
        {
            var result = new CategoryProductDao().NameExists(name);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

    }
}