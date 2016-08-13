using BabyShop.Common;
using Model.Dao;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: Admin/OrderDetail
        [HasCredential(RoleID = "VIEW_ORDERDETAIL")]
        public ActionResult Index()
        {
            var model = new OrderDetailDao().ListAll();
            return View(model);
        }
    }
}