using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MagazineCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;


        public MagazineCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
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
                    _unitOfWork.RegisterDeleted(magazine);
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
                    return _im.GetMagazineByIsbn10(isbn10);
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
                    return _im.GetMagazineByIsbn13(isbn13);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                    _unitOfWork.RegisterDirty(magazine);
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
                return _im.GetAllMagazines();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

    }
}
