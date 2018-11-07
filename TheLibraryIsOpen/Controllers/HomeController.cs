using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Models.Search;

namespace TheLibraryIsOpen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientStore _cs;
        private readonly BookCatalog _bookc;
        private readonly MusicCatalog _musicc;
        private readonly MovieCatalog _moviec;
        private readonly MagazineCatalog _magazinec;
        private readonly Search _search;

        public HomeController(ClientStore cs, BookCatalog bc, MusicCatalog muc, MovieCatalog moc, MagazineCatalog mac, Search search)
        {
            _cs = cs;
            _bookc = bc;
            _moviec = moc;
            _musicc = muc;
            _magazinec = mac;
            _search = search;
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

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            var form = HttpContext.Request.Form;
            string modeltype = form["modeltype"];
            string query = form["query"];

            TempData["ModelType"] = modeltype;
            TempData["Query"] = query;

            string[] querySplit = query.Split(';');

            var queryTask = new Task<List<SearchResult>>[querySplit.Length];

            for (int i = 0; i < querySplit.Length; i++)
            {
                querySplit[i] = querySplit[i].Trim();
                switch (modeltype)
                {
                    case "book":
                        queryTask[i] = _search.SearchBooksAsync(querySplit[i]);
                        break;
                    case "movie":
                        queryTask[i] = _search.SearchMoviesAsync(querySplit[i]);
                        break;
                    case "magazine":
                        queryTask[i] = _search.SearchMagazinesAsync(querySplit[i]);
                        break;
                    case "music":
                        queryTask[i] = _search.SearchMusicAsync(querySplit[i]);
                        break;
                    default:
                        queryTask[i] = _search.SearchAllAsync(querySplit[i]);
                        break;
                }
            }

            var searchResults = new List<SearchResult>[queryTask.Length];

            for (int i = 0; i < queryTask.Length; i++)
            {
                searchResults[i] = await queryTask[i];
            }

            HashSet<SearchResult> matchingSearchResults = new HashSet<SearchResult>(searchResults[0], new Search.SearchResultComparer());

            for (int i = 1; i < searchResults.Length; i++)
            {
                matchingSearchResults.IntersectWith(searchResults[i]);
            }

            return View(matchingSearchResults.OrderBy(r=>r.Name));
        }
    }
}
