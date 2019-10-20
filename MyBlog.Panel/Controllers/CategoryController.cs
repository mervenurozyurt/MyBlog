using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            if (Session["loginuser"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            using (CategoryService service = new CategoryService())
            {
                var categories = service.GetCategories();

                return View(categories);
            }
        }

        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(CategoryDTO obj)
        {
            using (CategoryService service = new CategoryService())
            {
                var result = service.Save(obj);

                if (result > 0)
                {
                    // Category/Get/5
                    return RedirectToAction("Get", new { id = result });
                }
                else
                {
                    ViewBag.ErrorMessage = "Bir hata oluştu.";
                    return View();
                }
            }
        }

        public ActionResult Get(int id)
        {
            using (CategoryService service = new CategoryService())
            {
                var result = service.Get(id);

                return View(result);
            }
        }

        public ActionResult Edit(int id)
        {
            using (DefinitionService service = new DefinitionService())
            {
                ViewBag.RecordStatuses = service.GetRecordStatuses();
            }

            using (CategoryService service = new CategoryService())
            {
                var result = service.Get(id);

                return View(result);
            }
        }

        [HttpPost]
        public ActionResult Edit(CategoryDTO obj)
        {
            using (CategoryService service = new CategoryService())
            {
                var result = service.Update(obj);

                if (result)
                {
                    // Category/Get/5
                    return RedirectToAction("Get", new { id = obj.CategoryId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Bir hata oluştu.";
                    return View();
                }
            }
        }
    }
}