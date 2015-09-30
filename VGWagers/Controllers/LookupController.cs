using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
using VGWagers.Models;
using VGWagers.Utilities;
using System.Collections.Generic;
using VGWagers.DAL;
using VGWagers.Resource;

namespace VGWagers.Controllers
{
    [Authorize(Roles="Administrator")]
    public class LookupController : BaseController
    {
        
        public LookupController()
        {
            
        }

        public ActionResult Index()
        {
            //Games List

            GameDAL gameDAL = new GameDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.GamesList = gameDAL.GetAllGames();
            ViewBag.LookupType = "Game";
            ViewBag.Mode = "List";
            return View(lookupViewModel);   
        }    
        
        public ActionResult NewGame()
        {
            GenreDAL genreDAL = new GenreDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.Game = new GameViewModel();
            ViewBag.LookupType = "Game";
            ViewBag.Mode = "New";
            ViewBag.GENREID = genreDAL.GetAllActiveGenre();
            return View("Index", lookupViewModel);
        }

        public ActionResult EditGame(int? GameId)
        {
            if (GameId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LookupViewModel lookupViewModel = new LookupViewModel();
            GameDAL gameDAL = new GameDAL();
            lookupViewModel.Game = gameDAL.FindByGameId((int)GameId);
            ViewBag.LookupType = "Game";
            ViewBag.Mode = "Edit";
            return View("Index", lookupViewModel);
        }

        public ActionResult PlatformList()
        {
            PlatformDAL platformDAL = new PlatformDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            ViewBag.LookupType = "Platform";
            ViewBag.Mode = "List";
            lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
            return View("Index", lookupViewModel);
        }

        public ActionResult NewPlatform()
        {
            PlatformDAL platformDAL = new PlatformDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.Platform = new PlatformViewModel();
            ViewBag.LookupType = "Platform";
            ViewBag.Mode = "New";
            return View("Index", lookupViewModel);
        }

        public ActionResult EditPlatform(int? PlatformId)
        {
            if (PlatformId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LookupViewModel lookupViewModel = new LookupViewModel();
            PlatformDAL platformDAL = new PlatformDAL();
            lookupViewModel.Platform = platformDAL.FindByPlatformId((int)PlatformId);
            ViewBag.LookupType = "Platform";
            ViewBag.Mode = "Edit";
            return View("Index", lookupViewModel);
        }

        public ActionResult SavePlatform(PlatformViewModel platform)
        {
            PlatformDAL platformDAL = new PlatformDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];
                        
            if (platform.PLATFORMID > 0)
            {
                //Update
                if (platformDAL.SavePlatform(platform, objCurrentUser.Id))
                {
                    ViewBag.Message = "Record modified successfully";
                }                
            }
            else 
            {
                //Insert
                if (platformDAL.SavePlatform(platform, objCurrentUser.Id))
                {
                    ViewBag.Message = "Record addded successfully";
                }
            }
            ViewBag.LookupType = "Platform";
            ViewBag.Mode = "List";
            lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
            return View("Index", lookupViewModel);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]        
        public ActionResult DeletePlatform(int? PlatformId)
        {
            if (PlatformId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LookupViewModel lookupViewModel = new LookupViewModel();
            PlatformDAL platformDAL = new PlatformDAL();
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

            bool result = platformDAL.DeletePlatform((int)PlatformId, objCurrentUser.Id);

            if (result)
            {
                ViewBag.Information = "Successfully deleted Platform record.";
            }
            else
            {
                ViewBag.Error = "Failed to delete Platform record. This may be due to the Platform being used in Games / Tournaments.";
            }

            lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
            ViewBag.LookupType = "Platform";
            ViewBag.Mode = "List";
            return View("Index", lookupViewModel);
        }

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeletePlatformConfirmed(int PlatformId)
        //{
        //    LookupViewModel lookupViewModel = new LookupViewModel();
        //    PlatformDAL platformDAL = new PlatformDAL();
            
        //    //to do: add call to DAL for delete

        //    lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
        //    ViewBag.LookupType = "Platform";
        //    ViewBag.Mode = "List";
        //    return View("Index", lookupViewModel);
        //}

        public ActionResult DifficultyLevelList()
        {
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            ViewBag.LookupType = "DifficultyLevel";
            ViewBag.Mode = "List";
            lookupViewModel.DifficultyLevelList = difficultyLevelDAL.GetAllDifficultyLevels();
            return View("Index", lookupViewModel);
        }

        public ActionResult NewDifficultyLevel()
        {
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.DifficultyLevel = new DifficultyLevelViewModel();
            ViewBag.LookupType = "DifficultyLevel";
            ViewBag.Mode = "New";
            return View("Index", lookupViewModel);
        }

        public ActionResult EditDifficultyLevel(int? DifficultyLevelId)
        {
            if (DifficultyLevelId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LookupViewModel lookupViewModel = new LookupViewModel();
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            lookupViewModel.DifficultyLevel = difficultyLevelDAL.FindByDifficultyLevelId((int)DifficultyLevelId);
            ViewBag.LookupType = "DifficultyLevel";
            ViewBag.Mode = "Edit";
            return View("Index", lookupViewModel);
        }

        public ActionResult GenreList()
        {
            GenreDAL genreDAL = new GenreDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.GenreList = genreDAL.GetAllGenre();
            ViewBag.LookupType = "Genre";
            ViewBag.Mode = "List";
            return View("Index", lookupViewModel);
        }

        public ActionResult NewGenre()
        {
            GenreDAL genreDAL = new GenreDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.Genre = new GenreViewModel();
            ViewBag.LookupType = "Genre";
            ViewBag.Mode = "New";
            return View("Index", lookupViewModel);
        }

        public ActionResult EditGenre(int? GenreId)
        {
            if (GenreId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LookupViewModel lookupViewModel = new LookupViewModel();
            GenreDAL genreDAL = new GenreDAL();
            lookupViewModel.Genre = genreDAL.FindByGenreId((int)GenreId);
            ViewBag.LookupType = "Genre";
            ViewBag.Mode = "Edit";            
            return View("Index", lookupViewModel);
        }
        
        private Dictionary<string, ModelErrorCollection> GetErrorsFromModelState() //IEnumerable<string>
        {
            return ModelState.Keys.Where(key => ModelState[key].Errors.Count > 0).ToDictionary(key => key, key => ModelState[key].Errors);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }

            base.Dispose(disposing);
        }

        #region Helpers

        
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

       
        #endregion
    }
}