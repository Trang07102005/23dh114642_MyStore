using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _23dh114642_MyStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult OrderSucces()
        {
            return View();
        }
    }
}