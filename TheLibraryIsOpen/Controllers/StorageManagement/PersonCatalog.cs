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
    public class PersonCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
        private readonly Db _db; // TODO: delete this when db code is removed


        public PersonCatalog(UnitOfWork unitOfWork, IdentityMap im, Db db)
        {
            _unitOfWork = unitOfWork;
            _im = im;
            _db = db; // TODO: delete this when db code is removed
        }

        //Create Person
        public Task<IdentityResult> CreateAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register return false
                    _unitOfWork.RegisterNew(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "person was null" });
            });
        }


        //Delete Magazine
        public Task<IdentityResult> DeleteAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDeleted(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "person was null" });
            });
        }

        public void Dispose()
        { }


        //Find methods (by id, isbn10, isbn13)

        public async Task<Person> FindByIdAsync(string personID)
        {
            Person person = await _im.FindPerson(int.Parse(personID));

            return person;
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


        public Task<IdentityResult> UpdateAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDirty(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "person was null" });
            });
        }


        //Get all person
        public Task<List<Person>> GetAllPersonDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: replace with _im
                return _db.GetAllPerson();
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

        public Task<IdentityResult> addModelCopy(string id)
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
        public Task<IdentityResult> deleteFreeModelCopy(string id)
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
