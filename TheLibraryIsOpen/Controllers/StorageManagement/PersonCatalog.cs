using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db; 
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class PersonCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
      

        public PersonCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _im = im;
          
        }

        //Create Person
        public Task<IdentityResult> CreateAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterNew(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "person was null" });
            });
        }


        //Delete Person
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
        //Find methods (by id)

        public async Task<Person> FindByIdAsync(string personID)
        {
            Person person = await _im.FindPerson(int.Parse(personID));

            return person;
        }

        //Update methods
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
                return _im.GetAllPerson();
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }
    }
}
