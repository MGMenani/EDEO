using Microsoft.AspNet.Identity;
using Project_EDEO.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_EDEO.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // Check if the user is not logged in
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the About landing page
                return RedirectToAction("Index", "About");
            }

            // Loqueras de Michael xD
            ViewBag.Image = "";
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

                // ACÁ SE PREPROCESA LA BOMBA Y SE AGARRA EL LINK DEL BICHILLO

                // This line calls the fuctions that knows how to estimate and which estimator use
                EstimatorModelsController estimatorModelsController = new EstimatorModelsController();
                string result = estimatorModelsController.Estimate(imagePath);

                // Converting age
                float age = float.Parse(result, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                ViewBag.Age = (int)age;
                ViewBag.Image = sourcePath;
                ViewBag.Pre = sourcePath;

                // Returning JSON
                return Json(new
                {
                    success = true,
                    response = "File uploaded",
                    image = sourcePath,
                    preprocess = sourcePath,
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
    }
}
