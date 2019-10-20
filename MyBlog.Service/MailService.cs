using MyBlog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class MailService : BaseService
    {
        public bool SendMail(MailDTO mail)
        {
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

                MailMessage mailMessage = new MailMessage("ayvansarayuni2019@gmail.com", mail.To,
                    mail.Subject,
                    mail.Body);
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
