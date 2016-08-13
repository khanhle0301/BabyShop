using Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class PostController : Controller
    {
        // GET: Post        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int cateId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            var model = new PostDao().ListAllByCateID(cateId, page, pageSize, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.Category = new PostCategoryDao().ViewDetail(cateId);
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalCount = totalRow;
            ViewBag.MaxPage = MaxPage;
            ViewBag.Page = page;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = new PostDao().GetByID(id);
            ViewBag.Tags = new PostDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            int MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            ViewBag.Tag = new PostDao().GetTag(tagId);
            var model = new PostDao().ListAllByTag(tagId, page, pageSize, out totalRow);
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
    }
}