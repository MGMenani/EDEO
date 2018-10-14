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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: About
        public ActionResult Index()
        {
            ViewBag.EstimationCount = db.Estimations.Count();
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
                // Giving new name and saving to disk
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/images/uploads"), filePath));

                // Server path for processing
                string imagePath = Path.Combine(Server.MapPath("~/images/uploads"), filePath);

                // Resource path for HTML
                string sourcePath = "/images/uploads/" + filePath;

                // Preparing for a Python call
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = string.Format("{0} {1} {2}", Server.MapPath("~/Estimator/estimator.py"), imagePath, "male"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                // Ready for call
                string result = "";
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        // Calling
                        string stderr = process.StandardError.ReadToEnd();
                        result = reader.ReadToEnd();

                        // If no results
                        if (result == "")
                            throw new System.NullReferenceException("Empty result");
                    }
                }
                float age = float.Parse(result, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);            

                // Create new entry
                Estimation estimation = new Estimation
                {
                    EstimationID = Guid.NewGuid(),
                    Gender = Gender.female,
                    Image = sourcePath,
                    EstimatedAge = age,
                    IPAddress = Request.UserHostAddress,
                    DateTime = DateTime.Now
                };

                // Save entry
                db.Estimations.Add(estimation);
                db.SaveChanges();

                // Returning JSON
                return Json(new
                {
                    success = true,
                    response = "File uploaded",
                    image = sourcePath,
                    estimation = age
                });
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    success = false,
                    response = exception.Message
                });
            }
        }

        // POST: About/Email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email([Bind(Include = "Name,Email,Subject,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage("edeoproject@gmail.com", "edeoproject@gmail.com");

                // More addresses
                string addresses = "jlatouche@ic-itcr.ac.cr;migramenani1@gmail.com;alonsors.809@gmail.com;parma@tec.ac.cr"; 

                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(address);
                }

                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    Credentials = new System.Net.NetworkCredential("edeoproject@gmail.com", "edeoboneage18"),
                    EnableSsl = true
                };

                // Mail body
                mail.Subject = "[EDEO] - " + contact.Subject;
                mail.Body = "From " + contact.Name + " <" + contact.Email + ">" + "\n\n" + contact.Message;
                client.Send(mail);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
