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

        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksPagesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searchString)
        {
            throw new NotImplementedException();
        }
        private Task<List<SearchResult>> SearchBooksIsbn13Async(string searchString)
        {
            throw new NotImplementedException();
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
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByType(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }
        private Task<List<SearchResult>> SearchMusicTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByTitle(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }
        private Task<List<SearchResult>> SearchMusicArtistsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByArtist(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }

        private Task<List<SearchResult>> SearchMusicLabelsAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByLabel(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }

        private Task<List<SearchResult>> SearchMusicReleaseDateAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByReleaseDate(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }
        private Task<List<SearchResult>> SearchMusicAsinAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> sr = new List<SearchResult>();

                List<Music> results = _db.SearchMusicByASIN(searchString);

                foreach (Music result in results)
                {
                    string[] description =
                    {
                        "Released in " + result.ReleaseDate,
                        "\nPerformed by " + result.Artist,
                        "\nProduced by" + result.Label,
                        "\nASIN: " + result.Asin
                    };

                    sr.Add(new SearchResult(Constants.TypeConstants.TypeEnum.Music, result.MusicId, result.Title, description));
                }

                return sr;
            });
        }

        #endregion
    }
}
