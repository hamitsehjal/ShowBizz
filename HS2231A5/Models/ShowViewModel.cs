using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }