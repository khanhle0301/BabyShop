using BabyShop.Areas.Admin.Models;
using BabyShop.Common;
using Model.Dao;
using Model.ViewModel;
using System;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/AdminUser
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            var result = new UserDao().ListAllPaging();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_USER")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag(null);
            return View();
        }

        [HasCredential(RoleID = "ADD_USER")]
        [HttpPost]
        public ActionResult Create(AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var admin = new Model.EF.User();
                var encryptedMd5Pas = Encryptor.MD5Hash(adminModel.Password);
                admin.UserName = adminModel.UserName;
                admin.Password = encryptedMd5Pas;
                admin.Name = adminModel.Name;
                admin.Address = adminModel.Address;
                admin.Email = adminModel.Email;
                admin.Phone = adminModel.Phone;
                admin.CreatedDate = DateTime.Now;
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = dao.GetByID(session.UserName);
                admin.CreatedBy = entity.UserName;
                admin.GroupID = adminModel.GroupID;
                admin.Status = adminModel.Status;
                int id = dao.Insert(admin);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
            SetViewBag(adminModel.GroupID);
            return View(adminModel);
        }

        [HasCredential(RoleID = "EDIT_USER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new UserDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.GroupID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_USER")]
        [HttpPost]
        public ActionResult Edit(Model.EF.User admin)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = dao.GetByID(session.UserName);
                admin.UpdatedBy = entity.UserName;
                var result = dao.Update(admin);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }
            SetViewBag(admin.GroupID);
            return View(admin);
        }

        [HasCredential(RoleID = "DETAIL_USER")]
        public ActionResult Detail(int id)
        {
            var result = new UserDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_USER")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new UserDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_USER")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = new UserDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = new UserDao().ViewDetail(id);
                return View(model);
            }
        }

        [HttpGet]
        public JsonResult UserNameExists(string username)
        {
            var result = new UserDao().UserNameExists(username);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EmailExists(string email)
        {
            var result = new UserDao().EmailExists(email);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBag(string selectedId)
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.List_All(), "ID", "Name", selectedId);
        }

        [HasCredential(RoleID = "CHANGESTATUS_USER")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public ActionResult Info()
        {          
            return View();
        }

        [HttpGet]
        public ActionResult EditInfo()
        {
            var dao = new UserDao();
            var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
            var entity = dao.GetByID(session.UserName);        
            var result = dao.ViewDetail(session.UserID);
            SetViewBag(result.GroupID);
            return View(result);
        }

        [HttpPost]
        public ActionResult EditInfo(Model.EF.User admin)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = dao.GetByID(session.UserName);
                admin.UpdatedBy = entity.UserName;
                var result = dao.Update(admin);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }
            SetViewBag(admin.GroupID);
            return View(admin);
        }

        [HttpGet]
        public ActionResult ChangePass()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(Changepass adminModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                var entity = dao.GetByID(session.UserName);
                var admin = new Model.EF.User();
                var encryptedMd5Pas = Encryptor.MD5Hash(adminModel.Password);                
                admin.Password = encryptedMd5Pas;
                admin.ID = entity.ID;
                var result = dao.ChangePass(admin);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }           
            return View(adminModel);
        }
    }
}