using BabyShop.Models;
using BotDetect.Web.Mvc;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback();
                feedback.Name = feedbackViewModel.Name;
                feedback.Email = feedbackViewModel.Email;
                feedback.CreateDate = DateTime.Now;
                feedback.Subject = feedbackViewModel.Subject;
                feedback.Message = feedbackViewModel.Message;
                feedback.Status = true;

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/feedback.html"));
                content = content.Replace("{{name}}", feedbackViewModel.Name);
                content = content.Replace("{{email}}", feedbackViewModel.Email);
                content = content.Replace("{{subject}}", feedbackViewModel.Subject);
                content = content.Replace("{{message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                var mail = MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);
                var id = new ContactDao().InsertFeedBack(feedback);
                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";
            }

            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index", feedbackViewModel);
        }
      

        private Contact GetDetail()
        {
            var model = new ContactDao().GetActiveContact();
            return model;
        }
    }
}