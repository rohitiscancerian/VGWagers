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
            return dbCon.vgw_platform_enum.Select(p => new PlatformViewModel 
                                                { 
                                                    PLATFORMID = p.PLATFORMID, 
                                                    PLATFORMNAME = p.PLATFORMNAME, 
                                                    ISACTIVE = p.ISACTIVE 
                                                }).ToList();
        }

        public IList<PlatformViewModel> GetAllActivePlatforms()
        {
            return dbCon.vgw_platform_enum.Where(p => p.ISACTIVE == true)
                                     .Select(p => new PlatformViewModel 
                                             {
                                                PLATFORMID = p.PLATFORMID, 
                                                PLATFORMNAME = p.PLATFORMNAME,
                                                ISACTIVE = p.ISACTIVE 
                                             })
                                     .ToList();
        }

        public PlatformViewModel FindByPlatformId(int PlatformId)
        {
            return dbCon.vgw_platform_enum.Where(p => p.PLATFORMID == PlatformId).Select(pv => new PlatformViewModel
                                                                                        {
                                                                                            PLATFORMID = pv.PLATFORMID,
                                                                                            PLATFORMNAME = pv.PLATFORMNAME,
                                                                                            ISACTIVE = pv.ISACTIVE
                                                                                        }
                                                                                   ).FirstOrDefault();
        }

        public vgw_platform_enum GetByPlatformId(int PlatformId)
        {
            return dbCon.vgw_platform_enum.Where(p => p.PLATFORMID == PlatformId).FirstOrDefault();
        }

        public bool SavePlatform(PlatformViewModel platformViewModel, int iUserId)
        {
            
            if (platformViewModel.PLATFORMID > 0)
            {
                vgw_platform_enum vgwPlatform = GetByPlatformId(platformViewModel.PLATFORMID);

                vgwPlatform.PLATFORMNAME = platformViewModel.PLATFORMNAME;
                vgwPlatform.ISACTIVE = platformViewModel.ISACTIVE;
                vgwPlatform.LASTUPDATEDDATE = DateTime.Now;
                vgwPlatform.LASTUPDATEDBYUSERID = iUserId;                
            }
            else
            {
                vgw_platform_enum vgwPlatform = new vgw_platform_enum();
                
                vgwPlatform.PLATFORMNAME = platformViewModel.PLATFORMNAME;
                vgwPlatform.ISACTIVE = platformViewModel.ISACTIVE;
                vgwPlatform.LASTUPDATEDDATE = DateTime.Now;
                vgwPlatform.LASTUPDATEDBYUSERID = iUserId;
                dbCon.vgw_platform_enum.Add(vgwPlatform);
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

        public bool DeletePlatform(int PlatformId, int iUserId)
        {
            bool result = false;
            vgw_platform_enum vgwPlatform = GetByPlatformId(PlatformId);
            if (vgwPlatform != null)
            {
                try 
                {
                    dbCon.vgw_platform_enum.Remove(vgwPlatform);
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
