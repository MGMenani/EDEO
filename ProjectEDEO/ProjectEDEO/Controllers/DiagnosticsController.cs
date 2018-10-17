using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_EDEO.Models;

namespace Project_EDEO.Controllers
{
    public class DiagnosticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diagnostics
        public ActionResult Index()
        {
            var diagnostics = db.Diagnostics.Include(d => d.MedicalRecord);
            string user = User.Identity.GetUserId();
            return View(diagnostics.Where(d => d.UserID == user).ToList());
        }

        // GET: Diagnostics/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            if (diagnostic == null)
            {
                return HttpNotFound();
            }
            return View(diagnostic);
        }

        // GET: Diagnostics/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name");
        //    return View();
        //}

        // GET: Diagnostics/Create
        public ActionResult Create(int age, string image, string pre)
        {
            ViewBag.Age = age;
            ViewBag.Image = image;
            ViewBag.Pre = pre;
            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name");
            return View();
        }

        // POST: Diagnostics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiagnosticID,ChronologicalAge,ModelEstimatedAge,DoctorEstimatedAge,Image,Date,MedicalRecordID,UserID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                diagnostic.DiagnosticID = Guid.NewGuid();
                diagnostic.UserID = User.Identity.GetUserId();
                db.Diagnostics.Add(diagnostic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name", diagnostic.MedicalRecordID);
            return View(diagnostic);
        }

        // GET: Diagnostics/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            if (diagnostic == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name", diagnostic.MedicalRecordID);
            return View(diagnostic);
        }

        // POST: Diagnostics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiagnosticID,ChronologicalAge,ModelEstimatedAge,DoctorEstimatedAge,Image,Date,MedicalRecordID,UserID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                diagnostic.UserID = User.Identity.GetUserId();
                db.Entry(diagnostic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name", diagnostic.MedicalRecordID);
            return View(diagnostic);
        }

        // GET: Diagnostics/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            if (diagnostic == null)
            {
                return HttpNotFound();
            }
            return View(diagnostic);
        }

        // POST: Diagnostics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            db.Diagnostics.Remove(diagnostic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Diagnostics/Share
        public ActionResult Share()
        {
            //ViewBag.Numero = db.Users;            
            return View();
        }
        // GetEmail
        public string GetEmail()
        {
            var IdToSearch = User.Identity.GetUserId();
            var SearchEmail = db.Users.Where(x => x.Id.Contains(IdToSearch) || IdToSearch == null).ToList();
            var email = "";
            foreach (var item in SearchEmail)
            {
                email = item.Email;
            }

            return email;
        }

        //GetDiagnosticAge
        public string GetDiagnostic(Guid? id)
        {
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            var SearchInfo = db.MedicalRecords.Find(diagnostic.MedicalRecordID);
            var a = diagnostic.Date.ToString();
            var b = SearchInfo.Name.ToString();
            var c = SearchInfo.LastName.ToString();
            var d = diagnostic.ModelEstimatedAge.ToString();
            var e = SearchInfo.BornDate.ToString();
            var Final = " Diagnostic date: " + a + ", \nName: " + b + ", \nLast Name: " + c + ", \nEstimated Age: " + d + " months, \nBorn Date: " + e + " \n";
            ViewBag.idSharediagnostic = id;
            return Final;
        }

        // POST: Diagnostics/Share

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Share([Bind(Include = "Name,Email,Subject,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage("edeoproject@gmail.com", "edeoproject@gmail.com");

                // More addresses
                string addresses = contact.Email;

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
                string[] words = contact.Message.Split(',');
                string FinalMessage = "";
                foreach (string word in words)
                {
                    FinalMessage += word;
                    FinalMessage += "\n";
                }
                mail.Body = "From: " + contact.Name + " \nTo: " + contact.Email + " " + "\n\n" + FinalMessage;
                client.Send(mail);
                string Forum = (string)this.RouteData.Values["Forum"];
                return RedirectToAction("", "MedicalRecords/");
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
