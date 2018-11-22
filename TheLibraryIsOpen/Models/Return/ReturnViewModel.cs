using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Return
{
    public class ReturnViewModel
    {
        public int ModelId { get; set; }
        public string Title { get; set; }
        public int ModelCopyId { get; set; }
        public TypeEnum ModelType { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool ToReturn { get; set; } = false;

    }
}