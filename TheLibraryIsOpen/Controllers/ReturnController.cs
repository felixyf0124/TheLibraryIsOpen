using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.Return;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers
{
    public class ReturnController : Controller
    {
        private readonly ClientManager _cm;
        private readonly BookCatalog _bookc;
        private readonly MusicCatalog _musicc;
        private readonly MovieCatalog _moviec;
        private readonly MagazineCatalog _magazinec;
        private readonly IdentityMap _identityMap;

        public ReturnController(ClientManager cm, BookCatalog bc, MusicCatalog muc, MovieCatalog moc, MagazineCatalog mac, IdentityMap imap)
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
            TempData["totalBorrowed"] = cartCount + numModels;
            TempData["canBorrow"] = cartCount + numModels <= borrowMax;
            TempData["borrowMax"] = borrowMax;
            var modelCopies = await _identityMap.FindModelCopiesByClient(client.clientId);
            var returnViewModels = new List<ReturnViewModel>(modelCopies.Count);

            foreach (ModelCopy element in modelCopies)
            {
                string title = "";
                switch (element.modelType)
                {
                    case TypeEnum.Book:
                        {
                            title = (await _identityMap.FindBook(element.modelID)).Title;
                            break;
                        }
                    case TypeEnum.Magazine:
                        {
                            title = (await _identityMap.FindMagazine(element.modelID)).Title;
                            break;
                        }
                    case TypeEnum.Movie:
                        {
                            title = (await _identityMap.FindMovie(element.modelID)).Title;
                            break;
                        }
                    case TypeEnum.Music:
                        {
                            title = (await _identityMap.FindMusic(element.modelID)).Title;
                            break;
                        }

                }

                returnViewModels.Add(new ReturnViewModel { 
                    BorrowDate = element.borrowedDate, 
                    ModelCopyId = element.id, 
                    ModelId = element.modelID, 
                    ModelType = element.modelType, 
                    ReturnDate = element.returnDate, 
                    Title = title });
            }

            return View(returnViewModels);
        }

    }
}
