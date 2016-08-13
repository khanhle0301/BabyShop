using BabyShop.Common;
using Model.Dao;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        [HasCredential(RoleID = "VIEW_FEEDBACK")]
        public ActionResult Index()
        {
            var model = new FeedbackDao().ListAll();
            return View(model);
        }

        [HasCredential(RoleID = "CHANGESTATUS_FEEDBACK")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new FeedbackDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "DELETE_FEEDBACK")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new FeedbackDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "DETAIL_FEEDBACK")]
        public ActionResult Detail(int id)
        {
            var result = new FeedbackDao().ViewDetail(id);
            return View(result);
        }
    }
}