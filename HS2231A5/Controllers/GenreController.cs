using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    public class GenreController : Controller
        {
        // Reference to the manager object
        private Manager m = new Manager();
        // GET: Genre
        public ActionResult Index()
            {
            // fetch the collection
            var genres = m.GenresGetAll();
            return View(genres);
            }

        }
    }