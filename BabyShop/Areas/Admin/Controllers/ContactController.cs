using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        [HasCredential(RoleID = "VIEW_CONTACT")]
        public ActionResult Index()
        {
            var model = new ContactDao().ListAll();
            return View(model);
        }

        [HasCredential(RoleID = "DETAIL_CONTACT")]
        public ActionResult Detail(int id)
        {
            var model = new ContactDao().ViewDetail(id);
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_CONTACT")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new ContactDao().ViewDetail(id);
            return View(user);
        }

        [HasCredential(RoleID = "EDIT_CONTACT")]
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