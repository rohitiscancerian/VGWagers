using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;
using System.Web.Mvc;

namespace VGWagers.Models
{
    public class GameViewModel
    {
        [DisplayName("Game Id")]
        public int GAMEID { get; set; }

        [DisplayName("Name")] 
        public string GAMENAME { get; set; }
        
        public byte[] GAMEIMAGE { get; set; }
        
        public int GENREID { get; set; }

        [DisplayName("Genre")] 
        public string GENRE { get; set; }

        [DisplayName("Active")]
        public bool ISACTIVE { get; set; }

        [DisplayName("Release Date")]
        public DateTime RELEASEDATE { get; set; }

        [DisplayName("Available on Platforms")]
        public SelectList AVAILABLEONPLATFORMS { get; set; }

        [DisplayName("Difficulty Levels")]
        public SelectList DIIFICULTYLEVELS { get; set; }
    }

    
}
