using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class GenreViewModel
    {
        [DisplayName("Genre Id")]
        public int GENREID { get; set; }

        [DisplayName("Genre")]
        public string GENRE { get; set; }
        
        [DisplayName("Active")]
        public bool ISACTIVE { get; set; }
        
    }

    
}
