using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace HS2231A5.Data
{
    public class Genre
    {
        public int Id { get; set; }

        // Name of the Genre; For example: "Comedy"
        [Required, StringLength(50)]
        public string Name { get; set; }

    }
}