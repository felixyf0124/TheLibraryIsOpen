using System.Collections.Generic;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Models
{
    public class CatalogViewModel
    {
        public List<Book> Book { get; set;}
        public List<Music> Music { get; set; }
        public List<DBModels.Movie> Movie { get; set; }
        public List<Magazine> Magazine { get; set; }

        public CatalogViewModel(List<Book> book, List<Music> music, List<DBModels.Movie> movie, List<Magazine> magazine)
        {
            this.Book = book;
            this.Music = music;
            this.Magazine = magazine;
            this.Movie =  movie;
        }
    }
}




