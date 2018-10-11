using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.Authentication;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieCatalog _mc; //gonna change this to the actual thing we're gonna use. (Catalog, UoW, etc.)
        // private readonly MovieManager _mm;
        //private Client client;

        // public AuthenticationController(Db db, MovieManager mm) //gonna change this to the actual thing we're gonna use (Catalog, UoW, etc.)
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

            return View(toEdit);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Movie edit)
        {
            try
            {
                // Movie m = await _mm.FindByIdAsync(id.ToString());
                // TODO: add edit attributes

                 await _mc.UpdateMovieAsync(edit);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Home", "Index");
            }
        }


        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

    }
}