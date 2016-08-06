using BabyShop.Areas.Admin.Models;
using BabyShop.Common;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {         
            var result = new ProductDao().ListAllPaging();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                if (model.PromotionPrice.HasValue && model.Price.Value <= decimal.Parse(model.PromotionPrice.HasValue.ToString()))
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    product.Name = model.Name;
                    product.Metatitle = StringHelper.ToUnsignString(model.Name);
                    product.CategoryID = model.CategoryID;
                    product.Image = model.Image;                  
                    product.Price = model.Price;
                    product.PromotionPrice = model.PromotionPrice;
                    product.Quantity = model.Quantity;
                    product.Description = model.Description;
                    product.Detail = model.Detail;
                    product.Size = model.Size;
                    product.Tag = model.Tag;                   
                    product.CreatedDate = DateTime.Now;
                    var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                    var entity = new AdminDao().GetByID(session.UserName);
                    product.CreatedBy = entity.UserName;
                    product.Status = model.Status;
                    var result = new ProductDao().Insert(product);
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new ProductDao();
            var result = dao.ViewDetail(id);
            SetViewBag(result.CategoryID);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                if (model.PromotionPrice.HasValue && model.Price.Value <= decimal.Parse(model.PromotionPrice.HasValue.ToString()))
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var session = (AdminLogin)Session[Common.CommonConstants.ADMIN_SESSION];
                        var entity = new AdminDao().GetByID(session.UserName);
                        model.UpdatedBy = entity.UserName;
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
            SetViewBag(model.ID);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var result = new ProductDao().ViewDetail(id);
            return View(result);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryProductDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpGet]
        public JsonResult NameExists(string name)
        {
            var result = new ProductDao().NameExists(name);
            return Json(!result, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(22);
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
    }
}