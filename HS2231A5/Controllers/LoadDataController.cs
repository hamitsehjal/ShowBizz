using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
        {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadRoles
        [AllowAnonymous]
        public ActionResult Index()
            {
            if (m.LoadRoles())
                {
                ViewBag.Result = "Roles data has been loaded";
                }
            else
                {
                ViewBag.Result = "Roles data exists already";
                }

            return View("Result");
            }

        // GET: LoadGenres
        [Authorize(Roles = "Admin")]
        public ActionResult Genres()
            {
            if (m.LoadGenres())
                {
                ViewBag.Result = "Genres data has been loaded";
                }
            else
                {
                ViewBag.Result = "Genres data already exists";
                }

            return View("Result");
            }


        // GET: LoadActors
        [Authorize(Roles = "Admin")]
        public ActionResult Actors()
            {
            if (m.LoadActors())
                {
                ViewBag.Result = "Actors data has been loaded";
                }
            else
                {
                ViewBag.Result = "Actors data already exists";
                }

            return View("Result");
            }

        // GET: LoadShows
        [Authorize(Roles = "Admin")]
        public ActionResult Shows()
            {
            if (m.LoadShows())
                {
                ViewBag.Result = "Shows data has been loaded";
                }
            else
                {
                ViewBag.Result = "Shows data already exists";
                }

            return View("Result");
            }

        // GET: LoadShows
        [Authorize(Roles = "Admin")]
        public ActionResult Episodes()
            {
            if (m.LoadEpisodes())
                {
                ViewBag.Result = "Episodes data has been loaded";
                }
            else
                {
                ViewBag.Result = "Episodes data already exists";
                }
            return View("Result");
            }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteData()
            {
            if (m.RemoveData())
                {
                ViewBag.Result = "Successful Removal Data";
                }
            else
                {
                ViewBag.Result = "FAILED Removal Data";

                }
            return View("Result");
            }
        //// GET: LoadData
        //public ActionResult Index()
        //    {
        //    if (m.LoadData())
        //        {
        //        return Content("data has been loaded");
        //        }
        //    else
        //        {
        //        return Content("data exists already");
        //        }
        //    }

        public ActionResult Remove()
            {
            if (m.RemoveData())
                {
                return Content("data has been removed");
                }
            else
                {
                return Content("could not remove data");
                }
            }

        public ActionResult RemoveDatabase()
            {
            if (m.RemoveDatabase())
                {
                return Content("database has been removed");
                }
            else
                {
                return Content("could not remove database");
                }
            }

        }
    }