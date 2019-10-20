using MyBlog.Panel.Models;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            if (Session["loginuser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            using (UserService service = new UserService())
            {
                var user = service.Authenticate(model.UserName, model.Password);

                if (user == null)
                {
                    ViewBag.LoginResult = "Hatalı kullanıcı adı veya şifre.";
                    return View();
                }
                else
                {
                    //Session: Oturum
                    Session["loginuser"] = user;
                    //SessionId => browser sessionStorage
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Remove("loginuser");

            return RedirectToAction("Index", "Account");
        }

        public ActionResult Verify(string id)
        {
            using (UserService userService = new UserService())
            {
                bool result = userService.Verify(id);

                if (result)
                {

                }

                return View();
            }
        }
    }
}