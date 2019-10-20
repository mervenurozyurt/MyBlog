using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyBlog.Api.Controllers
{
    public class BlogController : ApiController
    {
        [HttpPost]
        [EnableCors(origins:"*",headers:"*",methods:"post")]//* herkese açık 
        public IHttpActionResult Save(BlogDTO obj)
        {
            using (BlogService blogService = new BlogService())
            {
                
                obj.ImagePath = "";
                var result = blogService.Save(obj);

                if (result > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return InternalServerError();
                }
            }

        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "post")]
        public IHttpActionResult Edit(BlogDTO obj)
        {
            using (BlogService service = new BlogService())
            {
                obj.ImagePath = "";
                var result = service.Update(obj);

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
        //blog update sayfaında olur
        //ajax ile post 
        //hangi blogpostuna update id

        [HttpPost]
        public IHttpActionResult Upload(int id)
        {
            using (BlogService service=new BlogService())
            {
               // id = 3;
                var blog = service.Get(id);

                var allowedExtension = new[] { ".jgep", ".png", ".jpg" };//sadece bunlar kabul ediliyor.

                HttpFileCollection hfc = HttpContext.Current.Request.Files;//post işlemi bir file collection gönderiyor
                //dosyalar requestten geliyor bize
                //requesttin altında file tipi HttpFileCollection
                if (hfc.Count>0)//gelen dosyaların countu
                {
                    try
                    {
                        HttpPostedFile file = hfc[0];//collectionun ".elemanın al bana ver dosya alındı
                        if (allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))//uzantı kontrolu
                        {
                            ///HostingEnvironment:blog apinin yolunu verir
                            ///bunun altında files onun altında blogs
                            ///Combine:ayrı ayrı verilen stringlerden dosya yolu oluşturur
                            string rootPath = Path.Combine(HostingEnvironment.MapPath("~/"), "files", "blogs");//dosya nereye kaydedilsin
                            ///resmin oluşturma
                            ///kolay tahmin edilmemesi için : guid
                            ///dosya uzantısı:extension
                            string filename= Path.Combine(id+"_blog_"+Guid.NewGuid()+Path.GetExtension(file.FileName));//dosya adı

                            ///eğer idsi ile bulunan blogun daha önce resmi varsa
                            ///onu siliyoruz
                            if (!string.IsNullOrEmpty(blog.ImagePath))
                            {
                                File.Delete(Path.Combine(rootPath,blog.ImagePath));
                            }
                            ///bu klasör ve dosya adını birleştirerek bana geeln httpfile kaydet
                            file.SaveAs(Path.Combine(rootPath,filename));

                            ///dosya adını güncellle
                            blog.ImagePath = filename;

                            ///yeni adı ile kaydet
                            var result = service.Update(blog);

                            if (result)
                            {
                                return Ok(true);
                            }
                            else
                            {
                                return InternalServerError();

                            }
                        }

                        else
                        {
                            return Unauthorized();
                        }
                    }
                    catch (Exception ex)
                    {
                        return InternalServerError(ex);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
