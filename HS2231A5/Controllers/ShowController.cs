using HS2231A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    public class ShowController : Controller
        {
        private Manager m = new Manager();

        // GET ALL: Show
        public ActionResult Index()
            {
            // fetch the collection
            var shows = m.ShowsGetAll();
            return View(shows);
            }

        // GET ONE: Show/Details/5
        public ActionResult Details(int? id)
            {
            // try to find the item
            var show = m.ShowsGetOne(id.GetValueOrDefault());
            if (show == null)
                return HttpNotFound();

            return View(show);
            }

        // ADD NEW 'Episode' for this 'Show'
        // GET: Show/{id}/AddEpisode
        [HttpGet]
        [Authorize(Roles = "Clerk")]
        [Route("Show/{id}/addEpisode")]
        public ActionResult AddEpisode(int? id)
            {
            // Attempt to get the associated 'Show'
            var show = m.ShowsGetOne(id.GetValueOrDefault());

            if (show == null)
                {
                return HttpNotFound();
                }

            // Create and Configure a "Form" Object
            var formModel = new EpisodeAddFormViewModel();
            formModel.ShowId = show.Id;
            formModel.ShowName = show.Name;

            formModel.GenreList = new SelectList(
                items: m.GenresGetAll(),
                dataValueField: "Name",
                dataTextField: "Name"
                );
            return View(formModel);
            }

        // POST: Show/{id}/AddEpisode
        [HttpPost]
        [Authorize(Roles = "Clerk")]
        [Route("Show/{id}/addEpisode")]
        [ValidateInput(false)]
        public ActionResult AddEpisode(EpisodeAddViewModel newEpisode)
            {
            // Validate the input
            if (!ModelState.IsValid)
                {
                return View(newEpisode);
                }

            // Process the input
            var addedItem = m.EpisodeAdd(newEpisode);

            if (addedItem == null) { return View(addedItem); }

            else
                {
                return RedirectToAction("details", "Episode", new { id = addedItem.Id });
                }
            }

        }
    }
