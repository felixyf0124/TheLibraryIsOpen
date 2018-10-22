using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Models
{
    public class CatalogViewModel
    {
        private Task<List<Book>> Book { get; set;}
        private Task<List<Music>> Music { get; set; }
        private Task<List<DBModels.Movie>> Movie { get; set; }
        private Task<List<Magazine>> Magazine { get; set; }

        public CatalogViewModel(Task<List<Book>> book, Task<List<Music>> music, Task<List<DBModels.Movie>> movie, Task<List<Magazine>> magazine)
        {
            this.Book = book;
            this.Music = music;
            this.Magazine = magazine;
            this.Movie = movie;
        }

    }
}
