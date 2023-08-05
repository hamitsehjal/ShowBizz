using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Controllers
    {
    public class EpisodeController : Controller
        {
        private Manager m = new Manager();
        // GET: Episode
        public ActionResult Index()
            {
            //fetch the collection
            var episodes = m.EpisodesWithDetailGetAll();
            return View(episodes);
            }

        // GET: Episode/Details/5
        public ActionResult Details(int id)
            {
            return View();
            }

        // GET: Episode/Create
        public ActionResult Create()
            {
            return View();
            }

        // POST: Episode/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
            {
            try
                {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
                }
            catch
                {
                return View();
                }
            }

        // GET: Episode/Edit/5
        public ActionResult Edit(int id)
            {
            return View();
            }

        // POST: Episode/Edit/5
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

        // GET: Episode/Delete/5
        public ActionResult Delete(int id)
            {
            return View();
            }

        // POST: Episode/Delete/5
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
