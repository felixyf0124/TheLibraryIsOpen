using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models;
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

        public Task<List<PrintedLog>> getLogs()
        {
            return Task.Factory.StartNew(() =>
            {
                List<Log> logs = _db.GetAllLogs();
                List<PrintedLog> results = new List<PrintedLog>();
                foreach(Log log in logs)
                {
                    string modelName = "";
                    switch(log.ModelType)
                    {
                        case Constants.TypeConstants.TypeEnum.Book:
                            modelName = _db.GetBookById(log.ModelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Magazine:
                            modelName = _db.GetMagazineById(log.ModelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Movie:
                            modelName = _db.GetMovieById(log.ModelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Music:
                            modelName = _db.GetMusicById(log.ModelID).Title;
                            break;
                    }
                    Client client = _db.GetClientById(log.ClientID);
                    results.Add(new PrintedLog(client.FirstName + " " +client.LastName, log.Transaction, log.ModelType, modelName, log.ModelCopyID, log.TransactionTime));
                }

                return results;
            });
        }
    }
}
