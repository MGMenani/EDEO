﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Project_EDEO.Models;

namespace Project_EDEO.Controllers
{
    public class EstimatorModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstimatorModels
        public ActionResult Index()
        {
            return View(db.EstimatorModels.ToList());
        }

        // GET: EstimatorModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            if (estimatorModel == null)
            {
                return HttpNotFound();
            }
            return View(estimatorModel);
        }

        // GET: EstimatorModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstimatorModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstimatorModelID,Name,Directory,PythonFile,Active,DateTime")] EstimatorModel estimatorModel)
        {
            if (ModelState.IsValid)
            {
                estimatorModel.EstimatorModelID = Guid.NewGuid();
                estimatorModel.DateTime = DateTime.Now;
                estimatorModel.Directory = "~/Estimator/" + estimatorModel.Name.Replace(' ', '_') + "-" + Guid.NewGuid();
                estimatorModel.Active = false;
                
                Directory.CreateDirectory(Server.MapPath(estimatorModel.Directory));

                db.EstimatorModels.Add(estimatorModel);
                db.SaveChanges();
                return RedirectToAction("Upload", new { id = estimatorModel.EstimatorModelID });
            }

            return View(estimatorModel);
        }

        // GET: EstimatorModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            if (estimatorModel == null)
            {
                return HttpNotFound();
            }
            return View(estimatorModel);
        }

        // POST: EstimatorModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstimatorModelID,Name,Directory,PythonFile,Active,DateTime")] EstimatorModel estimatorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estimatorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estimatorModel);
        }

        // GET: EstimatorModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            if (estimatorModel == null)
            {
                return HttpNotFound();
            }
            return View(estimatorModel);
        }

        // POST: EstimatorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);

            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(estimatorModel.Directory));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            if (Directory.Exists(Server.MapPath(estimatorModel.Directory)))
            {
                Directory.Delete(Server.MapPath(estimatorModel.Directory));
            }

            db.EstimatorModels.Remove(estimatorModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: EstimatorModels/Activate/5
        public ActionResult Activate(Guid id)
        {
            foreach (var Estimator in db.EstimatorModels.Where(e => e.Active == true))
            {
                Estimator.Active = false;
                db.Entry(Estimator).State = EntityState.Modified;
            }
            db.SaveChanges();

            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            estimatorModel.Active = true;

            db.Entry(estimatorModel).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: EstimatorModels/Deactivate/5
        public ActionResult Deactivate(Guid id)
        {
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            estimatorModel.Active = false;

            db.Entry(estimatorModel).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: EstimatorModels/Upload
        public ActionResult Upload(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstimatorModel estimatorModel = db.EstimatorModels.Find(id);
            if (estimatorModel == null)
            {
                return HttpNotFound();
            }
            return View(estimatorModel);
        }

        // POST: EstimatorModels/Upload
        [HttpPost]
        public ActionResult Upload(Guid id, HttpPostedFileBase[] files)
        {
            try
            {
                EstimatorModel estimatorModel = db.EstimatorModels.Find(id);

                // Giving new name and saving to disk
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        file.SaveAs(Path.Combine(Server.MapPath(estimatorModel.Directory), file.FileName));
                    }
                }
                
                // Returning JSON
                return Json(new
                {
                    success = true,
                    response = "File uploaded"
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

        public string Estimate(string imagePath)
        {
            // Default running file
            string runningPath = "~/Estimator/Default/estimator.py";

            // Finding the active estimator
            IQueryable<EstimatorModel> estimatorModels = db.EstimatorModels.Where(e => e.Active == true);
            if (estimatorModels.LongCount() != 0)
            {
                var estimatorModel = estimatorModels.First();
                runningPath = Path.Combine(estimatorModel.Directory, estimatorModel.PythonFile);
            }

            // Preparing for a Python call
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = string.Format("{0} {1} {2}", HostingEnvironment.MapPath(runningPath), imagePath, "male"),
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
            return result;
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
