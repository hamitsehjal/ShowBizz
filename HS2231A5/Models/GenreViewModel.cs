using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HS2231A5.Models
    {
    public class GenreBaseViewModel
        {
        public int Id { get; set; }

        // Name of the Genre; For example: "Comedy"
        [Required, StringLength(50)]
        [Display(Name="Name")]
        public string Name { get; set; }
        }
    }