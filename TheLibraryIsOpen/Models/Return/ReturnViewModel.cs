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
        public TypeEnum Type { get; set; }

    }
}
