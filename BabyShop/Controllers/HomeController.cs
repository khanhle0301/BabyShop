using BabyShop.Models;
using Common;
using Model.Dao;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int Top = int.Parse(ConfigHelper.GetByKey("TopHome"));
            var productdao = new ProductDao();
            ViewBag.ListSlide = new SlideDao().SelectAll();
            ViewBag.NewProduct = productdao.ListNewProduct(Top);
            ViewBag.HotProduct = productdao.ListHotProduct(Top);
            ViewBag.PromotionProduct = productdao.ListPromotionProduct(Top);

            ViewBag.Title = ConfigHelper.GetByKey("HomeTitle");
            ViewBag.Keywords = ConfigHelper.GetByKey("HomeKeyword");
            ViewBag.Descriptions = ConfigHelper.GetByKey("HomeDescription");

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new PostCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopHot()
        {
            int Top = int.Parse(ConfigHelper.GetByKey("TopHot"));
            var model = new ProductDao().ListAllHotFlag(Top);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[BabyShop.Common.Constants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }          
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}