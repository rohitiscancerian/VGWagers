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
        //private MultiSelectList availableOnPlatforms = new MultiSelectList(new List<PlatformViewModel>(), "PLATFORMID", "PLATFORMNAME");
        //private MultiSelectList difficultyLevels = new MultiSelectList(new List<DifficultyLevelViewModel>(), "DIFFICULTYLEVELID", "DIFFICULTYLEVELNAME");

        [DisplayName("Game Id")]
        public int GAMEID { get; set; }

        [DisplayName("Name")] 
        public string GAMENAME { get; set; }
        
        //public byte[] GAMEIMAGE { get; set; }

        public GameImageModel GAMEIMAGE { get; set; }
        
        public int GENREID { get; set; }

        [DisplayName("Genre")] 
        public string GENRE { get; set; }

        [DisplayName("Active?")]
        public bool ISACTIVE { get; set; }
    
        [DisplayName("Can be played by teams?")]
        public bool CANBEPLAYEDBYTEAM { get; set; }

        [DisplayName("Position on Home Page caraousel")]
        [Range(0, 15)]
        [DefaultValue(0)]
        public int SORTORDER { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RELEASEDATE { get; set; }

        [DisplayName("Available on Platforms")]
        public string[] AVAILABLEONPLATFORMS { get; set; }

        [DisplayName("Difficulty Levels")]
        public string[] DIFFICULTYLEVELS { get; set; }

        public IEnumerable<int> SELECTEDPLATFORMS { get; set; }
        public List<PlatformViewModel> PLATFORMS { get; set; }

        public IEnumerable<int> SELECTEDLEVELS { get; set; }
        public List<DifficultyLevelViewModel> LEVELS { get; set; }


        public SelectList ACTIVEGENRES { get; set; }


    }

    
}
