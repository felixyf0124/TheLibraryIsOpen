using System;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Cart
{
    public class CartViewModel
    {
        public int ModelId { get; set; }
        public string Title { get; set; }
        public TypeEnum Type { get; set; }

    }
}
