using Project_EDEO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Diagnostics;

namespace Project_EDEO.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }

        // GET: About/Us
        public ActionResult Us()
        {
            return View();
        }

        // GET: About/Model
        public ActionResult Model()
        {
            return View();
        }

        // GET: About/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // POST: About/Estimate
        [HttpPost]
        public ActionResult Estimate(HttpPostedFileBase file)
        {
            try
            {
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/images/uploads"), filePath));

                string imagePath = Path.Combine(Server.MapPath("~/images/uploads"), filePath);
                
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "python";
                start.Arguments = string.Format("{0} {1} {2}", Server.MapPath("~/Estimator/estimator.py"), imagePath, "male");
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                string result = "";
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd();
                        result = reader.ReadToEnd();
                        if (result != "")
                        {
                            ViewBag.Estimation = result + " meses.";
                        }
                        else
                            ViewBag.Estimation = stderr;

                        ViewBag.Image = "~/images/uploads/" + filePath;
                    }
                }
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    success = false,
                    response = exception.Message
                });
            }

            return Json(new
            {
                success = true,
                response = "File uploaded."
            });
        }

        // POST: About/Email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email([Bind(Include = "Name,Email,Subject,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage("96jaslat@gmail.com", "96jaslat@gmail.com");
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    Credentials = new System.Net.NetworkCredential("96jaslat@gmail.com", "CONTRASEÑAMIEDO"),
                    EnableSsl = true
                };
                mail.Subject = "[EDEO] - " + contact.Subject;
                mail.Body = "From " + contact.Name + "(" + contact.Email + ")" + "\n" + contact.Message;
                client.Send(mail);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
