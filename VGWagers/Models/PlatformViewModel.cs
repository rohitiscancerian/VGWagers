using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class PlatformViewModel
    {
         [DisplayName("Platform Id")]
        public int PLATFORMID { get; set; }

        [DisplayName("Platform")]
        public string PLATFORMNAME { get; set; }

        [DisplayName("Active")] 
        public bool ISACTIVE { get; set; }
        
    }

    
}
