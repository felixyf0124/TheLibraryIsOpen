using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class ClientStore : IUserStore<Client>
    {
        private readonly DbQuery _db;

        public ClientStore(DbQuery db)
        {
            _db = db;
        }

        public Task CreateAsync(Client user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    //_db.CreateClient(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public Task DeleteAsync(Client user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteClient(int.Parse(user.Id));
                });
            }
            throw new ArgumentNullException("user");
        }

        public void Dispose()
        { }

        public Task<Client> FindByIdAsync(string userId)
        {
            return Task.Factory.StartNew(() =>
            {
                return new Client("", "", "", "", "", "");
                //return _db.GetClientByID(userId);
            });
            throw new ArgumentNullException("userId");
        }
        //input is email
        public Task<Client> FindByNameAsync(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return Task.Factory.StartNew(() =>
                {
                    return new Client("", "", "", "", "", "");
                    //return _db.GetClientByEmail(email);
                });
            }
            throw new ArgumentNullException("userName");
        }

        public Task UpdateAsync(Client user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    //return _db.UpdateClient(user);
                });
            }
            throw new ArgumentNullException("userName");
        }
    }
}
