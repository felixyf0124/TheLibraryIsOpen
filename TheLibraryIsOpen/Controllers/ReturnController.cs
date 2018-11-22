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

        public IActionResult Index()
        {
            return View();
        }

    }
}
