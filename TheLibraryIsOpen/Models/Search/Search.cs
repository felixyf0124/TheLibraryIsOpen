using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Models.Search
{
    public sealed class Search
    {
        private readonly Db _db;

        public Search(Db db)
        {
            _db = db;
        }


        public Task<List<SearchResult>> SearchAllAsync(string searchString)
        {
            throw new NotImplementedException();

        }

        public Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();

                List<Book> books = new List<Book>();
                foreach (Book book in _db.SearchBooksByIsbn10(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByIsbn13(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByTitle(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByAuthor(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByFormat(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByPages(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByPublisher(searchString)) { if (!books.Contains(book)) books.Add(book); }
                foreach (Book book in _db.SearchBooksByLanguage(searchString)) { if (!books.Contains(book)) books.Add(book); }

                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });

        }
        public Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        public Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        public Task<List<SearchResult>> SearchMusicAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        #region books

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByIsbn10(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksIsbn13Async(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByIsbn13(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;

            });
        }

        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByTitle(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByAuthor(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByFormat(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksPagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByPages(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByPublisher(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {

                List<SearchResult> results = new List<SearchResult>();
                List<Book> books = _db.SearchBooksByLanguage(searchString);
                foreach (Book book in books)
                {
                    string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                    SearchResult result = new SearchResult(0, book.BookId, book.Title, resultDescription);
                    results.Add(result);
                }
                return results;
            });
        }

        #endregion

        #region magazines

        private Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesPublishersAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesLanguagesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn10Async(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn13Async(string searhString)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region movies

        private Task<List<SearchResult>> SearchMoviesTitlesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesDirectorsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesProducersAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesActorsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesLanguagesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesSubtitlesAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchMoviesDubbedAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesReleaseDateAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesRunTimeAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region music

        private Task<List<SearchResult>> SearchMusicTypesAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchMusicTitlesAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchMusicArtistsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMusicLabelsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMusicReleaseDateAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchMusicAsinAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
