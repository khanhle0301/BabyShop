using Model.Dao;
using Model.EF;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var model = new ContactDao().ListAll();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = new ContactDao().ViewDetail(id);
            return View(model);
        }
      
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new ContactDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Contact Contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new ContactDao().Update(Contact);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Contact");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(Contact);
            }
            catch
            { return View(); }
        }
    }
}