using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;
using System.Web.Mvc;

namespace VGWagers.Models
{
    public class GamePlatformViewModel
    {        
        [DisplayName("Game Id")]
        public int GAMEID { get; set; }

        public int PLATFORMID { get; set; }

        [DisplayName("Active")]
        public bool ISACTIVE { get; set; }
        
    }

    
}
