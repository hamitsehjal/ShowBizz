using HS2231A5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HS2231A5.Models
    {
    public class EpisodeBaseViewModel
        {

        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; }

        // SeasonNumber
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        // EpisodeNumber
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }

        // Genre
        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        // AirDate
        [Required]
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime AirDate { get; set; }

        // ImageURL - episode cover art
        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        // Clerk - authenticated User who adds the new Episode
        [Required, StringLength(250)]
        [Display(Name = "Clerk")]
        public string Clerk { get; set; }

        // ShowId
        public int ShowId { get; set; }

        }

    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
        {
        [Display(Name = "Show Name")]
        public string ShowName { get; set; }
        }
    }