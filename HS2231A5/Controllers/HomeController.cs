﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    public class HomeController : Controller
        {
        // Reference to the manager object
        Manager m = new Manager();

        public ActionResult Index()
            {
            m.LoadRoles();
            return View();
            }
        }
    }