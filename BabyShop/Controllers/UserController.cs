using BotDetect.Web.Mvc;
using Facebook;
using Model.Dao;
using Model.EF;
using BabyShop.Common;
using BabyShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.ViewModel;

namespace BabyShop.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.Name = user.Name;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    userSession.Address = user.Address;
                    Session.Add(Constants.USER_SESSION, userSession);
                    return Redirect("/");
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
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new Customer();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewData["SuccessMsg"] = "Đăng ký thành công";
                    }
                    else
                        ModelState.AddModelError("", "Đăng ký thất bại");
                }
            }
            return View(model);
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigHelper.GetByKey("FbAppId"),
                client_secret = ConfigHelper.GetByKey("FbAppSecret"),
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;               
                string lastname = me.last_name;
                var user = new Customer();
                user.UserName = email;             
                user.Name = firstname + " " + middlename + " " + lastname;
                user.Email = email;              
                user.Status = true;             
                var resultInsert = new CustomerDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    var dao = new CustomerDao();

                    userSession.UserName = user.UserName;               
                    var entity = dao.GetByID(user.UserName);
               
                    userSession.UserID = entity.ID;
                    userSession.Name = user.Name;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    userSession.Address = user.Address;
                    Session.Add(Constants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        public ActionResult Info()
        {
            if (Session[Common.Constants.USER_SESSION] == null)
                return Redirect("/dang-nhap");
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if (Session[Common.Constants.USER_SESSION] == null)
                return Redirect("/dang-nhap");
            else
            {
                var dao = new CustomerDao();
                var session = (UserLogin)Session[Common.Constants.USER_SESSION];
                var entity = dao.GetByID(session.UserName);
                var result = dao.ViewDetailEdit(session.UserID);
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserEditModel customer)
        {
            if (ModelState.IsValid)
            {                
                var dao = new CustomerDao();
                var entity = new Customer();
                entity.ID = customer.ID;
                entity.Name = customer.Name;
                entity.Address = customer.Address;
                entity.Email = customer.Email;
                entity.Phone = customer.Phone;
                var result =dao.Update(entity);
                if (result)
                {
                    ViewData["SuccessMsg"] = "Cập nhât thành công";
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Changepass()
        {
            if (Session[Common.Constants.USER_SESSION] == null)
                return Redirect("/dang-nhap");
            return View();
        }

        [HttpPost]
        public ActionResult Changepass(ChangepassUser adminModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var session = (UserLogin)Session[Common.Constants.USER_SESSION];
                var entity = dao.GetByID(session.UserName);
                var admin = new Customer();
                var encryptedMd5Pas = Encryptor.MD5Hash(adminModel.Password);
                admin.Password = encryptedMd5Pas;
                admin.ID = entity.ID;
                var result = dao.ChangePass(admin);
                if (result)
                {
                    ViewData["SuccessMsg"] = "Đổi mật khẩu thành công";
                }
                else
                    ModelState.AddModelError("", "Đổi mật khẩu thất bại");
            }
            return View(adminModel);
        }

        [HttpGet]
        public ActionResult Forgotpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgotpassword(Forgotpassword forgotpassword)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var admin = new Customer();
                var newpass = new KeyGenerator().GetUniqueKey();
                var encryptedMd5Pas = Encryptor.MD5Hash(newpass);
                admin.Password = encryptedMd5Pas;
                admin.Email = forgotpassword.Email;
                var result = dao.Forgotpassword(admin);
                //send mail
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/forgotpass.html"));
                content = content.Replace("{{Newpass}}", newpass);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap");
                var sendmail = MailHelper.SendMail(forgotpassword.Email, "Lấy lại mật khẩu thành công", content);
                if (result && sendmail)
                {
                    ViewData["SuccessMsg"] = "Lấy mật khẩu thành công";
                }
                else
                    ModelState.AddModelError("", "Gởi thất bại");
            }
            return View(forgotpassword);
        }
    }
}