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
        private readonly Db _db; //gonna change this to the actual thing we're gonna use. (Catalog, UoW, etc.)
        // private readonly MovieManager _mm;
        //private Client client;

        // public AuthenticationController(Db db, MovieManager mm) //gonna change this to the actual thing we're gonna use (Catalog, UoW, etc.)


        public IActionResult Index()
        {
            ViewData["Message"] = "Your movie index page.";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Your create movie page.";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            ViewData["Message"] = "Your create movie page.";
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // TODO: add delete method
            return View();

        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            try
            {
                // TODO: add delete from db method
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            // Movie m = await _mm.FindByIdAsync(id.ToString());
            EditViewModel edit = new EditViewModel
            {
                // TODO: add attribute edits};
            };
            return View(edit);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, EditViewModel edit)
        {
            try
            {
                // Movie m = await _mm.FindByIdAsync(id.ToString());
                // TODO: add edit attributes
                // await _cm.UpdateAsync(c);
                return RedirectToAction(nameof(Edit), id);
            }
            catch
            {
                return RedirectToAction("Home", "ListOfMovies");
            }
        }

        public IActionResult ListOfMovies()
        {
            ViewData["Message"] = "Your movie list page.";
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

    }
}