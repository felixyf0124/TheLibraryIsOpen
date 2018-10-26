using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers
{
    public class MagazineController : Controller
    {
        private readonly MagazineCatalog _cs;

        public MagazineController(MagazineCatalog cs)
        {
            _cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            List<Magazine> li = await _cs.GetAllMagazinesDataAsync();
            return View(li);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _cs.FindByIdAsync(id);
            if (magazine == null)
            {
                return NotFound();
            }

            return View(magazine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MagazineId,Title,Publisher,Language,Date,Isbn10,Isbn13")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                await _cs.CreateAsync(magazine);
                await _cs.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazine);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _cs.FindByIdAsync(id);

            if (magazine == null)
            {
                return NotFound();
            }
            return View(magazine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MagazineId,Title,Publisher,Language,Date,Isbn10,Isbn13")] Magazine magazine)
        {
            if (int.Parse(id) != magazine.MagazineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cs.UpdateAsync(magazine);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazineExists(id))
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
            return View(magazine);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _cs.FindByIdAsync(id);

            if (magazine == null)
            {
                return NotFound();
            }
            await _cs.DeleteAsync(magazine);
            await _cs.CommitAsync();
            return View(magazine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var magazine = await _cs.FindByIdAsync(id);
            await _cs.DeleteAsync(magazine);
            return RedirectToAction(nameof(Index));
        }

        private bool MagazineExists(string id)
        {
            return (_cs.FindByIdAsync(id) != null);
        }
    }
}
