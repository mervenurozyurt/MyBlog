using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (BlogService service = new BlogService())
            {
                var result = service.GetBlogs();
                return View(result);
            }
           
        }


        public ActionResult Detail(int id)
        {
            using (BlogService service = new BlogService())
            {
                var result = service.Get(id);
                return View(result);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //[HttpPost]
        //public JsonResult SendMessage(MessageDTO obj)
        //{
        //    using (MessageService service = new MessageService())
        //    {
        //        var result = service.SendMessage(obj);
              
        //            return Json(obj,JsonRequestBehavior.AllowGet);
 
        //    }

        //}
    }
}