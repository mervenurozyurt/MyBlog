using System;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBlog.UnitTest
{
    [TestClass]
    public class MailUT
    {
        [TestMethod]
        public void SendMail()
        {
            //ayvansarayuni2019@gmail.com
            //au2019au

            try
            {
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Credentials = new NetworkCredential("ayvansarayuni2019@gmail.com", "au2019au")
                };

                MailMessage mail = new MailMessage("ayvansarayuni2019@gmail.com", "bulentbasyurt@gmail.com",
                    "Deneme mail gönderimi",
                    "Deneme mail body");

                client.Send(mail);
            }
            catch (Exception ex)
            {

            }

        }
    }
}
