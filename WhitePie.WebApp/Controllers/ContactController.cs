using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace WhitePie.WebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string name, string email, string phone, string subject, string message)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("EdibleMami Team", "management@ediblemami.com"));
            mail.ReplyTo.Add(new MailboxAddress(name, email));
            mail.To.Add(MailboxAddress.Parse("r.p.rollinger@gmail.com"));
            //mail.To.Add(MailboxAddress.Parse("management@ediblemami.com"));
            //mail.Cc.Add(MailboxAddress.Parse("vanessa@ediblemami.com"));

            mail.Subject = subject;
            mail.Body = new TextPart("plain")
            {
                Text = $"{message}\n\nPhone: {phone}"
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("r.p.rollinger@gmail.com", "vxzrwvzogcrrvjrr");
                client.Send(mail);
                TempData[TempDataKey.AlertSuccess] = "Your message was sent, thank you!";
            }
            catch(Exception ex)
            {
                TempData[TempDataKey.AlertDanger] = ex.Message;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

            return RedirectToAction("Index");
        }
    }
}
