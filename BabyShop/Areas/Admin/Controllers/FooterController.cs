using Model.Dao;
using Model.EF;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var model = new FooterDao().ListAll();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var res = new FooterDao().ViewDetail(id);
            return View(res);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var res = new FooterDao().ViewDetail(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit(Footer Footer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new FooterDao().Update(Footer);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Footer");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(Footer);
            }
            catch
            { return View(); }
        }
    }
}