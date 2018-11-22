using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
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
                    ModelCopy modelCopy =  _db.GetModelCopyById(log.ModelCopyID);
                    string modelName = "";
                    switch(modelCopy.modelType)
                    {
                        case Constants.TypeConstants.TypeEnum.Book:
                            modelName = _db.GetBookById(modelCopy.modelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Magazine:
                            modelName = _db.GetMagazineById(modelCopy.modelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Movie:
                            modelName = _db.GetMovieById(modelCopy.modelID).Title;
                            break;
                        case Constants.TypeConstants.TypeEnum.Music:
                            modelName = _db.GetMusicById(modelCopy.modelID).Title;
                            break;
                    }
                    Client client = _db.GetClientById(log.ClientID);
                    results.Add(new PrintedLog(client.FirstName + " " +client.LastName, log.Transaction, modelCopy.modelType, modelName, log.ModelCopyID, log.TransactionTime));
                }

                return results;
            });
        }
    }
}
