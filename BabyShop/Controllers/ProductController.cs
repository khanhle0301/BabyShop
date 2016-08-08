using Common;
using Model.Dao;
using System;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int cateId, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var productModel = new ProductDao().GetListProductByCategoryIdPaging(cateId, page, pageSize, sort, out totalRow);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Category = new ProductCategoryDao().ViewDetail(cateId);

            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(productModel);
        }

        public ActionResult Detail(int id)
        {
            var updateViewCount = new ProductDao().UpdateViewCount(id);
            var product = new ProductDao().ViewDetail(id);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            ViewBag.MoreImage = new ProductDao().MoreImage(id);
            ViewBag.Tags = new ProductDao().ListTag(id);
            ViewBag.ListSize = new ProductDao().ListSize(id);
            return View(product);
        }

        public ActionResult Tag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            ViewBag.Tag = new PostDao().GetTag(tagId);
            var model = new ProductDao().ListAllByTag(tagId, page, pageSize, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public JsonResult ListName(string keyword)
        {
            var data = new ProductDao().ListName(keyword);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var productModel = new ProductDao().Search(keyword, page, pageSize, sort, out totalRow);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(productModel);
        }

        public ActionResult ViewAllProduct(int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var productModel = new ProductDao().ListAllProduct(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(productModel);
        }

        public ActionResult ViewAllSaleOffProduct(int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var productModel = new ProductDao().ListAllSaleOffProduct(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(productModel);
        }

        public ActionResult ViewAllHotProduct(int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var productModel = new ProductDao().ListAllHotProduct(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(productModel);
        }
    }
}