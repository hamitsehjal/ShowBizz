using AutoMapper;
using HS2231A5.Data;
using HS2231A5.Models;
using Microsoft.Win32;
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
            var actor = m.ActorWithShowInfoGetById(id.GetValueOrDefault());
            if (actor == null)
                return HttpNotFound();

            var photosList = actor.ActorMediaItems
                            .Where(item =>
                                    item.ContentType.Split('/')[1].Equals("png", StringComparison.OrdinalIgnoreCase) ||
                                    item.ContentType.Split('/')[1].Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                                    item.ContentType.Split('/')[1].Equals("jpeg", StringComparison.OrdinalIgnoreCase))
                            .OrderBy(item => item.Caption)
                            .ToList();

            var documentsList = actor.ActorMediaItems
                                .Where(item => item.ContentType.Split('/')[1].Equals("pdf", StringComparison.OrdinalIgnoreCase))
                                .OrderBy(item => item.Caption)
                                .ToList();

            var audioClipsList = actor.ActorMediaItems
                                  .Where(item =>
                                  item.ContentType.Split('/')[1].Equals("mp3", StringComparison.OrdinalIgnoreCase) ||
                                  item.ContentType.Split('/')[1].Equals("mpeg", StringComparison.OrdinalIgnoreCase)
                                  )
                                  .OrderBy(item => item.Caption)
                                  .ToList();

            var videoClipsList = actor.ActorMediaItems
                                  .Where(item => item.ContentType.Split('/')[1].Equals("mp4", StringComparison.OrdinalIgnoreCase))
                                  .OrderBy(item => item.Caption)
                                  .ToList();
            actor.Photos = new List<ActorMediaItemBaseViewModel>(photosList);
            actor.Documents = new List<ActorMediaItemBaseViewModel>(documentsList);
            actor.AudioClips = new List<ActorMediaItemBaseViewModel>(audioClipsList);
            actor.VideoClips = new List<ActorMediaItemBaseViewModel>(videoClipsList);

            return View(actor);
            }

        // GET: Content/{id}
        [Route("Content/{id}")]
        public ActionResult Content(int? id)
            {
            // Attempt to get the matching object
            var obj = m.ActorMediaItemGetById(id.GetValueOrDefault());

            if (obj == null)
                {
                return HttpNotFound();
                }

            else
                {
                return File(obj.Content, obj.ContentType);
                }
            }
        // GET: MediaItem/{id}
        [Route("MediaItemDownload/{id}")]
        public ActionResult MediaItemDownload(int? id)
            {
            // Attempt to get the matching object
            var o = m.ActorMediaItemGetById(id.GetValueOrDefault());

            if (o == null)
                {
                return HttpNotFound();
                }
            else
                {
                // Get file extension, assumes the web server is Microsoft IIS based
                // Must get the extension from the Registry (which is a key-value storage structure for configuration settings, for the Windows operating system and apps that opt to use the Registry)

                // Working variables
                string extension;
                RegistryKey key;
                object value;

                // Open the Registry, attempt to locate the key
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + o.ContentType, false);
                // Attempt to read the value of the key
                value = (key == null) ? null : key.GetValue("Extension", null);
                // Build/create the file extension string
                extension = (value == null) ? string.Empty : value.ToString();

                // Create a new Content-Disposition header
                var cd = new System.Net.Mime.ContentDisposition
                    {
                    // Assemble the file name + extension
                    FileName = $"Doc-{id}{extension}",
                    // Force the media item to be saved (not viewed)
                    Inline = false
                    };
                // Add the header to the response
                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(o.Content, o.ContentType);
                }
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
        [ValidateInput(false)]

        public ActionResult Create(ActorAddViewModel newActor)
            {
            try
                {
                // TODO: Add insert logic here
                // Validate the Input

                if (!ModelState.IsValid)
                    {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    return View(newActor);
                    }

                // Process the input
                var addedItem = m.ActorAdd(newActor);

                if (addedItem == null)
                    return View(newActor);

                return RedirectToAction("Details", new { id = addedItem.Id });
                }
            catch
                {
                return View(m.mapper.Map<ActorAddViewModel, ActorAddFormViewModel>(newActor));
                }
            }

        // GET: Actor/{id}/AddShow
        [HttpGet]
        [Authorize(Roles = "Coordinator")]
        [Route("actor/{id}/addshow")]
        public ActionResult AddShow(int? id)
            {
            // Attempt to get the associated Object
            var actor = m.ActorWithShowInfoGetById(id.GetValueOrDefault());

            if (actor == null)
                return HttpNotFound();

            // Add Show for Known Actor

            // Create and configure a form object
            var formModel = new ShowAddFormViewModel();

            formModel.ActorList = new MultiSelectList(

                items: m.ActorsGetAll(),
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValues: new[] { actor.Id }
                );
            formModel.ActorName = actor.Name;

            formModel.GenreList = new SelectList(m.GenresGetAll(), "Name", "Name");

            return View(formModel);

            }



        // POST: Actor/{id}/AddShow
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        [Route("actor/{id}/addshow")]
        [ValidateInput(false)]
        public ActionResult AddShow(ShowAddViewModel newShow)
            {
            // Validate the input
            if (!ModelState.IsValid)
                {
                return View(newShow);
                }

            // Process the input
            var addedItem = m.ShowAdd(newShow);
            if (addedItem == null)
                {
                return View(newShow);
                }
            else
                {
                // Redirect to Shows Controller
                return RedirectToAction("details", "show", new { id = addedItem.Id });
                }
            }

        // GET: Actor/{id}/AddMediaItem
        [HttpGet]
        [Authorize(Roles = "Executive")]
        [Route("actor/{id}/addMediaItem")]
        public ActionResult AddMediaItem(int? id)
            {
            // Attempt to get the associated object
            var actor = m.ActorWithShowInfoGetById(id.GetValueOrDefault());
            if (actor == null)
                {
                return HttpNotFound();
                }

            // Add media item for Known actor
            var formModel = new ActorMediaItemAddFormViewModel();

            formModel.ActorName = actor.Name;
            formModel.ActorId = actor.Id;

            return View(formModel);
            }

        // POST: Actor/{id}/AddMediaItem
        [HttpPost]
        [Authorize(Roles = "Executive")]
        [Route("actor/{id}/addMediaItem")]
        [ValidateInput(false)]
        public ActionResult AddMediaItem(ActorMediaItemAddViewModel newMediaItem)
            {
            // Validate the input
            if (!ModelState.IsValid)
                {
                return View(newMediaItem);
                }

            // Process the input
            var addedItem = m.ActorMediaItemAdd(newMediaItem);
            if (addedItem == null)
                {
                return View(newMediaItem);
                }
            else
                {
                // Redirect to Actors Controller
                return RedirectToAction("details", "actor", new { id = addedItem.Id });
                }
            }

        }
    }
