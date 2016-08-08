using Model.Dao;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            var model = new OrderDao().ListAll();
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var result = new OrderDao().ViewDetail(id);
            return View(result);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new OrderDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}