using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_EDEO.Models;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

namespace Project_EDEO.Controllers
{
    public class DiagnosticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diagnostics
        public ActionResult Index()
        {
            return View(db.Diagnostics.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnostics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiagnosticID,EstimatedAge,Image,Date,MedicalRecordID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                diagnostic.DiagnosticID = Guid.NewGuid();
                db.Diagnostics.Add(diagnostic);
                db.SaveChanges();
                return RedirectToAction("Details/" + diagnostic.MedicalRecordID, "MedicalRecords/");
            }

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
            return View(diagnostic);
        }

        // POST: Diagnostics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiagnosticID,EstimatedAge,Image,Date,MedicalRecordID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnostic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + diagnostic.MedicalRecordID, "MedicalRecords/");
            }
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
            return RedirectToAction("Details/" + diagnostic.MedicalRecordID, "MedicalRecords/");
        }

        // GET: Diagnostics/Share
        public ActionResult Share()
        {
            //ViewBag.Numero = db.Users;            
            return View();
        }
        //GetEmail
        public  string GetEmail()
        {
            var IdToSearch  = User.Identity.GetUserId(); 
            var SearchEmail = db.Users.Where(x=> x.Id.Contains(IdToSearch) || IdToSearch == null).ToList();
            var email = "";
            foreach (var item in SearchEmail) {
                email = item.Email;
            }
            
            return email;
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
                mail.Body = "From " + contact.Name + " <" + contact.Email + ">" + "\n\n" + contact.Message;
                client.Send(mail);

                return RedirectToAction("Index");
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
