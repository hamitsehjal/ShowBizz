using AutoMapper;
using HS2231A5.Data;
using HS2231A5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V2 == 2231-a91f30b1-10b6-412e-985e-1a8325313f50
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace HS2231A5.Controllers
    {
    public class Manager
        {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
            {
            get
                {
                // On first use, it will be null, so set its value
                if (_user == null)
                    {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                    }
                return _user;
                }
            }

        // Default constructor...
        public Manager()
            {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<Actor, ActorBaseViewModel>();
                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>();
                cfg.CreateMap<ActorAddViewModel, Actor>();
                cfg.CreateMap<Show, ShowBaseViewModel>();
                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
            }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        public IEnumerable<GenreBaseViewModel> GenresGetAll()
            {
            var results = ds.Genres.OrderBy(genre => genre.Name);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(results);
            }

        // Actor GetAll
        public IEnumerable<ActorBaseViewModel> ActorsGetAll()
            {
            var results = ds.Actors.OrderBy(actor => actor.Name);
            return mapper.Map<IEnumerable<Actor>, IEnumerable<ActorBaseViewModel>>(results);
            }

        // Actor GetOne
        public ActorWithShowInfoViewModel ActorGetOne(int id)
            {
            var result = ds.Actors
                        .Include("Shows")
                        .SingleOrDefault(a => a.Id == id);

            return result == null ? null : mapper.Map<Actor, ActorWithShowInfoViewModel>(result);
            }

        // Actor Add New
        public ActorWithShowInfoViewModel ActorAdd(ActorAddViewModel newActor)
            {
            // User name of the authenticated user
            var user = HttpContext.Current.User.Identity.Name;
           // newActor
            // Attempt to add the new Actor
            var addedItem = ds.Actors.Add(mapper.Map<ActorAddViewModel, Actor>(newActor));

            // Set the executive property
            addedItem.Executive = user;

            ds.SaveChanges();

            // If successful, return the added Item

            return addedItem == null ? null : mapper.Map<Actor, ActorWithShowInfoViewModel>(addedItem);

            }
        public IEnumerable<ShowBaseViewModel> ShowsGetAll()
            {
            var results = ds.Shows.OrderBy(show => show.Name);
            return mapper.Map<IEnumerable<Show>, IEnumerable<ShowBaseViewModel>>(results);
            }

        public IEnumerable<EpisodeWithShowNameViewModel> EpisodesWithDetailGetAll()
            {
            var results = ds.Episodes
                .Include("Show")
                .OrderBy(episode => episode.Show.Name)
                .ThenBy(episode => episode.SeasonNumber)
                .ThenBy(episode => episode.EpisodeNumber);
            return mapper.Map<IEnumerable<Episode>, IEnumerable<EpisodeWithShowNameViewModel>>(results);
            }


        // *** Add your methods above this line **


        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
            {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
            }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // You can write one method or many methods but remember to
        // check for existing data first.  You will call this/these method(s)
        // from a controller action.

        // LoadRoles()
        public bool LoadRoles()
            {
            // Return if there's existing data
            if (ds.RoleClaims.Count() > 0)
                {
                return false;
                }

            // Otherwise, add data

            ds.RoleClaims.Add(new RoleClaim { Name = "Admin" });
            ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
            ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
            ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });

            // save changes
            ds.SaveChanges();
            return true;

            }

        // LoadGenres()
        public bool LoadGenres()
            {

            // Return if existing data
            if (ds.Genres.Count() > 0)
                {
                return false;
                }

            // Add Genre
            ds.Genres.Add(new Genre { Name = "Action" });
            ds.Genres.Add(new Genre { Name = "Anime" });
            ds.Genres.Add(new Genre { Name = "British" });
            ds.Genres.Add(new Genre { Name = "Canadian" });
            ds.Genres.Add(new Genre { Name = "Classic" });
            ds.Genres.Add(new Genre { Name = "Comedies" });
            ds.Genres.Add(new Genre { Name = "Crime" });
            ds.Genres.Add(new Genre { Name = "Dramas" });
            ds.Genres.Add(new Genre { Name = "Docuseries" });
            ds.Genres.Add(new Genre { Name = "Horror" });

            ds.SaveChanges();
            return true;
            }
        // LoadActors()
        public bool LoadActors()
            {
            // User name of the authenticated user
            var user = HttpContext.Current.User.Identity.Name;

            if (ds.Actors.Count() > 0)
                {
                return false;
                }
            // Add actors
            ds.Actors.Add(new Actor
                {
                Name = "Cillian Murphy",
                BirthDate = DateTime.ParseExact("05/25/1976", "MM/d/yyyy", CultureInfo.InvariantCulture),
                Height = 1.72,
                ImageUrl = "https://en.wikipedia.org/wiki/Cillian_Murphy#/media/File:Cillian_Murphy-2014.jpg",
                Executive = user
                });
            ds.Actors.Add(new Actor
                {
                Name = "Gabriel Macht",
                BirthDate = DateTime.ParseExact("01/22/1972", "MM/d/yyyy", CultureInfo.InvariantCulture),
                Height = 1.84,
                ImageUrl = "https://en.wikipedia.org/wiki/Gabriel_Macht#/media/File:Gabriel_Macht_3241.jpg",
                Executive = user
                });
            ds.Actors.Add(new Actor
                {
                Name = "Leonardo DiCaprio",
                AlternateName = "Leo",
                BirthDate = DateTime.ParseExact("11/11/1974", "MM/d/yyyy", CultureInfo.InvariantCulture),
                Height = 1.83,
                ImageUrl = "https://en.wikipedia.org/wiki/File:Leonardo_DiCaprio_2017.jpg",
                Executive = user
                });

            ds.SaveChanges();
            return true;
            }
        // LoadShows()
        public bool LoadShows()
            {
            // User name of the Authenticated User
            var user = HttpContext.Current.User.Identity.Name;
            // Return if there's existing data
            if (ds.Shows.Count() > 0)
                {
                return false;
                }
            // Otherwise add shows

            // Cillian Murphy
            // Fetch the Actor object
            var actor1 = ds.Actors.SingleOrDefault(a => a.Name == "Cillian Murphy");
            // Gabriel Macht
            // Fetch the Actor object
            var actor2 = ds.Actors.SingleOrDefault(actor => actor.Name == "Gabriel Macht");
            if (actor1 == null || actor2 == null) { return false; }
            // Continue
            ds.Shows.Add(new Show
                {
                Actors = new Actor[] { actor1 },
                Name = "Peaky Blinders",
                Genre = "Docuseries",
                ReleaseDate = DateTime.ParseExact("09/30/2014", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://en.wikipedia.org/wiki/Peaky_Blinders_(TV_series)#/media/File:Peaky_Blinders_titlecard.jpg",
                Coordinator = user

                });

            ds.Shows.Add(new Show
                {
                Actors = new Actor[] { actor2 },
                Name = "Suits",
                Genre = "Dramas",
                ReleaseDate = DateTime.ParseExact("03/13/2011", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://en.wikipedia.org/wiki/Suits_(American_TV_series)#/media/File:Title_card_for_the_US_TV_show_Suits.png",
                Coordinator = user

                });
            ds.SaveChanges();
            return true;
            }

        // LoadEpisodes()
        public bool LoadEpisodes()
            {
            var user = HttpContext.Current.User.Identity.Name;
            // Return if there's existing data
            if (ds.Episodes.Count() > 0) { return false; }

            // Add Episode's data
            var show1 = ds.Shows.SingleOrDefault(s => s.Name == "Peaky Blinders");
            var show2 = ds.Shows.SingleOrDefault(s => s.Name == "Suits");

            if (show1 == null || show2 == null) { return false; }

            // Continue

            // Peaky Blinders - Season 1; Episode 1
            ds.Episodes.Add(new Episode
                {
                Show = show1,
                ShowId = show1.Id,
                SeasonNumber = 1,
                Name = "Rise of the Shelby Gang",
                EpisodeNumber = 1,
                Genre = "Docuseries",
                AirDate = DateTime.ParseExact("09/12/2013", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://tinyurl.com/peakyEpisode1",
                Clerk = user
                });

            // Peaky Blinders - Season 1; Episode 2
            ds.Episodes.Add(new Episode
                {
                Show = show1,
                ShowId = show1.Id,
                SeasonNumber = 1,
                EpisodeNumber = 2,
                Genre = "Docuseries",
                Name = "The Dark Streets of Birmingham",
                AirDate = DateTime.ParseExact("09/19/2013", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://tinyurl.com/peakyEpisode2",
                Clerk = user
                });

            // Peaky Blinders - Season 2; Episode 1
            ds.Episodes.Add(new Episode
                {
                Show = show1,
                ShowId = show1.Id,
                SeasonNumber = 2,
                EpisodeNumber = 1,
                Genre = "Docuseries",
                Name = "Blood and Brotherhood",
                AirDate = DateTime.ParseExact("03/29/2014", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://tinyurl.com/peakyEpisode3",
                Clerk = user
                });


            // Suits - Season 1; Episode 1
            ds.Episodes.Add(new Episode
                {
                Show = show2,
                ShowId = show2.Id,
                SeasonNumber = 1,
                EpisodeNumber = 1,
                Genre = "Dramas",
                Name = "Pilot",
                AirDate = DateTime.ParseExact("06/23/2011", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://www.imdb.com/title/tt1632701/mediaviewer/rm3134549248?ref_=ttmi_mi_all_sf_2",
                Clerk = user
                });


            // Suits - Season 2; Episode 1
            ds.Episodes.Add(new Episode
                {
                Show = show2,
                ShowId = show2.Id,
                SeasonNumber = 2,
                EpisodeNumber = 1,
                Genre = "Dramas",
                Name = "Errors and Omissions",
                AirDate = DateTime.ParseExact("07/31/2011", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://tinyurl.com/suitEpisode2",
                Clerk = user
                });

            // Suits - Season 2; Episode 2
            ds.Episodes.Add(new Episode
                {
                Show = show2,
                ShowId = show2.Id,
                SeasonNumber = 2,
                EpisodeNumber = 2,
                Genre = "Dramas",
                Name = "Inside Track",
                AirDate = DateTime.ParseExact("08/17/2011", "MM/d/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://tinyurl.com/suitEpisode3",
                Clerk = user
                });

            ds.SaveChanges();
            return true;
            }

        //public bool LoadData()
        //    {
        //    // User name
        //    var user = HttpContext.Current.User.Identity.Name;

        //    // Monitor the progress
        //    bool done = false;

        //    // *** Role claims ***
        //    if (ds.RoleClaims.Count() == 0)
        //        {
        //        // Add role claims here

        //        //ds.SaveChanges();
        //        //done = true;
        //        }

        //    return done;
        //    }

        public bool RemoveData()
            {
            try
                {
                foreach (var e in ds.Episodes)
                    {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                    }
                foreach (var e in ds.Shows)
                    {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                    }
                foreach (var e in ds.Actors)
                    {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                    }
                foreach (var e in ds.Genres)
                    {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                    }
                foreach (var e in ds.RoleClaims)
                    {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                    }
                ds.SaveChanges();

                return true;
                }
            catch (Exception)
                {
                return false;
                }
            }

        public bool RemoveDatabase()
            {
            try
                {
                return ds.Database.Delete();
                }
            catch (Exception)
                {
                return false;
                }
            }

        }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
        {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
            {
            if (HttpContext.Current.Request.IsAuthenticated)
                {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
                }
            else
                {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
                }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
            }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
            {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
            }

        public bool HasClaim(string type, string value)
            {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
            }
        }

    #endregion

    }