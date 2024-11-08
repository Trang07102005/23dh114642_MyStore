using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _23dh114642_MyStore.Models;
using _23dh114642_MyStore.Models.ViewModels;
using PagedList;
using System.Runtime.Remoting.Messaging;
using System.Web.Security;

namespace _23dh114642_MyStore.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)

            {
                //kiểm tra xem tên đăng nhập đã tốn tại chưa
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }

                //nếu chưa tổn tại thì tạo bản ghi thông tin tài khoản trong bảng User
                var user = new User

                {
                    Username = model.Username,
                    Password = model.Password, // Lưu ý: Nên mã hóa mật khẩu trước khi lưu
                    UserRole = "Customer"
                };

                db.Users.Add(user);
                //và tạo bản ghi thông tin khách hàng trong bảng Customer
                var customer = new Customer
                {

                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,

                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,

                };
                db.Customers.Add(customer);

                // lưu thông tin tài khoản và thông tin khách hàng vào CSDL

                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }
        // GET: Account/Login

        public ActionResult Login()

        {
            return View();

        }

        // POST: Account/Login

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)

            {

                var user =db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.UserRole == "Customer");

                if (user != null)

                {

                    // Lưu trạng thời đăng nhập vào session
                    Session ["Username "]= user.Username; 
                    Session ["UserRole"] = user.UserRole;
                    // lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie (user. Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                { 
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult ProfileInfo()
        {
            var user = db.Users.SingleOrDefault( u=>u.Username == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterVM
            {
                Username = user.Username,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ProfileInfo(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault( u=> u.Username == User.Identity.Name);
                if(user == null)
                {
                    return RedirectToAction("Login", "Account");
                }    

                var customer = db.Customers.SingleOrDefault(c=> c.Username == user.Username);
                if(customer==null)
                {
                    return RedirectToAction("Index", "Home");
                }    
                customer.CustomerName = model.CustomerName;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerAddress = model.CustomerAddress;

                db.SaveChanges();
                return RedirectToAction("ProFile");
            }    
            return View(model);
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if(user==null)
                {
                    return RedirectToAction("Login", "Account");
                }    
                user.Password = model.Password;
                db.SaveChanges();
                return RedirectToAction("ProfileInfo");
            }    
            return View(model);
        }
        public ActionResult UpdateAccount(int id)
        {
            return View();
        }
        [HttpPost]
       public ActionResult UpdateAccount(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ChangePassword(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}