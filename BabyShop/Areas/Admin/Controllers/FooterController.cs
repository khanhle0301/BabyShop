using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        [HasCredential(RoleID = "VIEW_FOOTER")]
        public ActionResult Index()
        {
            var model = new FooterDao().ListAll();
            return View(model);
        }

        [HasCredential(RoleID = "DETAIL_FOOTER")]
        public ActionResult Detail(int id)
        {
            var res = new FooterDao().ViewDetail(id);
            return View(res);
        }

        [HasCredential(RoleID = "EDIT_FOOTER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var res = new FooterDao().ViewDetail(id);
            return View(res);
        }

        [HasCredential(RoleID = "EDIT_FOOTER")]
        [HttpPost]
        [ValidateInput(false)]
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