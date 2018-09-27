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
	public class HomeController : Controller
	{
		public IActionResult Index()
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

        public IActionResult ListOfClients()
        {

            //Retrive data from Database
            
            var databaseFunctions = new TheLibraryIsOpen.Database.Db();
            List<Client> allClientsList = new List<Client>();
            allClientsList = databaseFunctions.GetAllClients();
            

            // Hard coded data for testing (without database) for testing
            /*List<Client> allClientsList = new List<Client>();
            //Client client = new Client(clientID, firstName, lastName, emailAddress, homeAddress, phoneNumber, password, isAdmin);
            allClientsList.Add(new Client(01, "Test", "Test", "test@email.com", "1 road", "(514)111-1111", "1234", false));
            allClientsList.Add(new Client(02, "Name", "Name", "name@email.com", "2 road", "(514)121-1111", "1234", false));
            allClientsList.Add(new Client(03, "Fake", "Fake", "fake@email.com", "3 road", "(514)113-1111", "1234", false));
            allClientsList.Add(new Client(04, "Joe", "Doe", "joedoe@email.com", "4 road", "(514)111-4111", "1234", true));
            /////////////////////////////////////////////////
            */

            ViewData["count"] = allClientsList.Count();
            ViewData["allClients"] = allClientsList;

            return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
