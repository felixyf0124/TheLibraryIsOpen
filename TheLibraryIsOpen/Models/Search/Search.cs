using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();
            results.AddRange(await SearchBooksIsbn10Async(searchString));
            results.AddRange(await SearchBooksIsbn13Async(searchString));
            results.AddRange(await SearchBooksTitlesAsync(searchString));
            results.AddRange(await SearchBooksAuthorsAsync(searchString));
            results.AddRange(await SearchBooksFormatsAsync(searchString));
            results.AddRange(await SearchBooksPagesAsync(searchString));
            results.AddRange(await SearchBooksPublishersAsync(searchString));
            results.AddRange(await SearchBooksLanguagesAsync(searchString));
            return results.Distinct(new SearchResultComparer()).ToList();
        }

        public Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        public Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SearchResult>> SearchMusicAsync(string searchString)
        {

            List<SearchResult> results = new List<SearchResult>();

            results.AddRange(await SearchMusicTypesAsync(searchString));
            results.AddRange(await SearchMusicTitlesAsync(searchString));
            results.AddRange(await SearchMusicArtistsAsync(searchString));
            results.AddRange(await SearchMusicReleaseDateAsync(searchString));
            results.AddRange(await SearchMusicLabelsAsync(searchString));
            results.AddRange(await SearchMusicAsinAsync(searchString));

            return results.Distinct().ToList();
        }

        #region books

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByIsbn10(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksIsbn13Async(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByIsbn13(searchString));

            });
        }

        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByTitle(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByAuthor(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByFormat(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksPagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByPages(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByPublisher(searchString));
            });
        }

        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() => {
                return ConvertBooksToSearchResults(_db.SearchBooksByLanguage(searchString));
            });
        }

        private List<SearchResult> ConvertBooksToSearchResults(List<Book> books) {
            List<SearchResult> results = new List<SearchResult>();
            foreach (Book book in books)
            {
                string[] resultDescription = { book.Author, book.Format, book.Pages.ToString(), book.Publisher, book.Date, book.Language, book.Isbn10, book.Isbn13 };
                SearchResult result = new SearchResult(Constants.TypeConstants.TypeEnum.Book, book.BookId, book.Title, resultDescription);
                results.Add(result);
            }
            return results;
        }

        #endregion

        #region magazines

        private Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesPublishersAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesLanguagesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn10Async(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn13Async(string searchString)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region movies

        private Task<List<SearchResult>> SearchMoviesTitlesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesDirectorsAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesProducersAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesActorsAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesLanguagesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesSubtitlesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchMoviesDubbedAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesReleaseDateAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchMoviesRunTimeAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region music

        private Task<List<SearchResult>> SearchMusicTypesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByType(searchString);

                return MusicListToSearchResultList(results);
            });
        }
        private Task<List<SearchResult>> SearchMusicTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByTitle(searchString);

                return MusicListToSearchResultList(results);
            });
        }
        private Task<List<SearchResult>> SearchMusicArtistsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByArtist(searchString);

                return MusicListToSearchResultList(results);
            });
        }

        private Task<List<SearchResult>> SearchMusicLabelsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByLabel(searchString);

                return MusicListToSearchResultList(results);
            });
        }

        private Task<List<SearchResult>> SearchMusicReleaseDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByReleaseDate(searchString);

                return MusicListToSearchResultList(results);
            });
        }
        private Task<List<SearchResult>> SearchMusicAsinAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Music> results = _db.SearchMusicByASIN(searchString);

                return MusicListToSearchResultList(results);
            });
        }

        private List<SearchResult> MusicListToSearchResultList(List<Music> music)
        {
            List<SearchResult> sr = new List<SearchResult>();

            foreach (Music item in music)
            {
                string[] description =
                {
                    "Released in " + item.ReleaseDate,
                    "\nPerformed by " + item.Artist,
                    "\nProduced by" + item.Label,
                    "\nASIN: " + item.Asin
                };
                sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, item.MusicId, item.Title, description));
            }

            return sr;
        }
        #endregion

        private class SearchResultComparer : IEqualityComparer<SearchResult>
        {
            public bool Equals(SearchResult x, SearchResult y) 
            {
                return (x.ModelId == y.ModelId && x.Type == y.Type);
            }

            public int GetHashCode(SearchResult obj) 
            {
                return $"{obj.Type}-{obj.ModelId}".GetHashCode();
            }
        }
    }
}
