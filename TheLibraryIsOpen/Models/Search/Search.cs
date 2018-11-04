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


        public static Task<List<SearchResult>> SearchAllAsync(string searchString)
        {
            throw new NotImplementedException();

        }

        public static Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {
            throw new NotImplementedException();

        }
        public static Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        public static Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        public static Task<List<SearchResult>> SearchMusicAsync(string searchString)
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
