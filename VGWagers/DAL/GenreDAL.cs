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
    public class GenreDAL
    {
        VGWagersDB dbCon;

        public GenreDAL() 
        {
            dbCon = new VGWagersDB();
        }

        public IList<GenreViewModel> GetAllGenre()
        {
            return dbCon.vgw_genre_enum.Select(g => new GenreViewModel { 
                                                    GENREID = g.GENREID, 
                                                    GENRENAME = g.GENRE, 
                                                    ISACTIVE = g.ISACTIVE 
                                                }).ToList();
        }   

        public SelectList GetAllActiveGenre()
        {
            return new SelectList(dbCon.vgw_genre_enum.Where(g => g.ISACTIVE == true).ToList(), "GENREID", "GENRE");
        }

        public GenreViewModel FindByGenreId(int GenreId) 
        {
            return dbCon.vgw_genre_enum.Where(g => g.GENREID == GenreId).Select(gv => new GenreViewModel
                                                                                    {
                                                                                        GENREID = gv.GENREID,
                                                                                        GENRENAME = gv.GENRE,
                                                                                        ISACTIVE = gv.ISACTIVE
                                                                                    }
                                                                                ).FirstOrDefault();
        }

        public vgw_genre_enum GetByGenreId(int GenreId)
        {
            return dbCon.vgw_genre_enum.Where(d => d.GENREID == GenreId).FirstOrDefault();
        }

        public bool SaveGenre(GenreViewModel genreViewModel, int iUserId)
        {

            if (genreViewModel.GENREID > 0)
            {
                vgw_genre_enum vgwGenre = GetByGenreId(genreViewModel.GENREID);

                vgwGenre.GENRE = genreViewModel.GENRENAME;
                vgwGenre.ISACTIVE = genreViewModel.ISACTIVE;
                vgwGenre.LASTUPDATEDDATE = DateTime.Now;
                vgwGenre.LASTUPDATEDBYUSERID = iUserId;
            }
            else
            {
                vgw_genre_enum vgwGenre = new vgw_genre_enum();

                vgwGenre.GENRE = genreViewModel.GENRENAME;
                vgwGenre.ISACTIVE = genreViewModel.ISACTIVE;
                vgwGenre.LASTUPDATEDDATE = DateTime.Now;
                vgwGenre.LASTUPDATEDBYUSERID = iUserId;
                dbCon.vgw_genre_enum.Add(vgwGenre);
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

        public bool DeleteGenre(int GenreId, int iUserId)
        {
            bool result = false;
            vgw_genre_enum vgwGenre = GetByGenreId(GenreId);
            if (vgwGenre != null)
            {
                try
                {
                    dbCon.vgw_genre_enum.Remove(vgwGenre);
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
