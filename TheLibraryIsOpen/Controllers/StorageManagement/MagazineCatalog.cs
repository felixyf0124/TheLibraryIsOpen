using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MagazineCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
        private readonly Db _db; // TODO: delete this when db code is removed


        public MagazineCatalog(UnitOfWork unitOfWork, IdentityMap im, Db db)
        {
            _unitOfWork = unitOfWork;
            _im = im;
            _db = db; // TODO: delete this when db code is removed
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

        public async Task<Magazine> FindByIdAsync(string magazineId)
        {
            Magazine magazine = await _im.FindMagazine(int.Parse(magazineId));

            return magazine;
        }

        public Task<Magazine> FindByIsbn10Async(string isbn10)
        {
            if (!string.IsNullOrEmpty(isbn10))
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: replace with _im
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
                    // TODO: replace with _im
                    return _db.GetMagazineByIsbn13(isbn13);
                });
            }
            throw new ArgumentNullException("isbn13");
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
                // TODO: replace with _im
                return _db.GetAllMagazines();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<int> getNoOfAvailableModelCopies(Magazine magazine)
        {
            return Task.Factory.StartNew(() =>
            {

                int AvailableCopies = _db.CountModelCopiesOfModel(magazine.MagazineId, (int)TypeEnum.Magazine, BorrowType.NotBorrowed);

                return AvailableCopies;

            });

        }

        public async Task<List<ModelCopy>> getModelCopies(Magazine magazine)
        {
            List<ModelCopy> copies = await _im.FindModelCopies(magazine.MagazineId, TypeEnum.Magazine);
            
            return copies;

        }

        public Task<IdentityResult> addModelCopy(string id, Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Magazine
                    });
                    System.Diagnostics.Debug.WriteLine(magazine.MagazineId);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "book was null" });
            });
        }
        public Task<IdentityResult> deleteFreeModelCopy(string id, Magazine magazine)
        {
            if (magazine != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false
                    ModelCopy temp = new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Magazine
                    };
                    _im.DeleteFreeModelCopy(temp, Int32.Parse(id));
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Magazine was null" });
            });
        }
    }
}
