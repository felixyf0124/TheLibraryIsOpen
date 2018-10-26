﻿using System;
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
        private readonly BookCatalog _bookc;
        private readonly MusicCatalog _musicc;
        private readonly MovieCatalog _moviec;
        private readonly MagazineCatalog _magazinec;

        public HomeController(ClientStore cs, BookCatalog bc, MusicCatalog muc, MovieCatalog moc, MagazineCatalog mac)
        {
            _cs = cs;
            _bookc = bc;
            _moviec = moc;
            _musicc = muc;
            _magazinec = mac;
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

        public async Task<ActionResult> Catalog()
        {
            CatalogViewModel list = new CatalogViewModel(await _bookc.GetAllBookDataAsync(), await _musicc.GetAllMusicDataAsync(), await _moviec.GetAllMoviesDataAsync(), await _magazinec.GetAllMagazinesDataAsync());
            return View(list);
        }

        // For test db.cs
        public void ListTest()
	    {
	        Db _db = new Db();
            /*List<Book> allBooksList = _db.GetAllBooks();
	        foreach (var book in allBooksList as List<TheLibraryIsOpen.Models.DBModels.Book>)
	        {
	            Console.Write(book.ToString());
	        }*/
/*

	        List<Magazine> allMgsList = _db.GetAllMagazines();
	        foreach (var mg in allMgsList as List<TheLibraryIsOpen.Models.DBModels.Magazine>)
	        {
	            Console.Write(mg.ToString());
	        }*/
	        
/*

            Magazine mg1 = new Magazine("test23","test3","test2","test2","test2","test2");
            _db.UpdateMagazine(mg1,1);
            _db.DeleteMagazineByID(3);*/

           /*Book book2 = new Book("Do Android Dream of Electric Sheep?","Philip K. Dick","Paperback",55555,"Del Rey","Sept .26 2017","English","1524796972","978-1524796976");
            //_db.CreateBook(book2);
            _db.UpdateBookByBookIsbn(book2, "1524796972");*/
            //_db.DeleteBookByIsbn10("1524796972");
	        Magazine book1 = _db.GetMagazineByIsbn13("2323");
	        Console.Write(book1.ToString());

        }

	    public async Task<ActionResult> ListOfClients()
        {
            string clientEmail = User.Identity.Name;
            bool isAdmin = await _cs.IsItAdminAsync(clientEmail);
            if (isAdmin)
            {
                //Retrieve all the clients from the database
                List<Client> allClientsList = await _cs.GetAllClientsDataAsync();
                return View(allClientsList);
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
