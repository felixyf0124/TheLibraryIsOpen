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

        public Task<List<SearchResult>> SearchMagazinesTitlesAsync(string searchString)
        {
            return Task.Factory.StartNew(() =>
            {
                List<SearchResult> convertedResult = new List<SearchResult>();
                List<Magazine> result = _db.SearchMagazinesByTitle(searchString);
                if(result.Count != 0) 
                {
                    foreach(Magazine magazine in result)
                    {
                        String[] description = {
                            "publisher:" + magazine.Publisher,
                            "language:" + magazine.Language,
                            "date:"+ magazine.Date,
                            "isbn10:" + magazine.Isbn10,
                            "isbn13" + magazine.Isbn13
                        };
                        convertedResult.Add(new SearchResult(TypeConstants.TypeEnum.Magazine, magazine.MagazineId, magazine.Title, description));
                    }
                } else {
                    String[] description = { "nothing", "nothing", "nothing", "nothing", "nothing" };
                    convertedResult.Add(new SearchResult(TypeConstants.TypeEnum.Magazine, 0, "none", description));
                }
                for (int i = 0; i < convertedResult.Count; i++)
                {
                    Console.WriteLine("---------Start---------------------------");
                    Console.WriteLine(convertedResult[i].Type);
                    Console.WriteLine(convertedResult[i].Name);
                    Console.WriteLine(convertedResult[i].ModelId);
                    Console.WriteLine(convertedResult[i].Description[0]);
                    Console.WriteLine(convertedResult[i].Description[1]);
                    Console.WriteLine(convertedResult[i].Description[2]);
                    Console.WriteLine(convertedResult[i].Description[3]);
                    Console.WriteLine(convertedResult[i].Description[4]);
                }
                return convertedResult;
            });
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
