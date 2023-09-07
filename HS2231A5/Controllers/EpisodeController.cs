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
        public ActionResult Details(int? id)
            {
            // fetch the object
            var episode=m.EpisodeWithDetailById(id.GetValueOrDefault());

            if (episode == null)
                return HttpNotFound();

            return View(episode);
            }


        // GET: Video/{id}
        [Route("Video/{id}")]
        public ActionResult Video(int? id)
            {
            // Attempt to get the matching object
            var obj=m.EpisodeVideoGetById(id.GetValueOrDefault());

            if (obj == null)
                {
                return HttpNotFound();
                }

            else
                {
                return File(obj.Video,obj.VideoContentType);
                }
            }



        }
    }
