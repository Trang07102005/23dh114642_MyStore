using _23dh114642_MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _23dh114642_MyStore.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private MyStoreEntities db=new MyStoreEntities();
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products.ToList());
        }
    }
}