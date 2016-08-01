using BabyShop.Areas.Admin.Models;
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
        BabyShopDbContext db = new BabyShopDbContext();
        // GET: Admin/CategoryProduct
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.StatusSortParm = sortOrder == "status" ? "status_desc" : "status";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var result = new ProductCategoryDao().ListAllPaging(sortOrder, searchString, pageNumber, pageSize);
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
                var dao = new CategoryProduct();
                dao.Name = model.Name;
                dao.ParentID = model.ParentID;
                dao.Status = model.Status;
                var result = new ProductCategoryDao().Insert(dao);
                if (result > 0)
                {
                    SetAlert("Thêm danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "CategoryProduct");
                }
                else
                    ModelState.AddModelError("", "Thêm danh mục sản phẩm thất bại");
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
        public ActionResult Edit(CategoryProduct model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật danh mục sản phẩm thành công", "success");
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
            var dao = new ProductCategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpGet]
        public JsonResult NameExists(string name)
        {
            return Json(!db.CategoryProducts.Any(x => x.Name == name), JsonRequestBehavior.AllowGet);
        }

    }
}