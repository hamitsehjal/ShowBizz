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
        // GET: Show
        public ActionResult Index()
            {
            // fetch the collection
            var shows = m.ShowsGetAll();
            return View(shows);
            }

        // GET: Show/Details/5
        public ActionResult Details(int id)
            {
            return View();
            }

        // GET: Show/Create
        public ActionResult Create()
            {
            return View();
            }

        // POST: Show/Create
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

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
            {
            return View();
            }

        // POST: Show/Edit/5
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

        // GET: Show/Delete/5
        public ActionResult Delete(int id)
            {
            return View();
            }

        // POST: Show/Delete/5
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
