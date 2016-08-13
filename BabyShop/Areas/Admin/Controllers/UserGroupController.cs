using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        [HasCredential(RoleID = "VIEW_USERGROUP")]
        public ActionResult Index()
        {
            var result = new UserGroupDao().ListAll();
            return View(result);
        }

        [HasCredential(RoleID = "VIEW_USER")]       

        [HasCredential(RoleID = "ADD_USERGROUP")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_USERGROUP")]
        [HttpPost]
        public ActionResult Create(UserGroup model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = new UserGroupDao().CheckID(model.ID);
                    if (check)
                    {
                        ModelState.AddModelError("", "ID đã tồn tại");
                    }
                    else
                    {
                        bool id = new UserGroupDao().Insert(model);
                        if (id)
                        {
                            SetAlert("Thêm thành công", "success");
                            return RedirectToAction("Index", "UserGroup");
                        }
                        else
                            ModelState.AddModelError("", "Thêm thất bại");
                    }
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "EDIT_USERGROUP")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = new UserGroupDao().ViewDetail(id);
            return View(user);
        }

        [HasCredential(RoleID = "EDIT_USERGROUP")]
        [HttpPost]
        public ActionResult Edit(UserGroup UserGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new UserGroupDao().Update(UserGroup);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "UserGroup");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(UserGroup);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "DELETE_USERGROUP")]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var result = new UserGroupDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_USERGROUP")]
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var result = new UserGroupDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "UserGroup");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = new UserGroupDao().ViewDetail(id);
                return View(model);
            }

        }
    }
}