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
    public class CredentialController : BaseController
    {
        // GET: Admin/Credential
        public ActionResult Index(string id)
        {
            var checkUserGroup = new UserGroupDao().CheckUserGroupID(id);
            if (id == null || checkUserGroup.Count() == 0)
            {
                return RedirectToAction("Index", "UserGroup");
            }
            else
            {
                Session[Constants.USERGROUP_SESSION] = null;
                var result = new CredentialDao().ListAllByID(id);
                var nameGroup = new UserGroupDao().ViewDetail(id);
                var userGroupSession = new UserGroupCredential();
                userGroupSession.ID = nameGroup.ID;
                userGroupSession.Name = nameGroup.Name;
                Session.Add(Constants.USERGROUP_SESSION, userGroupSession);                     
                return View(result);
            }
        }

        public void SetViewBag(string userGroup)
        {
            var dao = new RoleDao();
            ViewBag.RoleID = new SelectList(dao.ListAll(userGroup), "ID", "Name");
        }

        public ActionResult Create()
        {
            var userGroup = (UserGroupCredential)Session[BabyShop.Common.Constants.USERGROUP_SESSION];
            SetViewBag(userGroup.ID);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Credential model)
        {
            if (ModelState.IsValid)
            {
                var userGroup = (UserGroupCredential)Session[BabyShop.Common.Constants.USERGROUP_SESSION];
                model.UserGroupID = userGroup.ID;
                var result = new CredentialDao().Insert(model);
                if (result)
                {
                    var url = "/Admin/Credential/Index/" + userGroup.ID;
                    SetAlert("Thêm danh thành công", "success");
                    return Redirect(url);
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
            SetViewBag(model.UserGroupID);
            return View(model);
        }


        [HttpPost]
        public JsonResult ChangeStatus(string groupId, string roleID)
        {
            var result = new CredentialDao().ChangeStatus(groupId, roleID);
            return Json(new
            {
                status = result
            });
        }
    }
}