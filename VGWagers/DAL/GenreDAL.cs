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
                                                    GENRE = g.GENRE, 
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
                                                                                        GENRE = gv.GENRE,
                                                                                        ISACTIVE = gv.ISACTIVE
                                                                                    }
                                                                                ).FirstOrDefault();
        }
    }

    
}
