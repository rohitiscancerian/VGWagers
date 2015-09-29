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
    public class PlatformDAL
    {
        VGWagersDB dbCon;

        public PlatformDAL() 
        {
            dbCon = new VGWagersDB();
        }

        public IList<PlatformViewModel> GetAllPlatforms()
        {
            return dbCon.vgw_platform.Select(p => new PlatformViewModel { 
                                                    PLATFORMID = p.PLATFORMID, 
                                                    PLATFORMNAME = p.PLATFORMNAME, 
                                                    ISACTIVE = p.ISACTIVE 
                                                }).ToList();
        }   

        public SelectList GetAllActivePlatforms()
        {
            return new SelectList(dbCon.vgw_platform.Where(p => p.ISACTIVE == true).ToList(), "PLATFORMID", "PLATFORMNAME");
        }

        public PlatformViewModel FindByPlatformId(int PlatformId)
        {
            return dbCon.vgw_platform.Where(p => p.PLATFORMID == PlatformId).Select(pv => new PlatformViewModel
                                                                                        {
                                                                                            PLATFORMID = pv.PLATFORMID,
                                                                                            PLATFORMNAME = pv.PLATFORMNAME,
                                                                                            ISACTIVE = pv.ISACTIVE
                                                                                        }
                                                                                   ).FirstOrDefault();
        }
    }

    
}
