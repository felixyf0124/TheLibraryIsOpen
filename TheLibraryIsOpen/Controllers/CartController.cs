using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Models.Search;
using static TheLibraryIsOpen.Constants.TypeConstants;
using TheLibraryIsOpen.Models.Cart;

namespace TheLibraryIsOpen.Controllers
{
    public class CartController : Controller
    {

        private readonly ClientManager _cm;
        private readonly BookCatalog _bookc;
        private readonly MusicCatalog _musicc;
        private readonly MovieCatalog _moviec;
        private readonly MagazineCatalog _magazinec;
        private readonly IdentityMap _identityMap;
       
        public CartController(ClientManager cm, BookCatalog bc, MusicCatalog muc, MovieCatalog moc, MagazineCatalog mac, IdentityMap imap)
        {
            _cm = cm;
            _bookc = bc;
            _moviec = moc;
            _musicc = muc;
            _magazinec = mac;
            _identityMap = imap;
        }

        public async Task<IActionResult> Index()
        {
            Client client = await _cm.FindByEmailAsync(User.Identity.Name);
            int borrowMax = client.BorrowMax;
            int numModels = await _identityMap.CountModelCopiesOfClient(client.clientId);
            int cartCount = HttpContext.Session.GetInt32("ItemsCount") ?? 0;
            List<SessionModel> Items = HttpContext.Session.GetObject<List<SessionModel>>("Items") ?? new List<SessionModel>();
            TempData["totalBorrowed"] = cartCount + numModels;
            TempData["canBorrow"] = cartCount + numModels <= borrowMax;
            TempData["borrowMax"] = borrowMax;
            List<Task<Book>> bookTasks = new List<Task<Book>>(Items.Count);
            List<Task<Magazine>> magazineTasks = new List<Task<Magazine>>(Items.Count);
            List<Task<Movie>> movieTasks = new List<Task<Movie>>(Items.Count);
            List<Task<Music>> musicTasks = new List<Task<Music>>(Items.Count);

            foreach (SessionModel element in Items)
            {
                switch (element.ModelType)
                {
                    case TypeEnum.Book:
                        {
                            bookTasks.Add(_bookc.FindByIdAsync(element.Id.ToString()));
                            break;
                        }
                    case TypeEnum.Magazine:
                        {
                            magazineTasks.Add(_magazinec.FindByIdAsync(element.Id.ToString()));
                            break;
                        }
                    case TypeEnum.Movie:
                        {
                            movieTasks.Add(_moviec.GetMovieByIdAsync(element.Id));
                            break;
                        }
                    case TypeEnum.Music:
                        {
                            musicTasks.Add(_musicc.FindMusicByIdAsync(element.Id.ToString()));
                            break;
                        }

                }
            }

            List<CartViewModel> result = new List<CartViewModel>(Items.Count);
            
            result.AddRange(bookTasks.Select(t =>
            {
                t.Wait();
                Book b = t.Result;
                return new CartViewModel { ModelId = b.BookId, Title = b.Title, Type = TypeEnum.Book };
            }));

            result.AddRange(magazineTasks.Select(t =>
            {
                t.Wait();
                Magazine m = t.Result;
                return new CartViewModel { ModelId = m.MagazineId, Title = m.Title, Type = TypeEnum.Magazine };
            }));

            result.AddRange(movieTasks.Select(t =>
            {
                t.Wait();
                Movie m = t.Result;
                return new CartViewModel { ModelId = m.MovieId, Title = m.Title, Type = TypeEnum.Movie };
            }));

            result.AddRange(musicTasks.Select(t =>
            {
                t.Wait();
                Music m = t.Result;
                return new CartViewModel { ModelId = m.MusicId, Title = m.Title, Type = TypeEnum.Music };
            }));


            return View(result);
        }


        //removes item from session
        public IActionResult RemoveFromSessionModel(int modelId, TypeEnum mt)
        {
            int cartCount = HttpContext.Session.GetInt32("ItemsCount") ?? 0;

            List<SessionModel> Items = HttpContext.Session.GetObject<List<SessionModel>>("Items") ?? new List<SessionModel>();

            Items.RemoveAt(
                Items.FindIndex(item => item.Id == modelId && item.ModelType == mt)
            );

            HttpContext.Session.SetObject("Items", Items);
            cartCount = cartCount - 1;

            HttpContext.Session.SetInt32("ItemsCount", cartCount);
          

            return RedirectToAction(nameof(Index));
        }

        //registers modelcopies of selected items to the client
        [HttpPost]
        public async Task<IActionResult> Borrow() {
            //TODO what is the correct return type?

            List<SessionModel> modelsToBorrow = HttpContext.Session.GetObject<List<SessionModel>>("Items") ?? new List<SessionModel>();

            Client client = await _cm.FindByEmailAsync(User.Identity.Name);
            List<ModelCopy> alreadyBorrowed = await _identityMap.FindModelCopiesByClient(client.clientId);

            //Borrow all available copies of selected items
            Boolean successfulReservation = _identityMap.ReserveModelCopiesToClient(modelsToBorrow, client.clientId);

            //if not all items were borrowed, determine which ones were not borrowed and display them to the client
            if (!successfulReservation) {
                List<ModelCopy> nowBorrowed = await _identityMap.FindModelCopiesByClient(client.clientId);
                HashSet<ModelCopy> borrowed = nowBorrowed.Except(alreadyBorrowed).ToHashSet();
                List<SessionModel> notBorrowed = new List<SessionModel>();

                foreach (SessionModel sm in modelsToBorrow)
                {
                    bool matchfound = false;
                    foreach (ModelCopy mc in borrowed)
                    {
                        if (mc.modelID == sm.Id && mc.modelType.Equals(sm.ModelType))
                        {
                            matchfound = true;
                            break;
                        }
                    }

                    if (!matchfound) {
                        notBorrowed.Add(sm);
                    }
                }

                //TODO return to cart with notBorrowed (list of cartModels not reserved)
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }
    

    }
}
