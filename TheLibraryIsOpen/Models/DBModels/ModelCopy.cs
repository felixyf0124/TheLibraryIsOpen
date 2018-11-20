using System;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.DBModels
{
    public class ModelCopy
    {
        public int id { get; set; }
        public TypeEnum modelType { get; set; }
        public int modelID { get; set; }
        public int? borrowerID { get; set; }
        public DateTime? borrowedDate { get; set; }
        public DateTime? returnDate { get; set; }
    }
}
