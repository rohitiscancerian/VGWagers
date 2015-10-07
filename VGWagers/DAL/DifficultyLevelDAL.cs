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
                                                    DIFFICULTYLEVELNAME = d.DIFFICULTYLEVEL, 
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
                                                                                                                    DIFFICULTYLEVELNAME = dv.DIFFICULTYLEVEL,
                                                                                                                    ISACTIVE = dv.ISACTIVE
                                                                                                                }
                                                                                                               ).FirstOrDefault();
        }

        public vgw_difficulty_level_enum GetByDifficultyLevelId(int DifficultyLevelId)
        {
            return dbCon.vgw_difficulty_level_enum.Where(d => d.DIFFICULTYLEVELID == DifficultyLevelId).FirstOrDefault();
        }

        public bool SaveDifficultyLevel(DifficultyLevelViewModel difficultyLevelViewModel, int iUserId)
        {

            if (difficultyLevelViewModel.DIFFICULTYLEVELID > 0)
            {
                vgw_difficulty_level_enum vgwDifficultyLevel = GetByDifficultyLevelId(difficultyLevelViewModel.DIFFICULTYLEVELID);

                vgwDifficultyLevel.DIFFICULTYLEVEL = difficultyLevelViewModel.DIFFICULTYLEVELNAME;
                vgwDifficultyLevel.ISACTIVE = difficultyLevelViewModel.ISACTIVE;
                vgwDifficultyLevel.LASTUPDATEDDATE = DateTime.Now;
                vgwDifficultyLevel.LASTUPDATEDBYUSERID = iUserId;
            }
            else
            {
                vgw_difficulty_level_enum vgwDifficultyLevel = new vgw_difficulty_level_enum();

                vgwDifficultyLevel.DIFFICULTYLEVEL = difficultyLevelViewModel.DIFFICULTYLEVELNAME;
                vgwDifficultyLevel.ISACTIVE = difficultyLevelViewModel.ISACTIVE;
                vgwDifficultyLevel.LASTUPDATEDDATE = DateTime.Now;
                vgwDifficultyLevel.LASTUPDATEDBYUSERID = iUserId;
                dbCon.vgw_difficulty_level_enum.Add(vgwDifficultyLevel);
            }

            int result = dbCon.SaveChanges();
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDifficultyLevel(int DifficultyLevelId, int iUserId)
        {
            bool result = false;
            vgw_difficulty_level_enum vgwDifficultyLevel = GetByDifficultyLevelId(DifficultyLevelId);
            if (vgwDifficultyLevel != null)
            {
                try
                {
                    dbCon.vgw_difficulty_level_enum.Remove(vgwDifficultyLevel);
                    dbCon.SaveChanges();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }
    }

    
}
