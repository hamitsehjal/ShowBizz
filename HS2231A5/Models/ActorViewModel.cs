using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HS2231A5.Models
    {
    public class ActorAddViewModel
        {
        [Required, StringLength(150)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(150)]
        [Display(Name = "Alternate Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height(m)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? Height { get; set; }

        [Required, StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Biography")]
        public string Biography { get; set; }


        }
    public class ActorBaseViewModel : ActorAddViewModel
        {

        public int Id { get; set; }
        [StringLength(250)]
        [Display(Name = "Executive")]
        public string Executive { get; set; }
        }

    public class ActorWithShowInfoViewModel : ActorBaseViewModel
        {
        public ActorWithShowInfoViewModel()
            {
            Shows = new HashSet<ShowBaseViewModel>();
            ActorMediaItems = new HashSet<ActorMediaItemBaseViewModel>();
            Photos = new HashSet<ActorMediaItemBaseViewModel>();
            Documents = new HashSet<ActorMediaItemBaseViewModel>();
            AudioClips = new HashSet<ActorMediaItemBaseViewModel>();
            VideoClips = new HashSet<ActorMediaItemBaseViewModel>();
            }
        public IEnumerable<ShowBaseViewModel> Shows { get; set; }
        [Display(Name = "Appeared On")]

        public int ShowsCount { get; set; }

        public IEnumerable<ActorMediaItemBaseViewModel> ActorMediaItems { get; set; }

        [Display(Name = "Photos")]
        public IEnumerable<ActorMediaItemBaseViewModel> Photos { get; set; }

        [Display(Name = "Documents")]

        public IEnumerable<ActorMediaItemBaseViewModel> Documents { get; set; }

        [Display(Name = "Audio Clips")]

        public IEnumerable<ActorMediaItemBaseViewModel> AudioClips { get; set; }

        [Display(Name = "Video Clips")]

        public IEnumerable<ActorMediaItemBaseViewModel> VideoClips { get; set; }

        }

    public class ActorAddFormViewModel
        {
        [Required, StringLength(150)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(150)]
        [Display(Name = "Alternate Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Please enter a Valid Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height(m)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? Height { get; set; }

        [Required, StringLength(250)]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Biography")]
        public string Biography { get; set; }
        }


    }