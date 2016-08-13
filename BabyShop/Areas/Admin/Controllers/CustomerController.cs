using BabyShop.Common;
using Model.Dao;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET:  Customer/User

        [HasCredential(RoleID = "VIEW_CUSTOMER")]
        public ActionResult Index()
        {
            var result = new CustomerDao().ListAll();
            return View(result);
        }

        [HasCredential(RoleID = "DETAIL_CUSTOMER")]
        public ActionResult Details(int id)
        {
            var model = new CustomerDao().ViewDetail(id);
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_CUSTOMER")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CustomerDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "CHANGESTATUS_CUSTOMER")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new CustomerDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}