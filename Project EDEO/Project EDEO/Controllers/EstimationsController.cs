using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_EDEO.Controllers
{
    public class EstimationsController : Controller
    {
        // GET: Estimations
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), filePath));
                //Here you can write code for save this information in your database if you want
            }
            return Json("file uploaded successfully");
        }
    }
}