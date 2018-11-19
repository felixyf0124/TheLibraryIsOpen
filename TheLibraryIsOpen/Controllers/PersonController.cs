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
    public class PersonController : Controller
    {
        private readonly PersonCatalog _mc;
        private readonly ClientStore _cs;

        public PersonController(PersonCatalog mc, ClientStore cs)
        {
            _mc = mc;
            _cs = cs;
        }
        
        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            List<Person> li = await _mc.GetAllPersonDataAsync();
            return View(li);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _mc.FindByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            //TempData["AvailableCopies"] = await _mc.getNoOfAvailableModelCopies(magazine);

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
                if (!isAdmin)
                    return Unauthorized();
                await _mc.CreateAsync(person);
                await _mc.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
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

            var person = await _mc.FindByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Person person)
        {
            if (int.Parse(id) != person.PersonId)
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
                    await _mc.UpdateAsync(person);
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
            return View(person);
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
            var person = await _mc.FindByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Person person)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            await _mc.DeleteAsync(person);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToCart(int id)
        {
            var Items = HttpContext.Session.GetObject<List<SessionModel>>("Items")
                ?? new List<SessionModel>();
            Items.Add(new SessionModel { Id = id, ModelType = TypeEnum.Magazine});
            HttpContext.Session.SetObject("Items", Items);
            HttpContext.Session.SetInt32("ItemsCount", Items.Count);
            return RedirectToAction(nameof(Details), new { id = id.ToString() });
        }

        private bool MagazineExists(string id)
        {
            return (_mc.FindByIdAsync(id) != null);
        }

        public async Task<IActionResult> AddModelCopy(string id)
        {
            await _mc.addModelCopy(id);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Details), new { id = id.ToString() });
        }

        public async Task<IActionResult> DeleteModelCopy(string id)
        {
            await _mc.deleteFreeModelCopy(id);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Details), new { id = id.ToString() });
        }
    }
}
