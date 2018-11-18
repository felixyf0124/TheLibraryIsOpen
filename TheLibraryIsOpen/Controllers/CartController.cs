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
        private UnitOfWork _unitOfWork;
       


        public CartController(ClientManager cm, BookCatalog bc, MusicCatalog muc, MovieCatalog moc, MagazineCatalog mac, IdentityMap imap, UnitOfWork uow)
        {
            _cm = cm;
            _bookc = bc;
            _moviec = moc;
            _musicc = muc;
            _magazinec = mac;
            _identityMap = imap;
            _unitOfWork = uow;
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
        public async void Borrrow(List<CartViewModel> modelsToBorrow) {
            //TODO what is the correct return type?
            //TODO is the list supposed to be a parameter? Not sure with POST

            //check for available modelcopies
            List<ModelCopy> copiesToRegister = new List<ModelCopy>();
            foreach (CartViewModel cm in modelsToBorrow) 
            {
                List<ModelCopy> copies = await _identityMap.FindModelCopies(cm.ModelId, cm.Type);
                foreach (ModelCopy copy in copies) 
                {
                    //TODO must be a better way to check if a copy is not currently checked out?
                    if (copy.borrowerID.ToString().Equals("") || copy.returnDate.ToString().Equals("") || copy.borrowedDate.ToString().Equals("")) 
                    {
                        copiesToRegister.Add(copy);
                        break;
                    }
                    //TODO return error here : model copy of this item is not available
                }

            }

            //register copies to current client
            Client client = await _cm.FindByEmailAsync(User.Identity.Name);
            int clientId = client.clientId;
            foreach (ModelCopy mc in copiesToRegister) 
            {
                mc.borrowerID = clientId;
                mc.borrowedDate = DateTime.Today;
                switch (mc.modelType)
                {
                    case TypeEnum.Book:
                        {
                            mc.returnDate = mc.borrowedDate.AddDays(7);
                            break;
                        }
                    case TypeEnum.Magazine:
                        {
                            mc.returnDate = mc.borrowedDate.AddDays(7);
                            break;
                        }
                    case TypeEnum.Movie:
                        {
                            mc.returnDate = mc.borrowedDate.AddDays(2);
                            break;
                        }
                    case TypeEnum.Music:
                        {
                            mc.returnDate = mc.borrowedDate.AddDays(2);
                            break;
                        }
                }
                _unitOfWork.RegisterDirty(mc);
            }

            //TODO return to home index?

        }
    

    }
}
