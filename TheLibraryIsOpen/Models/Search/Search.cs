using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Constants;

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

        public async Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();
            results.AddRange(await SearchMagazinesTitlesAsync(searchString));
            results.AddRange(await SearchMagazinesPublishersAsync(searchString));
            results.AddRange(await SearchMagazinesLanguagesAsync(searchString));
            results.AddRange(await SearchMagazinesDateAsync(searchString));
            results.AddRange(await SearchMagazinesIsbn10Async(searchString));
            results.AddRange(await SearchMagazinesIsbn13Async(searchString));
            return results.Distinct(new SearchResultComparer()).ToList();

        }
        public async Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();
            results.AddRange(await SearchMoviesTitleAsync(searchString));
            results.AddRange(await SearchMoviesDirectorAsync(searchString));
            results.AddRange(await SearchMoviesLanguageAsync(searchString));
            results.AddRange(await SearchMoviesSubtitlesAsync(searchString));
            results.AddRange(await SearchMoviesDubbedAsync(searchString));
            results.AddRange(await SearchMoviesReleaseDateAsync(searchString));
            results.AddRange(await SearchMoviesRunTimeAsync(searchString));
            return results.Distinct(new SearchResultComparer()).ToList();
        }
        public Task<List<SearchResult>> SearchMusicAsync(string searchString)
        {
            throw new NotImplementedException();
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

        private List<SearchResult> MagazineToSearchResult(List<Magazine> results)
        {
            //Initialization of a new list of search result
            List<SearchResult> convertedResult = new List<SearchResult>();

            foreach (Magazine magazine in results)
            {
                string[] description = {
                        "title:" + magazine.Title,
                        "publisher:" + magazine.Publisher,
                        "language:" + magazine.Language,
                        "date:"+ magazine.Date,
                        "isbn10:" + magazine.Isbn10,
                        "isbn13" + magazine.Isbn13
                    };
                convertedResult.Add(new SearchResult(TypeConstants.TypeEnum.Magazine, magazine.MagazineId, magazine.Title, description));
            }

            return convertedResult;
        }

        private Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by title
                List<Magazine> results = _db.SearchMagazinesByTitle(searchString);

                return MagazineToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMagazinesPublishersAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by publisher
                List<Magazine> results = _db.SearchMagazinesByPublisher(searchString);

                return MagazineToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMagazinesLanguagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by language
                List<Magazine> results = _db.SearchMagazinesByLanguage(searchString);

                return MagazineToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMagazinesDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by date
                List<Magazine> results = _db.SearchMagazinesByDate(searchString);

                return MagazineToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn10Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by isbn 10
                List<Magazine> results = _db.SearchMagazinesByIsbn10(searchString);

                return MagazineToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMagazinesIsbn13Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by isbn 13
                List<Magazine> results = _db.SearchMagazinesByIsbn13(searchString);

                return MagazineToSearchResult(results);
            });
        }

        #endregion
        #region movies

        private List<SearchResult> MovieToSearchResult(List<DBModels.Movie> results)
        {
            //Initialization of a new list of search result
            List<SearchResult> convertedResult = new List<SearchResult>();

            foreach (DBModels.Movie movie in results)
            {
                string[] description = {
                    "title:" + movie.Title,
                    "director:" + movie.Director,
                    "language:" + movie.Language,
                    "subtitles:" + movie.Subtitles,
                    "dubbed:" + movie.Dubbed,
                    "releaseDate:" + movie.ReleaseDate,
                    "runTime:" + movie.RunTime
                };
                convertedResult.Add(new SearchResult(TypeConstants.TypeEnum.Movie, movie.MovieId, movie.Title, description));
            }

            return convertedResult;
        }

        private Task<List<SearchResult>> SearchMoviesTitleAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by title
                List<DBModels.Movie> results = _db.SearchMoviesByTitle(searchString);

                return MovieToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMoviesDirectorAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by director
                List<DBModels.Movie> results = _db.SearchMoviesByDirector(searchString);

                return MovieToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMoviesLanguageAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by language
                List<DBModels.Movie> results = _db.SearchMoviesByLanguage(searchString);

                return MovieToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMoviesSubtitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by subtitle
                List<DBModels.Movie> results = _db.SearchMoviesBySubtitles(searchString);

                return MovieToSearchResult(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesDubbedAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by subtitle
                List<DBModels.Movie> results = _db.SearchMoviesByDubbed(searchString);

                return MovieToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMoviesReleaseDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by release date
                List<DBModels.Movie> results = _db.SearchMoviesByReleasedate(searchString);

                return MovieToSearchResult(results);
            });
        }

        private Task<List<SearchResult>> SearchMoviesRunTimeAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by run time
                List<DBModels.Movie> results = _db.SearchMoviesByRuntime(searchString);

                return MovieToSearchResult(results);
            });
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
