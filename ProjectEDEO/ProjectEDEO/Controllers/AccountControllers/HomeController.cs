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

        public int imageGui = 5;
        bool flag = false;
        string filePath = "1377.png";
        string pythonPath = @"C:\\Users\\migra\\AppData\\Local\\Programs\\Python\\Python35\\python.exe";

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
                string imagePath = Path.Combine(Server.MapPath("~/images/uploads"), filePath);

                //llama la función de python
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonPath;
                start.Arguments = string.Format("{0} {1} {2}", Server.MapPath("~/Estimator/estimator.py"), imagePath + "", "male"); //Path to .py file and any cmd line args
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                string result = "";                                //Resultado de python
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd();
                        result = reader.ReadToEnd();
                        if (result != "")
                        {
                            result = cortarString(result);
                            ViewBag.Estimation = result + " meses.";
                        }
                        else
                            ViewBag.Estimation = stderr;

                        ViewBag.Image = "~/images/uploads/" + filePath;
                    }
                }
            }
            else
            {
                ViewBag.Estimation = "Debe ingresar una imagen";
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

        //Cuts python return to delete garbage data.
        protected string cortarString(string s)
        {
            char[] arrayResult = { };                          //Utilizado para voltear el resultado y cortar lo innecesario
            int endString = 0;                                 //Saber cuántos caracteres sirven en el resultado

            arrayResult = s.ToCharArray().Reverse().ToArray(); //Se pasa el string a array y se voltea
            s = new string(arrayResult);                       //se pasa el array a string
            endString = s.IndexOf("p");                        //cantidad de caracteres útiles
            s = s.Substring(0, endString);                        //Corta lo necesario
            //Ahora a voltearlo de nuevo
            arrayResult = s.ToCharArray().Reverse().ToArray();
            s = new string(arrayResult);

            return s;
        }
    }
}
