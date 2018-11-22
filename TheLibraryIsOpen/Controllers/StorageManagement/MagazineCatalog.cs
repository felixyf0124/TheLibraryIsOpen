using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db; // TODO: delete this when db code is removed
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
        
        //Find methods (by id, isbn10, isbn13)

        public async Task<Magazine> FindByIdAsync(string magazineId)
        {
            Magazine magazine = await _im.FindMagazine(int.Parse(magazineId));

            return magazine;
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

        public Task<int> GetNoOfAvailableModelCopies(Magazine magazine)
        {
            return Task.Factory.StartNew(() =>
            {

                int AvailableCopies = _db.CountModelCopiesOfModel(magazine.MagazineId, (int)TypeEnum.Magazine, BorrowType.NotBorrowed);

                return AvailableCopies;

            });

        }

        public Task<IdentityResult> AddModelCopy(string id)
        {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Magazine
                    });
                    return IdentityResult.Success;
                });
        }
        public Task<IdentityResult> DeleteFreeModelCopy(string id)
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
    }
}
