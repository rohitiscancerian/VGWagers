using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class GameImageModel
    {
        //[Required(ErrorMessage = "Please select an image")]
        [Display(Name = "Upload Game Image")]
        [ValidateFile]
        public HttpPostedFileBase GAMEIMAGE { get; set; }

        public byte[] GAMEIMAGEBINARY { get; set; }

        
    }
}