using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Models.Authentication;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DbQuery _db; //gonna change this to the actual thing we're gonna use. (Catalog, UoW, etc.)
        private readonly UserManager<Client> _cm;

        public AuthenticationController(DbQuery db, ClientManager cm) //gonna change this to the actual thing we're gonna use (Catalog, UoW, etc.)
        {
            _db = db;
            _cm = cm;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            try
            {
                Client c = await _cm.FindAsync(model.Email, model.Password);
                if (c != null)
                {
                    await SignInAsync(c, true);
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(DefaultAuthenticationTypes.ExternalCookie);
            return Login();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception();
                var x = await _cm.CreateAsync(model.ToClient());
                if (!x.Succeeded)
                {
                    throw new Exception();
                }
                var client = await _cm.FindByEmailAsync(model.Email);
                await SignInAsync(client, true);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditAsync(int id)
        {
            Client c = await _cm.FindByIdAsync(id.ToString());
            EditViewModel edit = new EditViewModel {
                Email = c.EmailAddress,
                FName = c.FirstName,
                LName = c.LastName,
                PhoneNumber = c.PhoneNo,
                IsAdmin = c.IsAdmin
            };
            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, EditViewModel edit)
        {
            try
            {
                Client c = await _cm.FindByIdAsync(id.ToString());
                c.EmailAddress = edit.Email;
                c.FirstName = edit.FName;
                c.LastName = edit.LName;
                c.PhoneNo = edit.PhoneNumber;
                c.UserName = edit.Email;
                c.IsAdmin = edit.IsAdmin;
                await _cm.UpdateAsync(c);
                return RedirectToAction(nameof(EditAsync), id);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task SignInAsync(Client user, bool isPersistent)
        {
            await HttpContext.SignOutAsync(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _cm.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            var claimsPrincipal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            claimsPrincipal.AddIdentity(identity);
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
        }
    }
}