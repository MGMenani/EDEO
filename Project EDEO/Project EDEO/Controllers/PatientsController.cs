﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_EDEO.Controllers
{
    public class PatientsController : Controller
    {
        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }
    }
}