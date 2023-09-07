using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace HS2231A5.Data
    {
    public class Actor
        {
        public Actor()
            {
            Shows = new HashSet<Show>();
            ActorMediaItems = new HashSet<ActorMediaItem>();
            }
        public int Id { get; set; }

        // Actor's Full name; For example: "Dwayne Johnson"
        [Required, StringLength(150)]
        public string Name { get; set; }

        // Alternate Name(Stage Name); For example: "Rock"
        [StringLength(150)]
        public string AlternateName { get; set; }

        // `BirthDate` may or may not be known
        public DateTime BirthDate { get; set; }

        // Height (may or may not know); Stored in meters and may contain decimals
        public double Height { get; set; }

        // `ImageURL`
        [Required, StringLength(250)]
        public string ImageUrl { get; set; }

        // `Executive` - username(e.g. executive@example.com)
        [Required, StringLength(250)]
        public string Executive { get; set; }

        // Biography
        public string Biography { get; set; }
        // Navigation Properties
        public ICollection<Show> Shows { get; set; }
        public ICollection<ActorMediaItem> ActorMediaItems { get; set; }


        }
    }