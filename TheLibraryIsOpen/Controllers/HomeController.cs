using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Database;


namespace TheLibraryIsOpen.Controllers
{
	public class HomeController : Controller
	{
        private readonly ClientStore _cs;

        public HomeController(ClientStore cs)
        {
            _cs = cs;
        }
	    
        public IActionResult Index()
		{
			return View();
		}

        public IActionResult Book()
        {
            return View();
        }

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

        // for test
	    public void ListTest()
	    {
	        Db _db = new Db();
	        List<Book> allBooksList = _db.GetAllBooks();
	        foreach (var book in allBooksList as List<TheLibraryIsOpen.Models.DBModels.Book>)
	        {
	            Console.Write(book.ToString());
	        }
	    }

	    public async Task<ActionResult> ListOfClients()
        {
            string clientEmail = User.Identity.Name;
            bool isAdmin = await _cs.IsItAdminAsync(clientEmail);
            if (isAdmin)
            {
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
