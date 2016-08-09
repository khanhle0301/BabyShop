using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class PermissionController : BaseController
    {
        // GET: Admin/Permission
        public ActionResult Index()
        {
            return View();
        }
    }
}