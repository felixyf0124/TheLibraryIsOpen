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
        private readonly MagazineCatalog _mc;
        private readonly ClientStore _cs;

        public MagazineController(MagazineCatalog mc, ClientStore cs)
        {
            _mc = mc;
            _cs = cs;
        }
        
        public async Task<IActionResult> Index()
        {
            List<Magazine> li = await _mc.GetAllMagazinesDataAsync();
            return View(li);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _mc.FindByIdAsync(id);
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
        public async Task<IActionResult> Create(Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                if (!isAdmin)
                    return Unauthorized();
                await _mc.CreateAsync(magazine);
                await _mc.CommitAsync();
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

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var magazine = await _mc.FindByIdAsync(id);

            if (magazine == null)
            {
                return NotFound();
            }
            return View(magazine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Magazine magazine)
        {
            if (int.Parse(id) != magazine.MagazineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                    if (!isAdmin)
                        return Unauthorized();
                    await _mc.UpdateAsync(magazine);
                    await _mc.CommitAsync();
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

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            var magazine = await _mc.FindByIdAsync(id);

            if (magazine == null)
            {
                return NotFound();
            }
            await _mc.DeleteAsync(magazine);
            await _mc.CommitAsync();
            return View(magazine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            var magazine = await _mc.FindByIdAsync(id);
            await _mc.DeleteAsync(magazine);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazineExists(string id)
        {
            return (_mc.FindByIdAsync(id) != null);
        }
    }
}
