using System;
using System.Collections.Generic;
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

        public Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {
            throw new NotImplementedException();

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

        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksPagesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searhString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searhString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksIsbn13Async(string searhString)
        {
            throw new NotImplementedException();
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

        //// Not available
        //private Task<List<SearchResult>> SearchMoviesProducersAsync(string searhString)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task<List<SearchResult>> SearchMoviesActorsAsync(string searhString)
        //{
        //    throw new NotImplementedException();
        //}
        //// Not available

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
    }
}
