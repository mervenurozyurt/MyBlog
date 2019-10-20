using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBlog.Api.Controllers
{
    public class DefinitionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTownsByCity(int id)
        {
            using (DefinitionService service=new DefinitionService())
            {
                var result = service.GetTownsByCity(id);
                if (result==null)
                {
                    return Content(HttpStatusCode.NotFound, "Vierdiğiniz il kodu ile ilçe bulunamadı");

                    //return NotFound();
                }

                return Content(HttpStatusCode.OK, result);
                //return Ok(result);
            }
        }
    }
}
