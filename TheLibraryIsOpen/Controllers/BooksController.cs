using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.SessionExtensions;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookCatalog _bc;
        private readonly ClientStore _cs;

        public BooksController(BookCatalog bc, ClientStore cs)
        {
            _bc = bc;
            _cs = cs;
        }
        // GET: Books
        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            List<Book> books = await _bc.GetAllBookDataAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bc.FindByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            TempData["AvailableCopies"] = await _bc.GetNoOfAvailableModelCopies(book);

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                if (!isAdmin)
                    return Unauthorized();
                await _bc.CreateAsync(book);
                await _bc.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var book = await _bc.FindByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Book book)
        {
            if (int.Parse(id) != book.BookId)
            {
                return NotFound();
            }
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    await _bc.UpdateAsync(book);
                    await _bc.CommitAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var book = await _bc.FindByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            await _bc.DeleteAsync(book);
            await _bc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToCart(int id)
        {
            var Items = HttpContext.Session.GetObject<List<SessionModel>>("Items")
                ?? new List<SessionModel>();
            Items.Add(new SessionModel { Id = id, ModelType = TypeEnum.Book });
            HttpContext.Session.SetObject("Items", Items);
            HttpContext.Session.SetInt32("ItemsCount", Items.Count);
            return RedirectToAction(nameof(Details), new { id = id.ToString() });
        }

        private bool BookExists(string id)
        {
            return (_bc.FindByIdAsync(id) != null);
        }
        

        public async Task<IActionResult> AddModelCopy(string id)
        {
            await _bc.AddModelCopy(id);
            await _bc.CommitAsync();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> DeleteModelCopy(string id)
        {
            await _bc.DeleteFreeModelCopy(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }


    }
}
