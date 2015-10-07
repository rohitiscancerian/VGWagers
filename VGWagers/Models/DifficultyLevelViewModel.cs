using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class DifficultyLevelViewModel
    {
        [DisplayName("Difficulty Level Id")] 
        public int DIFFICULTYLEVELID { get; set; }
        
        [DisplayName("Difficulty Level")] 
        public string DIFFICULTYLEVELNAME { get; set; }

        [DisplayName("Active")]
        public bool ISACTIVE { get; set; }
        
    }

    
}
