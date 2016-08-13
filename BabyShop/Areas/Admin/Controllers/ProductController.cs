using BabyShop.Common;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        [HasCredential(RoleID = "VIEW_PRODUCT")]
        public ActionResult Index()
        {
            var result = new ProductDao().ListAllPaging();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_PRODUCT")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HasCredential(RoleID = "ADD_PRODUCT")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (model.PromotionFlag && (model.PromotionPrice == null || model.PromotionPrice >= model.Price))
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    model.Metatitle = StringHelper.ToUnsignString(model.Name);
                    model.CreatedDate = DateTime.Now;
                    var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.CreatedBy = entity.UserName;
                    model.ViewCount = 0;
                    model.QuantitySold = 0;
                    if (model.PromotionFlag)
                    {
                        model.PromotionPrice = model.PromotionPrice;
                    }
                    else
                    {
                        model.PromotionPrice = null;
                    }
                    var result = new ProductDao().Insert(model);
                    if (result > 0)
                    {
                        SetAlert("Thêm danh thành công", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            SetViewBag();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new ProductDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.CategoryID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                if (model.PromotionFlag && (model.PromotionPrice == null || model.PromotionPrice >= model.Price))
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var session = (AdminLogin)Session[Common.Constants.ADMIN_SESSION];
                        var entity = new UserDao().GetByID(session.UserName);
                        model.UpdatedBy = entity.UserName;
                        if (model.PromotionFlag)
                        {
                            model.PromotionPrice = model.PromotionPrice;
                        }
                        else
                        {
                            model.PromotionPrice = null;
                        }
                        var result = new ProductDao().Update(model);
                        if (result)
                        {
                            SetAlert("Cập nhật thành công", "success");
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật thất bại");
                        }
                    }
                }
            }
            SetViewBag(model.CategoryID);
            return View(model);
        }

        [HasCredential(RoleID = "DETAIL_PRODUCT")]
        public ActionResult Details(int id)
        {
            var result = new ProductDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_PRODUCT")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new ProductDao().ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_PRODUCT")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dao = new ProductDao().Delete(id);
            if (dao)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var result = new ProductDao().ViewDetail(id);
                return View(result);
            }
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListByChild(), "ID", "Name", selectedId);
        }

        public JsonResult LoadImages(int id)
        {
            ProductDao dao = new ProductDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImage;
            List<string> listImagesReturn = new List<string>();
            if (!string.IsNullOrEmpty(images))
            {
                XElement xImages = XElement.Parse(images);
                foreach (XElement element in xImages.Elements())
                {
                    listImagesReturn.Add(element.Value);
                }
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }

        [HasCredential(RoleID = "MORE_PRODUCT")]
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(26);
                //var subStringItem = item.Substring(22);
                xElement.Add(new XElement("Image", subStringItem));
            }
            ProductDao dao = new ProductDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HasCredential(RoleID = "CHANGESTATUS_PRODUCT")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}