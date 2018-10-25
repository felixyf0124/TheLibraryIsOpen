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


        public MagazineCatalog(UnitOfWork unitOfWork, Db db)
        {
            _unitOfWork = unitOfWork;
            _db = db; // TODO: delete this when db code is removed

        }

        //Create Magazine
        public Task<IdentityResult> CreateAsync(Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {

                    // TODO: find if magazine already exists

                    bool registered = _unitOfWork.RegisterNew(magazine);

                    // ? Not sure what error to return here
                    if (registered == false)
                        return IdentityResult.Failed(new IdentityError { Description = "cannot add magazine" });
                    // if (_db.GetMagazineByIsbn10(magazine.Isbn10) != null)
                    //     return IdentityResult.Failed(new IdentityError { Description = "magazine with this isbn10 already exists" });
                    // _db.CreateMagazine(magazine);
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
                    bool registered = _unitOfWork.RegisterDelete(magazine);

                    // ? not sure what error to return here 
                    if (registered == false)
                        return IdentityResult.Failed(new IdentityError { Description = "cannot delete magazine" });
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
                return _db.GetMagazineById(int.Parse(magazineId));
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
