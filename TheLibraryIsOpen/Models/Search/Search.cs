using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Search
{
    public sealed class Search
    {
        private readonly Db _db;

        public Search(Db db)
        {
            _db = db;
        }


        public async Task<List<SearchResult>> SearchAllAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();

            var books = SearchBooksAsync(searchString);
            var magazines = SearchMagazinesAsync(searchString);
            var movies = SearchMoviesAsync(searchString);
            var musics = SearchMusicAsync(searchString);
            results.AddRange(await books);
            results.AddRange(await magazines);
            results.AddRange(await movies);
            results.AddRange(await musics);

            return results;
        }

        public async Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();

            var b1 = SearchBooksIsbn10Async(searchString);
            var b2 = SearchBooksIsbn13Async(searchString);
            var b3 = SearchBooksTitlesAsync(searchString);
            var b4 = SearchBooksAuthorsAsync(searchString);
            var b5 = SearchBooksFormatsAsync(searchString);
            var b6 = SearchBooksPagesAsync(searchString);
            var b7 = SearchBooksPublishersAsync(searchString);
            var b8 = SearchBooksDateAsync(searchString);
            var b9 = SearchBooksLanguagesAsync(searchString);

            results.AddRange(await b1);
            results.AddRange(await b2);
            results.AddRange(await b3);
            results.AddRange(await b4);
            results.AddRange(await b5);
            results.AddRange(await b6);
            results.AddRange(await b7);
            results.AddRange(await b8);
            results.AddRange(await b9);

            return results.Distinct(new SearchResultComparer()).ToList();
        }
        public async Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();

            var m1 = SearchMagazinesTitlesAsync(searchString);
            var m2 = SearchMagazinesPublishersAsync(searchString);
            var m3 = SearchMagazinesLanguagesAsync(searchString);
            var m4 = SearchMagazinesDateAsync(searchString);
            var m5 = SearchMagazinesIsbn10Async(searchString);
            var m6 = SearchMagazinesIsbn13Async(searchString);

            results.AddRange(await m1);
            results.AddRange(await m2);
            results.AddRange(await m3);
            results.AddRange(await m4);
            results.AddRange(await m5);
            results.AddRange(await m6);

            return results.Distinct(new SearchResultComparer()).ToList();

        }
        public async Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();

            var m1 = SearchMoviesTitleAsync(searchString);
            var m2 = SearchMoviesDirectorAsync(searchString);
            var m3 = SearchMoviesLanguageAsync(searchString);
            var m5 = SearchMoviesSubtitlesAsync(searchString);
            var m6 = SearchMoviesDubbedAsync(searchString);
            var m7 = SearchMoviesReleaseDateAsync(searchString);
            var m8 = SearchMoviesRunTimeAsync(searchString);
            var m9 = SearchMoviesProducersAsync(searchString);
            var m10 = SearchMoviesActorsAsync(searchString);

            results.AddRange(await m1);
            results.AddRange(await m2);
            results.AddRange(await m3);
            results.AddRange(await m5);
            results.AddRange(await m6);
            results.AddRange(await m7);
            results.AddRange(await m8);
            results.AddRange(await m9);
            results.AddRange(await m10);

            return results.Distinct(new SearchResultComparer()).ToList();
        }
        public async Task<List<SearchResult>> SearchMusicAsync(string searchString)
        {
            List<SearchResult> results = new List<SearchResult>();

            var m1 = SearchMusicTypesAsync(searchString);
            var m2 = SearchMusicTitlesAsync(searchString);
            var m3 = SearchMusicArtistsAsync(searchString);
            var m4 = SearchMusicReleaseDateAsync(searchString);
            var m5 = SearchMusicLabelsAsync(searchString);
            var m6 = SearchMusicAsinAsync(searchString);

            results.AddRange(await m1);
            results.AddRange(await m2);
            results.AddRange(await m3);
            results.AddRange(await m4);
            results.AddRange(await m5);
            results.AddRange(await m6);

            return results.Distinct(new SearchResultComparer()).ToList();
        }

        #region books

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByIsbn10(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksIsbn13Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByIsbn13(searchString));

            });
        }
        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByTitle(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByAuthor(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByFormat(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksPagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByPages(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByPublisher(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByDate(searchString));
            });
        }
        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertBooksToSearchResults(_db.SearchBooksByLanguage(searchString));
            });
        }

        #endregion

        #region magazines
        
        private Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by title
                List<Magazine> results = _db.SearchMagazinesByTitle(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMagazinesPublishersAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by publisher
                List<Magazine> results = _db.SearchMagazinesByPublisher(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMagazinesLanguagesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by language
                List<Magazine> results = _db.SearchMagazinesByLanguage(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMagazinesDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by date
                List<Magazine> results = _db.SearchMagazinesByDate(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMagazinesIsbn10Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by isbn 10
                List<Magazine> results = _db.SearchMagazinesByIsbn10(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMagazinesIsbn13Async(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding magazines by isbn 13
                List<Magazine> results = _db.SearchMagazinesByIsbn13(searchString);

                return ConvertMagazinesToSearchResults(results);
            });
        }

        #endregion

        #region movies

        private Task<List<SearchResult>> SearchMoviesTitleAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by title
                List<DBModels.Movie> results = _db.SearchMoviesByTitle(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesDirectorAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by director
                List<DBModels.Movie> results = _db.SearchMoviesByDirector(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesProducersAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by producer
                List<DBModels.Movie> results = _db.SearchMoviesByProducer(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesActorsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by actor
                List<DBModels.Movie> results = _db.SearchMoviesByActor(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesLanguageAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by language
                List<DBModels.Movie> results = _db.SearchMoviesByLanguage(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesSubtitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by subtitle
                List<DBModels.Movie> results = _db.SearchMoviesBySubtitles(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesDubbedAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by subtitle
                List<DBModels.Movie> results = _db.SearchMoviesByDubbed(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesReleaseDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by release date
                List<DBModels.Movie> results = _db.SearchMoviesByReleasedate(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }
        private Task<List<SearchResult>> SearchMoviesRunTimeAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                //Retrieve all the corresponding movies by run time
                List<DBModels.Movie> results = _db.SearchMoviesByRuntime(searchString);

                return ConvertMoviesToSearchResults(results);
            });
        }

        #endregion

        #region music

        private Task<List<SearchResult>> SearchMusicTypesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByType(searchString));
            });
        }
        private Task<List<SearchResult>> SearchMusicTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByTitle(searchString));
            });
        }
        private Task<List<SearchResult>> SearchMusicArtistsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByArtist(searchString));
            });
        }
        private Task<List<SearchResult>> SearchMusicLabelsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByLabel(searchString));
            });
        }
        private Task<List<SearchResult>> SearchMusicReleaseDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByReleaseDate(searchString));
            });
        }
        private Task<List<SearchResult>> SearchMusicAsinAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                return ConvertMusicsToSearchResults(_db.SearchMusicByASIN(searchString));
            });
        }

        #endregion

        #region modelToSearchResult
        private List<SearchResult> ConvertBooksToSearchResults(List<Book> books)
        {
            List<SearchResult> results = new List<SearchResult>();
            foreach (Book book in books)
            {
                string[] resultDescription = {
                    "Author: " , book.Author,
                    "Publisher: " , book.Publisher,
                    "Date: " , book.Date.ToShortDateString(),
                    "Language: " , book.Language,
                    "Format: " , book.Format,
                    "Pages: " , book.Pages.ToString(),
                    "ISBN-10: " , book.Isbn10,
                    "ISBN-13: " , book.Isbn13
                };
                SearchResult result = new SearchResult(TypeEnum.Book, book.BookId, book.Title, book.Date, resultDescription);
                results.Add(result);
            }
            return results;
        }

        private List<SearchResult> ConvertMagazinesToSearchResults(List<Magazine> results)
        {
            //Initialization of a new list of search result
            List<SearchResult> convertedResult = new List<SearchResult>();

            foreach (Magazine magazine in results)
            {
                string[] description = {
                        "Publisher: " , magazine.Publisher,
                        "Date: ", magazine.Date.ToShortDateString(),
                        "Language: " , magazine.Language,
                        "ISBN-10: " , magazine.Isbn10,
                        "ISBN-13: " , magazine.Isbn13
                    };
                convertedResult.Add(new SearchResult(TypeEnum.Magazine, magazine.MagazineId, magazine.Title, magazine.Date, description));
            }

            return convertedResult;
        }

        private List<SearchResult> ConvertMoviesToSearchResults(List<DBModels.Movie> results)
        {
            //Initialization of a new list of search result
            List<SearchResult> convertedResult = new List<SearchResult>();

            foreach (DBModels.Movie movie in results)
            {
                string[] description = {
                    "Director: " , movie.Director,
                    "Date: " , movie.ReleaseDate.ToShortDateString(),
                    "Language: " , movie.Language,
                    "Subtitles: " , movie.Subtitles,
                    "Dubbed: " , movie.Dubbed,
                    "RunTime: " , movie.RunTime
                };
                convertedResult.Add(new SearchResult(TypeEnum.Movie, movie.MovieId, movie.Title, movie.ReleaseDate, description));
            }

            return convertedResult;
        }

        private List<SearchResult> ConvertMusicsToSearchResults(List<Music> music)
        {
            List<SearchResult> sr = new List<SearchResult>();

            foreach (Music item in music)
            {
                string[] description =
                {
                    "Artist: " , item.Artist,
                    "Producer: " , item.Label,
                    "Date: " , item.ReleaseDate.ToShortDateString(),
                    "ASIN: " , item.Asin
                };
                sr.Add(new SearchResult(TypeEnum.Music, item.MusicId, item.Title, item.ReleaseDate, description));
            }

            return sr;
        }
        #endregion

        public class SearchResultComparer : IEqualityComparer<SearchResult>
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
