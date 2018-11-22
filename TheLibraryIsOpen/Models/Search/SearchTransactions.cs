using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Search
{
    public class SearchTransactions
    {
        private readonly IdentityMap _im;
        private readonly Db _db;
        public SearchTransactions(Db db, IdentityMap im)
        {
            _db = db;
            _im = im;
        }

        public async Task<List<PrintedLog>> SearchLogsAsync(string clientID, string CopyID,
            string type, string ModelId, string dateTime1, string dateTime2, 
            bool exactTime, string transac)
        {
            List<PrintedLog> results = new List<PrintedLog>();

            List<List<Log>> Logs = new List<List<Log>>();


            if (clientID != "")
            {
                List<Log> temp = await SearchLogsByClientIDAsync(int.Parse(clientID));
                if(temp != null)
                    Logs.Add(temp);
            }

            if (CopyID != "")
            {
                List<Log> temp = await SearchLogsByCopyIDAsync(int.Parse(CopyID));
                if (temp != null)
                    Logs.Add(temp);
            }

            if (type != "" && ModelId != "")
            {
                Enum.TryParse(type, out TypeEnum m_type);
                List<Log> temp = await SearchLogsByModelTypeAndIdAsync(m_type, int.Parse(ModelId));
                if (temp != null)
                    Logs.Add(temp);
            }

            if (dateTime1 != "")
            {
                if(dateTime2 == "")
                {
                    List<Log> temp = await SearchLogsByDateAsync(DateTime.Parse(dateTime1), exactTime);
                    if (temp != null)
                        Logs.Add(temp);
                }
                else
                {
                    List<Log> temp = await SearchLogsByPeriodAsync(DateTime.Parse(dateTime1), DateTime.Parse(dateTime2), exactTime);
                    if (temp != null)
                        Logs.Add(temp);
                }
            }

            if (transac != "")
            {
                Enum.TryParse(transac, out TransactionType t_type);
                List<Log> temp = await SearchLogsByTransactionAsync(t_type);
                if (temp != null)
                    Logs.Add(temp);
            }
            
            IEnumerable<Log> intersection = Logs[0];
            for(int i = 1; i < Logs.Count; i++)
            {
                List<Log> temp = Logs[i];
                intersection = intersection.Intersect(Logs[i], new SearchTransactionComparer());
            }
            //results = intersection.ToList();
            foreach (Log log in intersection.ToList())
            {
                ModelCopy modelCopy = await _im.FindModelCopy(log.ModelCopyID);
                string modelName = "";
                switch (modelCopy.modelType)
                {
                    case Constants.TypeConstants.TypeEnum.Book:
                        modelName = (await _im.FindBook(modelCopy.modelID)).Title;
                        break;
                    case Constants.TypeConstants.TypeEnum.Magazine:
                        modelName = (await _im.FindMagazine(modelCopy.modelID)).Title;
                        break;
                    case Constants.TypeConstants.TypeEnum.Movie:
                        modelName = (await _im.FindMovie(modelCopy.modelID)).Title;
                        break;
                    case Constants.TypeConstants.TypeEnum.Music:
                        modelName = (await _im.FindMusic(modelCopy.modelID)).Title;
                        break;
                }
                Client client = _db.GetClientById(log.ClientID);
                results.Add(new PrintedLog(client.FirstName + " " + client.LastName, log.Transaction, modelCopy.modelType, modelName, log.ModelCopyID, log.TransactionTime));
            }
               return results;
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
