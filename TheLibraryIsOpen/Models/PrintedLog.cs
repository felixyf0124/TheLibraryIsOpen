using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models
{
    public class PrintedLog
    {
        public string ClientName { get; set; }
        public TransactionType Transaction { get; set; }
        public TypeEnum ModelType { get; set; }
        public string ModelName { get; set; }
        public int ModelCopyID { get; set; }
        public DateTime TransactionTime { get; set; }

        public PrintedLog(string clientName, TransactionType transaction, TypeEnum modelType, string modelName, int modelCopyID, DateTime transactionTime)
        {
            ClientName = clientName;
            Transaction = transaction;
            ModelType = modelType;
            ModelName = modelName;
            ModelCopyID = modelCopyID;
            TransactionTime = transactionTime;
        }
    }
}
