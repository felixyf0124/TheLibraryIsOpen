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
    public class MusicController : Controller
    {
        private readonly MusicCatalog _mc;
        private readonly ClientStore _cs;

        public MusicController(MusicCatalog mc, ClientStore cs)
        {
            _mc = mc;
            _cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            List<Music> musicList = await _mc.GetAllMusicDataAsync();
            return View(musicList);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _mc.FindMusicByIdAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            TempData["AvailableCopies"] = await _mc.getNoOfAvailableModelCopies(music);

            return View(music);
        }

        public async Task<IActionResult> Create()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Music music)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            if (ModelState.IsValid)
            {
                await _mc.CreateMusicAsync(music);
                await _mc.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

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

            var music = await _mc.FindMusicByIdAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Music music)
        {
            if (int.Parse(id) != music.MusicId)
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
                    await _mc.UpdateMusicAsync(music);
                    await _mc.CommitAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(id))
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
            return View(music);
        }
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

            var music = await _mc.FindMusicByIdAsync(id);

            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Music music)
        {

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            await _mc.DeleteMusicAsync(music);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToCart(int id)
        {
            var Items = HttpContext.Session.GetObject<List<SessionModel>>("Items")
                ?? new List<SessionModel>();
            Items.Add(new SessionModel { Id = id, ModelType = TypeEnum.Music });
            HttpContext.Session.SetObject("Items", Items);
            HttpContext.Session.SetInt32("ItemsCount", Items.Count);
            return RedirectToAction(nameof(Details), new { id = id.ToString() });
        }

        private bool MusicExists(string id)
        {
            return (_mc.FindMusicByIdAsync(id) != null);
        }

    }
}