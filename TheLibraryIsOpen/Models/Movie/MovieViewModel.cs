using System.Collections.Generic;
using System.Linq;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Models.Movie
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public List<int> Producers { get; set; } = new List<int>();
        public List<int> Actors { get; set; } = new List<int>();
        public string Language { get; set; }
        public string Subtitles { get; set; }
        public string Dubbed { get; set; }
        public string ReleaseDate { get; set; }
        public string RunTime { get; set; }

        public DBModels.Movie ToMovie()
        {
            return new DBModels.Movie(MovieId, Title, Director, Language, Subtitles, Dubbed, ReleaseDate, RunTime)
            {
                Actors = this.Actors.Select(a => new Person { PersonId = a }).ToList(),
                Producers = this.Producers.Select(p => new Person { PersonId = p }).ToList()
            };
        }
    }

    public static class MovieExtension
    {
        public static MovieViewModel ToEditViewModel(this DBModels.Movie mov)
        {
            return new MovieViewModel
            {
                MovieId = mov.MovieId,
                Title = mov.Title,
                Director = mov.Director,
                Language = mov.Language,
                Subtitles = mov.Subtitles,
                Dubbed = mov.Dubbed,
                ReleaseDate = mov.ReleaseDate,
                RunTime = mov.RunTime,
                Actors = mov.Actors?.Select(a => a.PersonId)?.ToList() ?? new List<int>(),
                Producers = mov.Producers?.Select(p => p.PersonId)?.ToList() ?? new List<int>()
            };
        }
    }
}
