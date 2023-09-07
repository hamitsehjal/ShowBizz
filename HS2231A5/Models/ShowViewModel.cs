using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HS2231A5.Models
    {
    public class ShowBaseViewModel
        {

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        // 'unassociated' genre name
        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ReleaseDate { get; set; }

        // ImageURL - TV series Cover art
        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        // `Coordinator` - username(e.g. executive@example.com)
        [Required, StringLength(250)]
        [Display(Name = "Coordinator")]
        public string Coordinator { get; set; }

        }

    public class ShowWithInfoViewModel : ShowBaseViewModel
        {
        public ShowWithInfoViewModel()
            {
            Actors = new HashSet<ActorBaseViewModel>();
            Episodes = new HashSet<EpisodeBaseViewModel>();
            }
        public IEnumerable<ActorBaseViewModel> Actors { get; set; }
        public IEnumerable<EpisodeBaseViewModel> Episodes { get; set; }

        [Display(Name = "Cast")]
        public int ActorsCount { get; set; }

        [Display(Name = "Episodes")]
        public int EpisodesCount { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Premise")]
        public string Premise { get; set; }
        }
    public class ShowAddFormViewModel : ShowAddViewModel
        {
        // SelectList for Genre
        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }


        // SelectList for the associated Item
        [Display(Name = "Actors")]
        public MultiSelectList ActorList { get; set; }

        // Display the name of the associated item
        public string ActorName { get; set; }
        }

    public class ShowAddViewModel
        {
        public ShowAddViewModel()
            {
            ActorIds = new List<int>();
            ReleaseDate = DateTime.Today;
            }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]


        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name="Premise")]
        public string Premise { get; set; }

        // Identifier for associated Item
        public IEnumerable<int> ActorIds { get; set; }

        }

    }