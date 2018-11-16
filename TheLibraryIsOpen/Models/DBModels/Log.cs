using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.DBModels
{
    public class Log
    {
        public int LogID { get; set; }
        public int ClientID { get; set; }
        public int ModelCopyID { get; set; }
        public TransactionType Transaction { get; set; }
        public int ModelID { get; set; }
        public TypeEnum ModelType { get; set; }
        public DateTime TransactionTime { get; set; }

        public Log() { }

        public Log(int clientID, int modelCopyID, TransactionType transaction, DateTime transactionTime)
        {
            ClientID = clientID;
            ModelCopyID = modelCopyID;
            Transaction = transaction;
            TransactionTime = transactionTime;
        }

        public Log(int clientID, int modelCopyID, TransactionType transaction, int modelID, TypeEnum modelType, DateTime transactionTime)
        {
            ClientID = clientID;
            ModelCopyID = modelCopyID;
            Transaction = transaction;
            TransactionTime = transactionTime;
            ModelID = modelID;
            ModelType = modelType;
        }

        public Log(int logID, int clientID, int modelCopyID, TransactionType transaction, DateTime transactionTime)
        {
            LogID = logID;
            ClientID = clientID;
            ModelCopyID = modelCopyID;
            Transaction = transaction;
            TransactionTime = transactionTime;
        }
    }
}
