using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            if (Session["loginuser"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            using (BlogService blogService = new BlogService())
            {
                var result = blogService.GetBlogs();

                return View(result);
            }
        }


        public ActionResult Save()
        {
            if (Session["loginuser"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.UserId = (Session["loginuser"] as UserDTO).UserId;

            using (CategoryService categoryService = new CategoryService())
            {
                ViewBag.Categories = categoryService.GetCategories();
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(BlogDTO obj)
        {
            using (BlogService blogService = new BlogService())
            {
                obj.UserId = (Session["loginuser"] as UserDTO).UserId;
                obj.ImagePath = "";
                var result = blogService.Save(obj);

                if (result > 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Get(int id)
        {
            if (Session["loginuser"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            
            using (BlogService blogService = new BlogService())
            {
                var result = blogService.Get(id);

                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    ViewBag.ErrorMessage = "Kayıt getirilirken bir hata oluştu.";
                    return View();
                }
            }
        }


        public ActionResult Edit(int id)
        {
           
            using (CategoryService categoryService = new CategoryService())
            {
                ViewBag.Categories = categoryService.GetCategories();
                
            }
            using (DefinitionService service = new DefinitionService())
            {
                ViewBag.RecordStatuses = service.GetRecordStatuses();
            }
            using (BlogService blogService = new BlogService())
            {
                var result = blogService.Get(id);

                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    ViewBag.ErrorMessage = "Kayıt getirilirken bir hata oluştu.";
                    return View();
                }

             
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(BlogDTO obj)
        {
          
            using (BlogService blogService = new BlogService())
            {

                var result = blogService.Update(obj);

                return Json(result);

              
            }
        }
    }
}