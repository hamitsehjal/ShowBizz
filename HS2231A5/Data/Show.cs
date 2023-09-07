using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace HS2231A5.Data
{
    public class Show
    {
        public Show()
        {
            ReleaseDate = DateTime.Now;
            Actors = new HashSet<Actor>();
            Episodes = new HashSet<Episode>();
        }
        public int Id { get; set; }

        // Name of the show; For example("WWE Smackdown")
        [Required, StringLength(150)]
        public string Name { get; set; }

        // 'unassociated' genre name
        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        // ImageURL - TV series Cover art
        [Required, StringLength(250)]
        public string ImageUrl { get; set; }

        // `Coordinator` - username(e.g. executive@example.com)
        [Required, StringLength(250)]
        public string Coordinator { get; set; }

        // Premise
        public string Premise { get; set; }


        // Navigation Properties
        public ICollection<Actor> Actors { get; set; }

        public ICollection<Episode> Episodes { get; set; }

    }

}