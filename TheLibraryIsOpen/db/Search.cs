using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.db
{
    public sealed class Search
    {
        private readonly Db _db;
        private static readonly Lazy<Search> lazy = new Lazy<Search>(() => new Search());
        private Task<List<SearchResult>> searchString;

        public static Search Instance { get { return lazy.Value; }}

        private Search()
        {

        }

        public static Task<List<SearchResult>> SearchAllAsync(string searchString)
        {

        }

        public static Task<List<SearchResult>> SearchBooksAsync(string searchString)
        {

        }
        public static Task<List<SearchResult>> SearchMagazinesAsync(string searchString)
        {

        }
        public static Task<List<SearchResult>> SearchMoviesAsync(string searchString)
        {

        }
        public static Task<List<SearchResult>> SearchMusicAsync(string searchString)
        {

        }






        private Task<List<SearchResult>> SearchBooksTitlesAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchBooksAuthorsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchBooksFormatsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchBooksPagesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchBooksPublishersAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchBooksLanguagesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchBooksIsbn10Async(string searhString)
        {

        }
        private  Task <List<SearchResult>> SearchBooksIsbn13Async (string searhString)
        {
           
        }



        private Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMagazinesPublishersAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMagazinesLanguagesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMagazinesIsbn10Async(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMagazinesIsbn13Async(string searhString)
        {

        }



        private Task<List<SearchResult>> SearchMoviesTitlesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesDirectorsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesProducersAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesActorsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesLanguagesAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesSubtitlesAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchMoviesDubbedAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesReleaseDateAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMoviesRunTimeAsync(string searhString)
        {

        }


        private Task<List<SearchResult>> SearchMusicTypesAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchMusicTitlesAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchMusicArtistsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMusicLabelsAsync(string searhString)
        {

        }

        private Task<List<SearchResult>> SearchMusicReleaseDateAsync(string searhString)
        {

        }
        private Task<List<SearchResult>> SearchMusicAsinAsync(string searhString)
        {

        }
    }
}
