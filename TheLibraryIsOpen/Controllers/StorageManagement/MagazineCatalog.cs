using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed


namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MagazineCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Db _db; // TODO: delete this when db code is removed
        private readonly IdentityMap _im;


        public MagazineCatalog(UnitOfWork unitOfWork, Db db, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _db = db; // TODO: delete this when db code is removed
            _im = im;
        }

        //Create Magazine
        public Task<IdentityResult> CreateAsync(Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register return false
                    _unitOfWork.RegisterNew(magazine);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "magazine was null" });
            });
        }


        //Delete Magazine
        public Task<IdentityResult> DeleteAsync(Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // _unitOfWork.RegisterDelete(magazine);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "magazine was null" });
            });
        }

        public void Dispose()
        { }


        //Find methods (by id, isbn10, isbn13)

        public Task<Magazine> FindByIdAsync(string magazineId)
        {
            return Task.Factory.StartNew(() =>
            {
                Magazine magazine = _im.FindMagazine(int.Parse(magazineId));
                
                return magazine;
            });
            throw new ArgumentNullException("magazineId");
        }

        public Task<Magazine> FindByIsbn10Async(string isbn10)
        {
            if (!string.IsNullOrEmpty(isbn10))
            {
                return Task.Factory.StartNew(() =>
                {
                    return _db.GetMagazineByIsbn10(isbn10);
                });
            }
            throw new ArgumentNullException("isbn10");
        }

        public Task<Magazine> FindByIsbn13Async(string isbn13)
        {
            if (!string.IsNullOrEmpty(isbn13))
            {
                return Task.Factory.StartNew(() =>
                {
                    return _db.GetMagazineByIsbn13(isbn13);
                });
            }
            throw new ArgumentNullException("isbn13");
        }


        //Update Methods (per attribute, general)

        public Task SetTitleAsync(Magazine magazine, string title)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Title = title;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }


        public Task SetPublisherAsync(Magazine magazine, string publisher)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Publisher = publisher;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }

        public Task SetLanguageAsync(Magazine magazine, string language)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Language = language;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }

        public Task SetDateAsync(Magazine magazine, string date)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Date = date;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }

        public Task SetIsbn10Async(Magazine magazine, string isbn10)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Isbn10 = isbn10;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }

        public Task SetIsbn13Async(Magazine magazine, string isbn13)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    magazine.Isbn13 = isbn13;
                    _db.UpdateMagazine(magazine);
                });
            }
            throw new ArgumentNullException("magazine");
        }

        public Task<IdentityResult> UpdateAsync(Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdateMagazine(magazine);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "magazine was null" });
            });
        }


        //Get all Magazines
        public Task<List<Magazine>> GetAllMagazinesDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMagazines();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

    }
}
