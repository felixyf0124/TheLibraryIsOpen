using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class TransactionCatalog
    {
        private readonly Db _db;

        public TransactionCatalog(Db db)
        {
            _db = db;
        }

        public Task<List<Log>> getLogs()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllLogs();
            });
        }
    }
}
