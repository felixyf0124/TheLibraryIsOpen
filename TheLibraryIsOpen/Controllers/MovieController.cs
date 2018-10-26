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

namespace TheLibraryIsOpen.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieCatalog _mc;

        public MovieController(MovieCatalog mc)
        {
            _mc = mc;
        }

        public async Task<IActionResult> Index()
        {
            List<Movie> lm = await _mc.GetAllMoviesDataAsync();
            ViewData["Message"] = "Your movie list page.";
            return View(lm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Your create movie page.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            ViewData["Message"] = "Your create movie page.";
            await _mc.CreateMovieAsync(movie);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            Movie toDelete = await _mc.GetMovieByIdAsync(id);

            return View(toDelete);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Movie movie)
        {
            try
            {
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

            return View(toDetails);

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
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
                await _mc.UpdateMovieAsync(edit.ToMovie());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Home", "Index");
            }
        }
    }
}