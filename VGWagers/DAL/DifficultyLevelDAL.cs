using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;
using System.Linq;
using System.Web.Mvc;
using VGWagers.Models;

namespace VGWagers.DAL
{
    public class DifficultyLevelDAL
    {
        VGWagersDB dbCon;

        public DifficultyLevelDAL() 
        {
            dbCon = new VGWagersDB();
        }

        public IList<DifficultyLevelViewModel> GetAllDifficultyLevels()
        {
            return dbCon.vgw_difficulty_level_enum.Select(d => new DifficultyLevelViewModel
                                                {
                                                    DIFFICULTYLEVELID = d.DIFFICULTYLEVELID,
                                                    DIFFICULTYLEVEL = d.DIFFICULTYLEVEL, 
                                                    ISACTIVE = d.ISACTIVE 
                                                }).ToList();
        }   

        public SelectList GetAllActiveDifficultyLevels()
        {
            return new SelectList(dbCon.vgw_difficulty_level_enum.Where(d => d.ISACTIVE == true).ToList(), "DIFFICULTYLEVELID", "DIFFICULTYLEVEL");
        }

        public DifficultyLevelViewModel FindByDifficultyLevelId(int DifficultyLevelId)
        {
            return dbCon.vgw_difficulty_level_enum.Where(d => d.DIFFICULTYLEVELID == DifficultyLevelId).Select(dv => new DifficultyLevelViewModel
                                                                                                                {
                                                                                                                    DIFFICULTYLEVELID = dv.DIFFICULTYLEVELID,
                                                                                                                    DIFFICULTYLEVEL = dv.DIFFICULTYLEVEL,
                                                                                                                    ISACTIVE = dv.ISACTIVE
                                                                                                                }
                                                                                                               ).FirstOrDefault();
        }
    }

    
}
