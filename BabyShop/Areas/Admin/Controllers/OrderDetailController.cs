using Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: Admin/OrderDetail
        public ActionResult Index()
        {          
            var model = new OrderDetailDao().ListAll();
            return View(model);
        }          
    }
}