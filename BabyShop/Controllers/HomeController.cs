using Model.Dao;
using BabyShop.Common;
using BabyShop.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;

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
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
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
            var cart = Session[BabyShop.Common.CommonConstants.CartSession];
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