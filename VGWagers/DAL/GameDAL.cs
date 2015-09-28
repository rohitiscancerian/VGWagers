using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;
using System.Linq;
using VGWagers.Models;

namespace VGWagers.DAL
{
    public class GameDAL
    {
        VGWagersDB dbCon;

        public GameDAL() 
        {
            dbCon = new VGWagersDB();
        }

        public IList<GameViewModel> GetAllGames()
        {
            return dbCon.vgw_game
                .Join(dbCon.vgw_genre_enum, g => g.GENREID, r => r.GENREID, (g, r) => new { g, r })
                .Where(ge => ge.g.ISACTIVE == true)
                .Select(gm => new GameViewModel { 
                                                    GAMEID = gm.g.GAMEID, 
                                                    GAMENAME = gm.g.GAMENAME, 
                                                    GENRE = gm.r.GENRE, 
                                                    GAMEIMAGE = gm.g.GAMEIMAGE, 
                                                    ISACTIVE = gm.g.ISACTIVE 
                                                }
                       )
                .ToList();
        }

    }

    
}
