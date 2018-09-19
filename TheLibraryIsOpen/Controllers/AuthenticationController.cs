using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Models.Authentication;

namespace TheLibraryIsOpen.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DbQuery _db; //gonna change this to the actual thing we're gonna use. (Catalog, UoW, etc.)

        public AuthenticationController(DbQuery db) //gonna change this to the actual thing we're gonna use (Catalog, UoW, etc.)
        {
            _db = db;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection collection)
        {
            try
            {
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
        
        public ActionResult Logout()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception();

                //TODO: add client to db.
                //authenticate client via cookie or whatever

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditViewModel edit)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Edit), id);
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
    }
}