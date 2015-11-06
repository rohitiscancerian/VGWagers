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
using System.IO;

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
            PlatformDAL platformDAL = new PlatformDAL();
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            GenreDAL genreDAL = new GenreDAL();
            
            LookupViewModel lookupViewModel = new LookupViewModel();
            lookupViewModel.GamesList = gameDAL.GetAllGames();
            //lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
            //lookupViewModel.DifficultyLevelList = difficultyLevelDAL.GetAllDifficultyLevels();
            //lookupViewModel.GenreList = genreDAL.GetAllGenre();

            ViewBag.LookupType = "Game";
            ViewBag.Mode = "List";
            return View(lookupViewModel);   
        }    
        
        public ActionResult NewGame()
        {
            GenreDAL genreDAL = new GenreDAL();
            LookupViewModel lookupViewModel = new LookupViewModel();
            GameViewModel game = new GameViewModel();
            ViewBag.ACTIVEPLATFORMS = new List<PlatformViewModel>();
            ViewBag.ACTIVEDIFFICULTYLEVELS = new List<DifficultyLevelViewModel>();
            lookupViewModel.Game = game;
            ViewBag.LookupType = "Game";
            ViewBag.Mode = "New";
            game.ACTIVEGENRES = genreDAL.GetAllActiveGenre();
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
            GenreDAL genreDAL = new GenreDAL();
            GameViewModel game = gameDAL.GetGameDetails((int)GameId);
            game.ACTIVEGENRES = genreDAL.GetAllActiveGenre();
            lookupViewModel.Game = game;
            ViewBag.LookupType = "Game";
            ViewBag.Mode = "Edit";             
            return View("Index", lookupViewModel);
        }

        public ActionResult SaveGame(GameViewModel game, HttpPostedFileBase file)
        {
            GameDAL gameDAL = new GameDAL();
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

            HttpPostedFile pic = null;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                pic = System.Web.HttpContext.Current.Request.Files["GAMEIMAGE.GAMEIMAGE"];
                if (pic == null)
                {
                    return Json(new { success = false, msg = "No file uploaded. Please upload a valid image file" });
                }
            }

            if (CommonFunctions.IsImage(pic))
            {
                byte[] fileByteArray = null;
                using (var binaryReader = new BinaryReader(pic.InputStream))
                {
                    pic.InputStream.Position = 0;
                    fileByteArray = binaryReader.ReadBytes(pic.ContentLength);
                }

                game.GAMEIMAGE.GAMEIMAGEBINARY = fileByteArray;
                
            }
            else
            {
                return Json(new { success = false, msg = "The file is not an image. Please upload a valid image file" });
            }            

            if (game.GAMEID > 0)
            {
                //Update
                if (gameDAL.SaveGame(game, objCurrentUser.Id))
                {
                    Success("Record modified successfully", true);
                }
                else
                {
                    Danger("Failed to modify record. Please try again.", true);
                }
            }
            else
            {
                //Insert
                if (gameDAL.SaveGame(game, objCurrentUser.Id))
                {
                    Success("Record addded successfully", true);
                }
                else
                {
                    Danger("Failed to insert new record. Please try again.", true);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetGameImage(int gameId)
        {
            GameDAL gameDAL = new GameDAL();
            byte[] gameImage = gameDAL.GetGameImage(gameId);
            String base64 = Convert.ToBase64String(gameImage);
            return Json(base64, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGamePlatforms(int gameId)
        {
            GameDAL gameDAL = new GameDAL();
            string[] gamePlatforms = gameDAL.GetGamePlatforms(gameId);
            return Json(gamePlatforms, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetActivePlatforms(string searchText)
        {
            PlatformDAL platformDAL = new PlatformDAL();            
            PlatformViewModel[] matching = string.IsNullOrWhiteSpace(searchText) ?
            platformDAL.GetAllActivePlatforms().ToArray() :
            platformDAL.GetAllActivePlatforms().Where(p => p.PLATFORMNAME.ToUpper().StartsWith(searchText.ToUpper())).ToArray();

            return Json(matching, JsonRequestBehavior.AllowGet);
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
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];
                        
            if (platform.PLATFORMID > 0)
            {
                //Update
                if (platformDAL.SavePlatform(platform, objCurrentUser.Id))
                {
                    Success("Record modified successfully", true);
                }  
                else
                {
                    Danger("Failed to modify record. Please try again.", true);
                }
            }
            else 
            {
                //Insert
                if (platformDAL.SavePlatform(platform, objCurrentUser.Id))
                {
                    Success("Record addded successfully", true);
                }
                else 
                {
                    Danger("Failed to insert new record. Please try again.", true);
                }
            }
            return RedirectToAction("PlatformList");
        }

        [HttpDelete]
        public ActionResult DeletePlatform(int? PlatformId)
        {
            //LookupViewModel lookupViewModel = new LookupViewModel();
            PlatformDAL platformDAL = new PlatformDAL();
                
            if (Request.IsAjaxRequest())
            {
                if (PlatformId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

                bool result = platformDAL.DeletePlatform((int)PlatformId, objCurrentUser.Id);

                if (result)
                {
                    Success("Successfully deleted Platform record.", true);
                }
                else
                {
                    Danger("Failed to delete Platform record. This may be due to the Platform being used in Games / Tournaments.", true);
                }                
            }

            //lookupViewModel.PlatformList = platformDAL.GetAllPlatforms();
            //ViewBag.LookupType = "Platform";
            //ViewBag.Mode = "List";
            return RedirectToAction("PlatformList");
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

        public JsonResult GetActiveDifficultyLevels(string searchText)
        {
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            DifficultyLevelViewModel[] matching = string.IsNullOrWhiteSpace(searchText) ?
                                        difficultyLevelDAL.GetAllActiveDifficultyLevels().ToArray() :
                                        difficultyLevelDAL.GetAllActiveDifficultyLevels().Where(p => p.DIFFICULTYLEVELNAME.ToUpper().StartsWith(searchText.ToUpper())).ToArray();

            return Json(matching, JsonRequestBehavior.AllowGet);
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

        public ActionResult SaveDifficultyLevel(DifficultyLevelViewModel difficultyLevel)
        {
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];
            
            if (difficultyLevel.DIFFICULTYLEVELID > 0)
            {
                //Update
                if (difficultyLevelDAL.SaveDifficultyLevel(difficultyLevel, objCurrentUser.Id))
                {
                    Success("Record modified successfully", true);
                }
                else
                {
                    Danger("Failed to modify record. Please try again.", true);
                }
            }
            else
            {
                //Insert
                if (difficultyLevelDAL.SaveDifficultyLevel(difficultyLevel, objCurrentUser.Id))
                {
                    Success("Record addded successfully", true);
                }
                else
                {
                    Danger("Failed to insert new record. Please try again.", true);
                }
            }
            return RedirectToAction("DifficultyLevelList");
        }

        [HttpDelete]
        public ActionResult DeleteDifficultyLevel(int? DifficultyLevelId)
        {
            DifficultyLevelDAL difficultyLevelDAL = new DifficultyLevelDAL();

            if (Request.IsAjaxRequest())
            {
                if (DifficultyLevelId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

                bool result = difficultyLevelDAL.DeleteDifficultyLevel((int)DifficultyLevelId, objCurrentUser.Id);

                if (result)
                {
                    Success("Successfully deleted Platform record.", true);
                }
                else
                {
                    Danger("Failed to delete Platform record. This may be due to the Platform being used in Games / Tournaments.", true);
                }
            }
            return RedirectToAction("DifficultyLevelList");
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

        public ActionResult SaveGenre(GenreViewModel genre)
        {
            GenreDAL genreDAL = new GenreDAL();
            ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

            if (genre.GENREID > 0)
            {
                //Update
                if (genreDAL.SaveGenre(genre, objCurrentUser.Id))
                {
                    Success("Record modified successfully", true);
                }
                else
                {
                    Danger("Failed to modify record. Please try again.", true);
                }
            }
            else
            {
                //Insert
                if (genreDAL.SaveGenre(genre, objCurrentUser.Id))
                {
                    Success("Record addded successfully", true);
                }
                else
                {
                    Danger("Failed to insert new record. Please try again.", true);
                }
            }
            return RedirectToAction("GenreList");
        }

        [HttpDelete]
        public ActionResult DeleteGenre(int? GenreId)
        {
            GenreDAL genreDAL = new GenreDAL();

            if (Request.IsAjaxRequest())
            {
                if (GenreId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                ApplicationUser objCurrentUser = (ApplicationUser)Session[SessionVariables.sesApplicationUser];

                bool result = genreDAL.DeleteGenre((int)GenreId, objCurrentUser.Id);

                if (result)
                {
                    Success("Successfully deleted Platform record.", true);
                }
                else
                {
                    Danger("Failed to delete Platform record. This may be due to the Platform being used in Games / Tournaments.", true);
                }
            }
            return RedirectToAction("GenreList");
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