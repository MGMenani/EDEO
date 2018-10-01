using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
