using BabyShop.Areas.Admin.Models;
using BabyShop.Common;
using Model.Dao;
using System;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/AdminUser
        [HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Index()
        {
            var result = new AdminDao().ListAllPaging();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_ADMIN")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_ADMIN")]
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
                    var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                    var entity = dao.GetByID(session.UserName);
                    admin.CreatedBy = entity.UserName;
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

        [HasCredential(RoleID = "EDIT_ADMIN")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new AdminDao().ViewDetail(id);
            return View(user);
        }

        [HasCredential(RoleID = "EDIT_ADMIN")]
        [HttpPost]
        public ActionResult Edit(Model.EF.Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new AdminDao();
                    var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                    var entity = dao.GetByID(session.UserName);
                    admin.UpdatedBy = entity.UserName;
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

        [HasCredential(RoleID = "DETAIL_ADMIN")]
        public ActionResult Detail(int id)
        {
            var result = new AdminDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_ADMIN")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new AdminDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_ADMIN")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = new AdminDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = new AdminDao().ViewDetail(id);
                return View(model);
            }
            
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

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new AdminDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}