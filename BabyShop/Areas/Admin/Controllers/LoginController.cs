﻿using BabyShop.Areas.Admin.Models;
using BabyShop.Common;
using Model.Dao;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new AdminLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.Name = user.Name;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    userSession.Address = user.Address;
                    userSession.GroupID = user.GroupID;
                    var listCredentials = dao.GetListCredential(model.UserName);

                    Session.Add(Constants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(Constants.ADMIN_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[Constants.ADMIN_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}