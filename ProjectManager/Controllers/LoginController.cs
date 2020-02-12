using ProjectManager.DAL;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectManager.Controllers
{
    public class LoginController : Controller
    {
        private ManagerContext db = new ManagerContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string ReturnUrl)
        {
            if (IsValid(user))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return Redirect("/");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Courses()
        {
            return RedirectToAction("Index", "Course", null);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        public bool IsValid(User user)
        {
            User dbUser = db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            return dbUser != null ? true : false;
        }
    }
}