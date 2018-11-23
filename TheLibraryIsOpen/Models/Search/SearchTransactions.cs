using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Search
{
    public class SearchTransactions
    {
        private readonly IdentityMap _im;
        private readonly ClientManager _clientManager;
        public SearchTransactions(IdentityMap im, ClientManager clientManager)
        {
            _im = im;
            _clientManager = clientManager;
        }

        public async Task<List<PrintedLog>> SearchLogsAsync(string clientName, string CopyID,
            string type, string ModelId, string dateTime1, string dateTime2,
            bool exactTime, string transac)
        {
            List<PrintedLog> results = new List<PrintedLog>();

            List<List<Log>> logs = new List<List<Log>>();

            List<Task<List<Log>>> logsTasks = new List<Task<List<Log>>>();


            if (!string.IsNullOrEmpty(clientName))
            {
                var clients = await _clientManager.FindClientsByNameAsync(clientName);
                foreach (var client in clients)
                {
                    logsTasks.Add(SearchLogsByClientIDAsync(client.clientId));
                }
            }

            if (!string.IsNullOrEmpty(CopyID))
            {
                logsTasks.Add(SearchLogsByCopyIDAsync(int.Parse(CopyID)));
            }

            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(ModelId))
            {
                Enum.TryParse(type, out TypeEnum m_type);
                logsTasks.Add(SearchLogsByModelTypeAndIdAsync(m_type, int.Parse(ModelId)));
            }

            if (!string.IsNullOrEmpty(dateTime1))
            {
                if (string.IsNullOrEmpty(dateTime2))
                {
                    logsTasks.Add(SearchLogsByDateAsync(DateTime.Parse(dateTime1), exactTime));
                }
                else
                {
                    logsTasks.Add(SearchLogsByPeriodAsync(DateTime.Parse(dateTime1), DateTime.Parse(dateTime2), exactTime));
                }
            }

            if (!string.IsNullOrEmpty(transac))
            {
                Enum.TryParse(transac, out TransactionType t_type);
                logsTasks.Add(SearchLogsByTransactionAsync(t_type));
            }

            foreach (var lt in logsTasks)
            {
                logs.Add(await lt);
            }
            if (logs.Count > 0)
            {
                HashSet<Log> intersection = new HashSet<Log>(logs[0], new SearchTransactionComparer());
                for (int i = 1; i < logs.Count; i++)
                {

                    intersection.IntersectWith(logs[i]);
                }

                var lTasks = new List<Task<PrintedLog>>(intersection.Count);
                lTasks.AddRange(intersection.Select(log => GetPLog(log)));

                foreach (var lTask in lTasks)
                {
                    results.Add(await lTask);
                }
            }
                return results;
            
            async Task<PrintedLog> GetPLog(Log log)
            {
                ModelCopy modelCopy = await _im.FindModelCopy(log.ModelCopyID).ConfigureAwait(false);
                string modelName = "";
                switch (modelCopy.modelType)
                {
                    case TypeEnum.Book:
                        modelName = (await _im.FindBook(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Magazine:
                        modelName = (await _im.FindMagazine(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Movie:
                        modelName = (await _im.FindMovie(modelCopy.modelID))?.Title;
                        break;
                    case TypeEnum.Music:
                        modelName = (await _im.FindMusic(modelCopy.modelID))?.Title;
                        break;
                }

                Client client = await _clientManager.FindByIdAsync(log.ClientID.ToString());
                return new PrintedLog(client.FirstName + " " + client.LastName, log.Transaction,
                    modelCopy.modelType, modelName, log.ModelCopyID, log.TransactionTime);
            }
        }

        private Task<List<Log>> SearchLogsByDateAsync(DateTime searchDate, bool exact)
        {
            return _im.FindLogsByDate(searchDate, exact);
        }

        private Task<List<Log>> SearchLogsByPeriodAsync(DateTime startDate, DateTime endDate, bool exact)
        {
            return _im.FindLogsByPeriod(startDate, endDate, exact);
        }

        private Task<List<Log>> SearchLogsByModelTypeAndIdAsync(TypeEnum type, int id)
        {
            return _im.FindLogsByModelTypeAndId(type, id);
        }

        private Task<List<Log>> SearchLogsByClientIDAsync(int id)
        {
            return _im.FindLogsByClientID(id);
        }

        private Task<List<Log>> SearchLogsByCopyIDAsync(int id)
        {
            return _im.FindLogsByCopyID(id);
        }

        private Task<List<Log>> SearchLogsByTransactionAsync(TransactionType transac)
        {
            return _im.FindLogsByTransaction(transac);
        }



        public class SearchTransactionComparer : IEqualityComparer<Log>
        {
            public bool Equals(Log x, Log y)
            {
                return (x.LogID == y.LogID);
            }

            public int GetHashCode(Log log)
            {

                return log.LogID.GetHashCode();
            }
        }


    }
}
