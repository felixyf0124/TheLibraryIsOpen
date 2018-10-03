using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Your movie index page.";
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Message"] = "Your create movie page.";
            return View();
        }

        public IActionResult Delete()
        {

            ViewData["Message"] = "Your delete movie page.";
            return View();
        }


        public IActionResult Edit()
        {
            ViewData["Message"] = "Your edit movie page.";
            return View();
        }

        public IActionResult List()
        {
            ViewData["Message"] = "Your movie list page.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}