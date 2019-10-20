using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBlog.Api.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Contact(MessageDTO obj)
        {
            using (MessageService service=new MessageService())
            {
                var result=service.SendMessage(obj);
                if (result)
                {
                    return Ok(true);
                }
                else
                {
                    return InternalServerError();
                }
            }

        }
    }
}
