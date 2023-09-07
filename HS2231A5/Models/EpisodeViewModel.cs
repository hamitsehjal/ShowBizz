using HS2231A5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //ShowId
        public int ShowId { get; set; }

        

        }

    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
        {
        [Display(Name = "Show Name")]
        public string ShowName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Premise")]
        public string Premise { get; set; }

        [Display(Name = "Episode Video")]

        public string VideoContentType { get; set; }

        public string VideoUrl
            {
            get
                {
                return $"/Episode/Video/{Id}";
                }
            }
        }

    public class EpisodeAddFormViewModel
        {
        public EpisodeAddFormViewModel()
            {
            AirDate = DateTime.Today;
            SeasonNumber = 1;
            EpisodeNumber = 1;
            }

        [Required, StringLength(150)]
        public string Name { get; set; }

        // SeasonNumber
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        // EpisodeNumber
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }


        // AirDate
        [Required]
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime AirDate { get; set; }

        // ImageURL - episode cover art
        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Premise")]
        public string Premise { get; set; }
        ////ShowId
        [Range(1, Int32.MaxValue)]
        public int ShowId { get; set; }

        // SelectList for Genre
        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }


        // Name of the associated Show
        [Display(Name = "Show Name")]
        public string ShowName { get; set; }


        [Required]
        [Display(Name = "Episode Video")]
        [DataType(DataType.Upload)]
        public string VideoUpload { get; set; }
        }
    public class EpisodeAddViewModel
        {
        public EpisodeAddViewModel()
            {
            AirDate = DateTime.Today;
            SeasonNumber = 1;
            EpisodeNumber = 1;
            }
        [Required, StringLength(150)]
        public string Name { get; set; }

        // SeasonNumber
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        // EpisodeNumber
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }


        // AirDate
        [Required]
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime AirDate { get; set; }

        // ImageURL - episode cover art
        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name="Premise")]
        public string Premise { get; set; }

        // Genre
        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        ////ShowId
        [Range(1, Int32.MaxValue)]
        public int ShowId { get; set; }

        [Required]
        public HttpPostedFileBase VideoUpload { get; set; }
        }

    public class EpisodeVideoViewModel { 
        
        public int Id { get; set; }
        public string VideoContentType { get; set; }
        public byte[] Video { get; set; }
        }

    }