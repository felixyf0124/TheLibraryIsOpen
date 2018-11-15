using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.Authentication;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Models.Movie;
using TheLibraryIsOpen.Models;
using static TheLibraryIsOpen.Constants.SessionExtensions;

namespace TheLibraryIsOpen.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieCatalog _mc;
        private readonly ClientStore _cs;

        public MovieController(MovieCatalog mc, ClientStore cs)
        {
            _mc = mc;
            _cs = cs;
        }
        
        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            List<Movie> lm = await _mc.GetAllMoviesDataAsync();
            ViewData["Message"] = "Your movie list page.";
            return View(lm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            ViewData["Message"] = "Your create movie page.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            ViewData["Message"] = "Your create movie page.";
            await _mc.CreateMovieAsync(movie);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            Movie toDelete = await _mc.GetMovieByIdAsync(id);

            return View(toDelete);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Movie movie)
        {
            try
            {
                bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                if (!isAdmin)
                    return Unauthorized();
                ViewData["Message"] = "Your delete movie page.";
                await _mc.DeleteMovieAsync(movie);
                await _mc.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        public async Task<ActionResult> Details(int id)
        {

            Movie toDetails = await _mc.GetMovieByIdAsync(id);
            toDetails.Producers = await _mc.GetAllMovieProducerDataAsync(id);
            toDetails.Actors = await _mc.GetAllMovieActorDataAsync(id);

            TempData["AvailableCopies"] = await _mc.getNoOfAvailableModelCopies(toDetails);

            return View(toDetails);

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            Movie toEdit = await _mc.GetMovieByIdAsync(id);
            toEdit.Producers = await _mc.GetAllMovieProducerDataAsync(id);
            toEdit.Actors = await _mc.GetAllMovieActorDataAsync(id);

            return View(toEdit.ToEditViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MovieViewModel edit)
        {
            try
            {
                bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                if (!isAdmin)
                    return Unauthorized();
                await _mc.UpdateMovieAsync(edit.ToMovie());
                await _mc.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Home", "Index");
            }
        }

        public async void AddToCart(Movie movie)
        {
            var Items = HttpContext.Session.GetObject<List<SessionModel>>("Items")
                ?? new List<SessionModel>();
            SessionModel _item = new SessionModel();
            List<ModelCopy> copies = await _mc.getModelCopies(movie);
            foreach (ModelCopy tempMC in copies)
            {
                if (tempMC.borrowerID == 0)
                {
                    _item.Id = tempMC.id;
                    _item.ModelType = tempMC.modelType;
                    Items.Add(_item);
                    HttpContext.Session.SetObject("Items", Items);
                    break;
                }
            }
        }

    }
}