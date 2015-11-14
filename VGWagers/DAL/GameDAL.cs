using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;
using System.Linq;
using VGWagers.Models;
using System.Web.Mvc;

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
                .Select(gm => new GameViewModel
                {
                    GAMEID = gm.g.GAMEID,
                    GAMENAME = gm.g.GAMENAME,
                    GENRE = gm.r.GENRE,
                    ISACTIVE = gm.g.ISACTIVE,
                    RELEASEDATE = gm.g.RELEASEDATE                    
                }
                       )
                .ToList();
        }

        public GameViewModel GetGameDetails(int GameId)
        {
            
            GameViewModel gameViewModel = dbCon.vgw_game
                                                .Join(dbCon.vgw_genre_enum, g => g.GENREID, r => r.GENREID, (g, r) => new { g, r })
                                                .Where(gg => gg.g.GAMEID == GameId).Select(gv => new GameViewModel
                                                                            {
                                                                                GAMEID = gv.g.GAMEID,
                                                                                GAMENAME = gv.g.GAMENAME,
                                                                                GAMEIMAGE = new GameImageModel 
                                                                                                {
                                                                                                    GAMEIMAGEBINARY = gv.g.GAMEIMAGE
                                                                                                },
                                                                                ISACTIVE = gv.g.ISACTIVE,
                                                                                RELEASEDATE = gv.g.RELEASEDATE,
                                                                                GENREID = gv.g.GENREID,
                                                                                GENRE = gv.r.GENRE,
                                                                                PLATFORMS = dbCon.vgw_platform_enum
                                                                                       .Join(dbCon.vgw_game_platform_xref, p => p.PLATFORMID, gp => gp.PLATFORMID, (p, gp) => new { p, gp })
                                                                                       .Where(gpl => gpl.gp.GAMEID == gv.g.GAMEID)
                                                                                       .Select(
                                                                                                gpl => new PlatformViewModel
                                                                                                {
                                                                                                    PLATFORMID = gpl.p.PLATFORMID,
                                                                                                    PLATFORMNAME = gpl.p.PLATFORMNAME,
                                                                                                    ISACTIVE = gpl.p.ISACTIVE
                                                                                                }
                                                                                             ).ToList(),
                                                                                LEVELS = dbCon.vgw_difficulty_level_enum
                                                                                        .Join(dbCon.vgw_game_difficulty_level_xref, d => d.DIFFICULTYLEVELID, gd => gd.DIFFICULTYLEVELID, (d, gd) => new { d, gd })
                                                                                        .Where(ggd => ggd.gd.GAMEID == gv.g.GAMEID)
                                                                                        .Select(
                                                                                                    ggd => new DifficultyLevelViewModel
                                                                                                    {
                                                                                                        DIFFICULTYLEVELID = ggd.d.DIFFICULTYLEVELID,
                                                                                                        DIFFICULTYLEVELNAME = ggd.d.DIFFICULTYLEVEL
                                                                                                    }
                                                                                               )
                                                                                        .ToList()
                                                                            }
                                                                        ).FirstOrDefault();
            
           
            
            return gameViewModel;
        }


        public vgw_game GetByGameId(int GameId)
        {
            return dbCon.vgw_game.Where(g => g.GAMEID == GameId).FirstOrDefault();
        }

        public byte[] GetGameImage(int GameId)
        {
            return dbCon.vgw_game.Where(g => g.GAMEID == GameId).Select(g => g.GAMEIMAGE).FirstOrDefault();
        }

        public string[] GetGamePlatforms(int GameId)
        {
            return dbCon.vgw_platform_enum
                   .Join(dbCon.vgw_game_platform_xref, p => p.PLATFORMID, gp => gp.PLATFORMID, (p, gp) => new { p, gp })
                   .Where(gp => gp.gp.GAMEID == GameId)
                   .Select(gp => gp.p.PLATFORMNAME)
                   .ToArray();
        }

        public string[] GetGameDifficultyLevels(int GameId)
        {
            return dbCon.vgw_difficulty_level_enum
                   .Join(dbCon.vgw_game_difficulty_level_xref, d => d.DIFFICULTYLEVELID, gd => gd.DIFFICULTYLEVELID, (d, gd) => new { d, gd })
                   .Where(gd => gd.gd.GAMEID == GameId)
                   .Select(gd => gd.d.DIFFICULTYLEVEL)
                   .ToArray();
        }

        private ICollection<vgw_game_difficulty_level_xref> getGameDifficultyLevelXref(int GameId)
        {
            return dbCon.vgw_game_difficulty_level_xref
                       .Where(gd => gd.GAMEID == GameId)
                       .ToList();
        }

        private ICollection<vgw_game_platform_xref> getGamePlatformXref(int GameId)
        {
            return dbCon.vgw_game_platform_xref
                       .Where(gp => gp.GAMEID == GameId)
                       .ToList();
        }

        public bool SaveGame(GameViewModel gameViewModel, int iUserId)
        {

            if (gameViewModel.GAMEID > 0)
            {
                vgw_game vgwGame = GetByGameId(gameViewModel.GAMEID);
                if (!prepareVGWGameContext(ref vgwGame, gameViewModel, iUserId))
                {
                    return false;
                }                
            }
            else
            {
                vgw_game vgwGame = new vgw_game();
                if (prepareVGWGameContext(ref vgwGame, gameViewModel, iUserId))
                {
                    dbCon.vgw_game.Add(vgwGame);
                }
                else
                {
                    return false;
                }
                
            }

            int result = dbCon.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool prepareVGWGameContext(ref vgw_game vgwGame, GameViewModel gameViewModel, int iUserId)
        {
            try
            {
                
                ICollection<vgw_game_platform_xref> availableOnPlatforms;
                ICollection<vgw_game_difficulty_level_xref> difficultyLevels;

                if (vgwGame.GAMEID > 0)
                {
                    availableOnPlatforms = getGamePlatformXref(vgwGame.GAMEID);
                    difficultyLevels = getGameDifficultyLevelXref(vgwGame.GAMEID);
                }
                else
                {
                    availableOnPlatforms = new List<vgw_game_platform_xref>();
                    difficultyLevels = new List<vgw_game_difficulty_level_xref>();
                }
                
                vgwGame.GAMENAME = gameViewModel.GAMENAME;
                vgwGame.GENREID = gameViewModel.GENREID;
                if (gameViewModel.GAMEIMAGE != null && gameViewModel.GAMEIMAGE.GAMEIMAGEBINARY != null)
                {
                    vgwGame.GAMEIMAGE = gameViewModel.GAMEIMAGE.GAMEIMAGEBINARY;
                }
                vgwGame.ISACTIVE = gameViewModel.ISACTIVE;
                vgwGame.RELEASEDATE = gameViewModel.RELEASEDATE;
                vgwGame.LASTUPDATEDDATE = DateTime.Now;
                vgwGame.LASTUPDATEDBYUSERID = iUserId;

                int iPlatformId;
                if (gameViewModel.SELECTEDPLATFORMS == null)
                {
                }
                else
                {
                    foreach (int platformID in gameViewModel.SELECTEDPLATFORMS)
                    {
                        iPlatformId = platformID;
                        if (availableOnPlatforms.Where(p => p.PLATFORMID == iPlatformId).Count() == 0)
                        {
                            vgw_game_platform_xref platformGameXref = new vgw_game_platform_xref();
                            platformGameXref.GAMEID = vgwGame.GAMEID;
                            platformGameXref.ISACTIVE = true;
                            platformGameXref.LASTUPDATEDDATE = DateTime.Now;
                            platformGameXref.LASTUPDATEDUSERID = iUserId;
                            platformGameXref.PLATFORMID = iPlatformId;
                            availableOnPlatforms.Add(platformGameXref);
                        }
                    }
                }

                int iDifficultyLevelId;
                int iDifficultyLevelSortOrder = 1;
                if (gameViewModel.SELECTEDLEVELS == null)
                {
                }
                else
                {
                    foreach (int difficultyLevelId in gameViewModel.SELECTEDLEVELS)
                    {
                        iDifficultyLevelId = difficultyLevelId;
                        if (difficultyLevels.Where(d => d.DIFFICULTYLEVELID == iDifficultyLevelId).Count() == 0)
                        {
                            vgw_game_difficulty_level_xref gameDifficultyLevelXref = new vgw_game_difficulty_level_xref();
                            gameDifficultyLevelXref.DIFFICULTYLEVELID = iDifficultyLevelId;
                            gameDifficultyLevelXref.GAMEID = vgwGame.GAMEID;
                            gameDifficultyLevelXref.LASTUPDATEDBYUSERID = iUserId;
                            gameDifficultyLevelXref.LASTUPDATEDDATE = DateTime.Now;
                            gameDifficultyLevelXref.SORTORDER = iDifficultyLevelSortOrder;
                            iDifficultyLevelSortOrder++;
                            difficultyLevels.Add(gameDifficultyLevelXref);
                        }
                    }
                }

                vgwGame.AVAILABLEONPLATFORMS = availableOnPlatforms;
                vgwGame.DIFFICULTYLEVELS = difficultyLevels;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }

    
}
