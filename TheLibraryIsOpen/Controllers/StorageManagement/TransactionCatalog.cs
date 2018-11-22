using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class TransactionCatalog
    {
        private readonly IdentityMap _identityMap;
        private readonly ClientManager _clientManager;

        public TransactionCatalog(IdentityMap identityMap, ClientManager clientManager)
        {
            _identityMap = identityMap;
            _clientManager = clientManager;
        }

        public async Task<List<PrintedLog>> GetLogs()
        {
            var logs = await _identityMap.GetAllLogs();
            List<PrintedLog> results = new List<PrintedLog>();
            List<Task<PrintedLog>> tasker = new List<Task<PrintedLog>>(logs.Count);
            foreach (var log in logs)
            {
                tasker.Add((GetPLog(log)));
            }

            foreach (var task in tasker)
            {
                results.Add(await task);
            }

            return results;

            async Task<PrintedLog> GetPLog(Log log)
            {
                ModelCopy modelCopy = await _identityMap.FindModelCopy(log.ModelCopyID).ConfigureAwait(false);
                string modelName = "";
                switch (modelCopy.modelType)
                {
                    case TypeEnum.Book:
                        modelName = (await _identityMap.FindBook(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Magazine:
                        modelName = (await _identityMap.FindMagazine(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Movie:
                        modelName = (await _identityMap.FindMovie(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Music:
                        modelName = (await _identityMap.FindMusic(modelCopy.modelID))?.Title;
                        break;
                }

                Client client = await _clientManager.FindByIdAsync(log.ClientID.ToString());
                return new PrintedLog(client.FirstName + " " + client.LastName, log.Transaction,
                    modelCopy.modelType, modelName, log.ModelCopyID, log.TransactionTime);
            }
        }
    }
}
