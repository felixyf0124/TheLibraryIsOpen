using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{

    public class ClientManager : UserManager<Client>
    {
        public ClientManager(ClientStore store) : base(store)
        {
        }
    }
}
