using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HS2231A5.Models
    {

    public class ActorMediaItemAddFormViewModel
        {

        // ActorId
        [Range(1, Int32.MaxValue)]
        public int ActorId { get; set; }

        // Name of the associated Show
        [Display(Name = "Actor Name")]
        public string ActorName { get; set; }


        [Required, StringLength(100)]
        public string Caption { get; set; }


        [Required]
        [Display(Name = "Attachment")]
        [DataType(DataType.Upload)]
        public string ContentUpload { get; set; }
        }
    public class ActorMediaItemAddViewModel
        {
        //ActorId
        [Range(1, Int32.MaxValue)]
        public int ActorId { get; set; }

        [Required, StringLength(100)]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFileBase ContentUpload { get; set; }
        }

    public class ActorMediaItemBaseViewModel
        {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string ContentType { get; set; }

        }

    public class ActorMediaItemWithContentViewModel : ActorMediaItemBaseViewModel
        {
        public byte[] Content { get; set; }
        }


    }