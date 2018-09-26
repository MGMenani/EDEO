using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_EDEO.Models;

namespace Project_EDEO.Controllers.AccountControllers
{
    public class SpecificDiagnostics : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpecificDiagnostics
        public ActionResult Index()
        {
            var diagnostics = db.Diagnostics.Include(d => d.MedicalRecord);
            return View(diagnostics.ToList());
        }

        // GET: SpecificDiagnostics/Details/5
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

        // GET: SpecificDiagnostics/Create
        public ActionResult Create()
        {
            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name");
            return View();
        }

        // POST: SpecificDiagnostics/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiagnosticID,EstimatedAge,Image,Date,MedicalRecordID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                diagnostic.DiagnosticID = Guid.NewGuid();
                db.Diagnostics.Add(diagnostic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name", diagnostic.MedicalRecordID);
            return View(diagnostic);
        }

        // GET: SpecificDiagnostics/Edit/5
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

        // POST: SpecificDiagnostics/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiagnosticID,EstimatedAge,Image,Date,MedicalRecordID")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnostic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalRecordID = new SelectList(db.MedicalRecords, "MedicalRecordID", "Name", diagnostic.MedicalRecordID);
            return View(diagnostic);
        }

        // GET: SpecificDiagnostics/Delete/5
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

        // POST: SpecificDiagnostics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Diagnostic diagnostic = db.Diagnostics.Find(id);
            db.Diagnostics.Remove(diagnostic);
            db.SaveChanges();
            return RedirectToAction("Index");
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
