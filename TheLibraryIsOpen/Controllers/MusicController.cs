using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheLibraryIsOpen.Controllers
{
    public class MusicController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewMusic()
        {
            ViewData["Message"] = "Add Music";

            return View();
        }
    
        public IActionResult DeleteMusic()
        {
            ViewData["Message"] = "Delete Music";

            return View();
        }
        public IActionResult EditMusic()
        {
            ViewData["Message"] = "Edit Music";

            return View();
        }
    }
}