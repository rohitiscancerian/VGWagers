using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class LookupViewModel
    {
        public IList<GameViewModel> GamesList { get; set; }

        public GameViewModel Game { get; set; }

        public IList<GenreViewModel> GenreList { get; set; }

        public GenreViewModel Genre { get; set; }

        public IList<DifficultyLevelViewModel> DifficultyLevelList { get; set; }

        public DifficultyLevelViewModel DifficultyLevel { get; set; }

        public IList<PlatformViewModel> PlatformList { get; set; }

        public PlatformViewModel Platform { get; set; }
    }

    
}
