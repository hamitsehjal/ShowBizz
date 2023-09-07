using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace HS2231A5.Data
{
    public class Episode
    {
        public Episode()
        {
            AirDate = DateTime.Now;
            SeasonNumber = 1;
            EpisodeNumber = 1;
        }
        public int Id { get; set; }

        // Name; For example: "Final Smackdown"
        [Required, StringLength(150)]
        public string Name { get; set; }

        // SeasonNumber
        public int SeasonNumber { get; set; }

        // EpisodeNumber
        public int EpisodeNumber { get; set; }

        // Genre
        [Required]
        public string Genre { get; set; }

        // AirDate
        [Required]
        public DateTime AirDate { get; set; }

        // ImageURL - episode cover art
        [Required, StringLength(250)]
        public string ImageUrl { get; set; }

        // Clerk - authenticated User who adds the new Episode
        [Required, StringLength(250)]
        public string Clerk { get; set; }

        // Premise
        public string Premise { get; set; }

        // Video Media Item

        public string VideoContentType { get; set; }
        public byte[] Video { get; set; }

        // ShowId
        public int ShowId { get; set; }

        // Navigation Properties
        [Required]
        public Show Show { get; set; }
    }
}