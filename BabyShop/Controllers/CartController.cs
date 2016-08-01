﻿using BabyShop.Common;
using BabyShop.Models;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BabyShop.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[BabyShop.Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult GetUser()
        {
            var session = (UserLogin)Session[BabyShop.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var userId = session.UserID;
                var user = new UserDao().GetID(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<CartItem>)Session[BabyShop.Common.CommonConstants.CartSession];
            var product = new ProductDao().ViewDetail(productId);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Sản phẩm này hiện đang hết hàng"
                });
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                CartItem newItem = new CartItem();
                newItem.ProductId = productId;
                newItem.Product = product;
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            Session[BabyShop.Common.CommonConstants.CartSession] = cart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[BabyShop.Common.CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[BabyShop.Common.CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[BabyShop.Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[BabyShop.Common.CommonConstants.CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[BabyShop.Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderDao = new OrderDao();
            var orderNew = new Order();
            orderNew.CreatedDate = DateTime.Now;
            orderNew.CustomerAddress = order.Address;
            orderNew.CustomerMobile = order.Phone;
            orderNew.CustomerName = order.Name;
            orderNew.CustomerEmail = order.Email;
            orderNew.CustomerMessage = order.Message;
            orderDao.Insert(orderNew);

            var detailDao = new Model.Dao.OrderDetailDao();
            var cart = (List<CartItem>)Session[BabyShop.Common.CommonConstants.CartSession];
            decimal total = 0;
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.OrderID = orderNew.ID;
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                if (item.Product.PromotionPrice.HasValue)
                {
                    detail.Price = item.Product.PromotionPrice;
                    total += (item.Product.PromotionPrice.GetValueOrDefault(0) * item.Quantity);
                }
                else
                {
                    detail.Price = item.Product.Price;
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                detailDao.Insert(detail);

            }

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));
            content = content.Replace("{{CustomerName}}", order.Name);
            content = content.Replace("{{Phone}}", order.Phone);
            content = content.Replace("{{Email}}", order.Email);
            content = content.Replace("{{Address}}", order.Address);
            content = content.Replace("{{Total}}", total.ToString("N0"));
            var adminEmail = ConfigHelper.GetByKey("AdminEmail");
            MailHelper.SendMail(order.Email, "Đơn hàng mới từ BabyShop", content);
            MailHelper.SendMail(adminEmail, "Đơn hàng mới từ BabyShop", content);
            Session[BabyShop.Common.CommonConstants.CartSession] = null;

            return Json(new
            {
                status = true
            });
        }
    }
}