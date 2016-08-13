using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {           
            return View();
        }


        public ActionResult About()
        {
            var model = new AboutDao().GetAbout(1);
            return View(model);
        }

        public ActionResult Chinhsachbaomat()
        {
            var model = new AboutDao().GetAbout(2);
            return View(model);
        }

        public ActionResult Dieukhoansudung()
        {
            var model = new AboutDao().GetAbout(3);
            return View(model);
        }

        public ActionResult Huongdanmuahang()
        {
            var model = new AboutDao().GetAbout(4);
            return View(model);
        }

        public ActionResult Hinhthucthanhtoan()
        {
            var model = new AboutDao().GetAbout(5);
            return View(model);
        }

        public ActionResult Chinhsachgiaohang()
        {
            var model = new AboutDao().GetAbout(6);
            return View(model);
        }

        public ActionResult Chinhsachbaohanh()
        {
            var model = new AboutDao().GetAbout(7);
            return View(model);
        }

        public ActionResult Chinhsachdoitrahang()
        {
            var model = new AboutDao().GetAbout(8);
            return View(model);
        }
    }
}