using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Controllers.StorageManagement;

namespace TheLibraryIsOpen.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionCatalog _tc;

        public TransactionController(TransactionCatalog tc)
        {
            _tc = tc;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _tc.getLogs());
        }
    }
}