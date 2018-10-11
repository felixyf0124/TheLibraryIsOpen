using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class BooksController : Controller
    {
        private readonly BookCatalog _bc;

        public BooksController(BookCatalog bc)
        {
            _bc = bc;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View();
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

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Format,Pages,Publisher,Year,Language,Isbn10,Isbn13")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bc.CreateAsync(book);
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            return View(book);
        }

        // POST: Books/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,Title,Author,Format,Pages,Publisher,Year,Language,Isbn10,Isbn13")] Book book)
        {
            if (int.Parse(id) != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bc.UpdateAsync(book);

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
        public async Task<IActionResult> Delete(string id)
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
            await _bc.DeleteAsync(book);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _bc.FindByIdAsync(id);

            await _bc.DeleteAsync(book);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return (_bc.FindByIdAsync(id) != null);
        }
    }
}
