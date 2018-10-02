using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_EDEO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Check if the user is not logged in
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the About landing page
                return RedirectToAction("Index", "About");
            }

            // Loqueras de Michael xD
            ViewBag.flagvalue = flag;
            ViewBag.Image = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            flag = true;   //To know if there is a loaded image 
            ViewBag.flagvalue = flag;

            if (filePath != "")
            {
                // Redirect to the About landing page
                return RedirectToAction("Index", "About");
            }
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
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
