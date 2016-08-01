using BabyShop.Areas.Admin.Models;
using BabyShop.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace BabyShop.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {

        // GET: Admin/AdminUser
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.UserNameSortParm = sortOrder == "username" ? "username_desc" : "username";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = new AdminDao().ListAllPaging(sortOrder, searchString, pageNumber, pageSize);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new AdminDao();
                    var admin = new Model.EF.Admin();
                    var encryptedMd5Pas = Encryptor.MD5Hash(adminModel.Password);
                    admin.UserName = adminModel.UserName;
                    admin.Password = encryptedMd5Pas;
                    admin.Name = adminModel.Name;
                    admin.Address = adminModel.Address;
                    admin.Email = adminModel.Email;
                    admin.Phone = adminModel.Phone;
                    admin.CreatedDate = DateTime.Now;
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    if (session != null)
                    {
                        var entity = dao.GetByID(session.UserName);
                        admin.CreatedBy = entity.UserName;
                    }
                    admin.Status = adminModel.Status;
                    int id = dao.Insert(admin);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
                return View(adminModel);
            }
            catch
            { return View(); }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new AdminDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {                  
                    var dao = new AdminDao();                    
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    if (session != null)
                    {
                        var entity = dao.GetByID(session.UserName);
                        admin.UpdatedBy = entity.UserName;
                    }
                    var result = dao.Update(admin);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(admin);
            }
            catch
            { return View(); }
        }

        [HttpDelete]   
        public ActionResult Delete(int id)
        {
            new AdminDao().Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]      
        public JsonResult ChangeStatus(int id)
        {
            var result = new AdminDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


        [HttpGet]
        public JsonResult UserNameExists(string username)
        {
            var result = new AdminDao().UserNameExists(username);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult EmailExists(string email)
        {
            var result = new AdminDao().EmailExists(email);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }
    }
}