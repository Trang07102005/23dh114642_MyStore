﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _23dh114642_MyStore.Models;
using _23dh114642_MyStore.Models.ViewModels;
using PagedList;

namespace _23dh114642_MyStore.Controllers
{
    public class CartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        //Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }
        //Hiển thị giỏ hàng không gom nhóm theo danh mục
        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }
        //Thêm sản phẩm vào giỏ
        public ActionResult AddToCart(int id, int quantity =1)
        {
            var product = db.Products.Find(id);
            if(product != null)
            {
                var CartService = GetCartService();
                CartService.GetCart().AddItem(product.ProductID, product.ProductImage, product.ProductName, product.ProductPrice, quantity, product.Category.CategoryName);
            } 
                return RedirectToAction("Index");
        }
        //Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }
        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cartService = GetCartService();
            cartService.GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}