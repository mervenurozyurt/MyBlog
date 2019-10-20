using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index(int id)
        {
            using (CommentService service = new CommentService())
            {
                var result = service.List(id);
                return View(result);
            }
        }

        public ActionResult Confirm(int id)
        {

            using (CommentService service = new CommentService())
            {
                var obj = service.Get(id);
                var result = service.Confirm(obj);
                if (result)
                {
                    return RedirectToAction("Index","Blog");
                }
                return HttpNotFound();
            }
                
        }   
    }
}