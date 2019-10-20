using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class UserController : Controller
    {
        //List: FullName, CityName, TownName, EmailAddress,UserTypeName, Detay
        //Save
        //IsEmailVerified=true
        //UserTypeID=2
        public ActionResult Index()
        {
            //if (Session["loginuser"] == null)
            //{
            //    return RedirectToAction("Index", "Account");
            //}
            using (UserService service = new UserService())
            {
                var users = service.List();
                return View(users);
            }
        }


        public ActionResult Get(int id)
        {

            using (UserService service = new UserService())
            {
                var result = service.Get(id);

                return View(result);
            }



        }

        public ActionResult Save()
        {
            using (DefinitionService service = new DefinitionService())
            {

                ViewBag.Cities = service.GetCities();


            }

            return View();
        }



        [HttpPost]
        public ActionResult Save(UserDTO obj)
        {
            using (UserService service = new UserService())
            {


                obj.UserTypeID = 2;
                obj.IsEmailVerified = true;

                var result = service.Register(obj);
               

                if (result)
                {
                   
                    return RedirectToAction("Index","User");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bir hata oluştu.";
                    return View();
                }

            }
        }


        public ActionResult Edit(int id) //render sayfayı olusturuyo
        {
            using (DefinitionService service = new DefinitionService())
            {
                ViewBag.RecordStatuses = service.GetRecordStatuses();

            }
            using (UserService service = new UserService())
            {
                var result = service.Get(id);
                return View(result);
            }

        }

        [HttpPost]
        public ActionResult Edit(UserDTO obj) //veri posr
        {

            using (UserService service = new UserService())
            {
                var result = service.Update(obj);
                if (result)
                {
                    //Category/Get/5
                    return RedirectToAction("Get", new { id = obj.UserId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Bir hata oluştu.";
                    return View();
                }
            }
        }



        [HttpGet]
        public JsonResult GetTownsByCity(int id)
        {
            using (DefinitionService service = new DefinitionService())
            {
          
                return Json(service.GetTownsByCity(id), JsonRequestBehavior.AllowGet);

            }

        }

    }
}