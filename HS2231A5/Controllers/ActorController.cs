using HS2231A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    public class ActorController : Controller
        {
        private Manager m = new Manager();
        // GET: Actor
        public ActionResult Index()
            {
            // fetch the collection
            var actors = m.ActorsGetAll();

            return View(actors);
            }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
            {
            var actor = m.ActorGetOne(id.GetValueOrDefault());
            if (actor == null)
                return HttpNotFound();
            return View(actor);
            }

        // GET: Actor/Create
        [Authorize(Roles = "Executive")]
        [HttpGet]
        public ActionResult Create()
            {
            // Configure a "ActorAddFormViewModel" object
            var form = new ActorAddFormViewModel();
            return View(form);
            }

        // POST: Actor/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ActorAddViewModel newActor)
            {
            try
                {
                // TODO: Add insert logic here
                // Validate the Input

                if (!ModelState.IsValid)
                    {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    // Configure a "ActorAddFormViewModel" object
                    var form = new ActorAddFormViewModel();
                    return View(form);
                    }

                // Process the input
                var addedItem = m.ActorAdd(newActor);

                if (addedItem == null)
                    return View(newActor);

                return RedirectToAction("Details", new { id = addedItem.Id });
                }
            catch
                {
                var form = new ActorAddFormViewModel();
                return View(form);
                }
            }

        // GET: Actor/Edit/5
        public ActionResult Edit(int id)
            {
            return View();
            }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
            {
            try
                {
                // TODO: Add update logic here

                return RedirectToAction("Index");
                }
            catch
                {
                return View();
                }
            }

        // GET: Actor/Delete/5
        public ActionResult Delete(int id)
            {
            return View();
            }

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
            {
            try
                {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
                }
            catch
                {
                return View();
                }
            }
        }
    }
