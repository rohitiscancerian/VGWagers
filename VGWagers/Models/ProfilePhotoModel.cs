using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class ProfilePhotoModel
    {
        [Required(ErrorMessage = "Please select a photo")]
        [Display(Name = "Upload Photo")]
        [ValidateFile]
        public HttpPostedFileBase PROFILEPHOTO { get; set; }

        public byte[] PHOTOBINARY { get; set; }

        
    }
}