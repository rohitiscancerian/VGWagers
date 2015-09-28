using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

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
        
        public string GENRE { get; set; }

        [DisplayName("Active")]
        public bool ISACTIVE { get; set; }

        [DisplayName("Available on Platforms")]
        public IList<vgw_platform> AVAILABLEONPLATFORMS { get; set; }

        [DisplayName("Difficulty Levels")] 
        public IList<vgw_difficulty_level_enum> DIIFICULTYLEVELS { get; set; }
    }

    
}
