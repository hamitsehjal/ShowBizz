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
            }
        public IEnumerable<ShowBaseViewModel> Shows { get; set; }
        [Display(Name="Appeared On")]
   
        public int ShowsCount { get; set; }
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
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height(m)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? Height { get; set; }

        [Required, StringLength(250)]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        }


    }