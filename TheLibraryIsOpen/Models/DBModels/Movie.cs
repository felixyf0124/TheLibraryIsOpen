﻿﻿namespace TheLibraryIsOpen.Models.DBModels
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        //public string Producers { get; set; }
        //public string Actors { get; set; }
        public string Language { get; set; }
        public string Subtitles { get; set; }
        public string Dubbed { get; set; }
        public string ReleaseDate { get; set; }
        public string RunTime { get; set; }

        // Default constructor
        public Movie() { }

        /* Here the constructor assign values to attributes besides MovieId.
         * The MovieId is generated by database, when insert an entry to the "movie" table (assumed it's already & primary key autoincrement).
         * The last id just entered from table would be assigned to MovieId for the movie object. So that to avoid same id appears when server gets restarted.
        */
        //public Movie(string title, string director, string producers, string actors, string language, string subtitles,
        //            string dubbed, string releaseDate, string runTime)
        //{
        //    Title = title;
        //    Director = director;
        //    Producers = producers;
        //    Actors = actors;
        //    Language = language;
        //    Subtitles = subtitles;
        //    Dubbed = dubbed;
        //    ReleaseDate = releaseDate;
        //    RunTime = runTime;
        //}

        public Movie(string title,string director, string language, string subtitles,
                    string dubbed, string releaseDate, string runTime)
        {
            Title = title;
            Director = director;
            Language = language;
            Subtitles = subtitles;
            Dubbed = dubbed;
            ReleaseDate = releaseDate;
            RunTime = runTime;
        }

        // another construcor who  assigns movie id is added as requested.
        //public Movie(int mvId, string title, string director, string producers, string actors, string language, 
        //             string subtitles, string dubbed, string releaseDate, string runTime)
        //{
        //    MovieId = mvId;
        //    Title = title;
        //    Director = director;
        //    Producers = producers;
        //    Actors = actors;
        //    Language = language;
        //    Subtitles = subtitles;
        //    Dubbed = dubbed;
        //    ReleaseDate = releaseDate;
        //    RunTime = runTime;
        //}

        public Movie(int mvId, string title, string director, string language,
                     string subtitles, string dubbed, string releaseDate, string runTime)
        {
            MovieId = mvId;
            Title = title;
            Director = director;
            Language = language;
            Subtitles = subtitles;
            Dubbed = dubbed;
            ReleaseDate = releaseDate;
            RunTime = runTime;
        }

        // Return information about the movie
        //public override string ToString()
        //{
        //    return "Movie:\nMovie Name:" + Title + "\nDirector:" + Director + "\nProducers: " + Producers + 
        //        "\nActors:" + Actors + "\nLanguage:" + Language + "\nSubtitles:" + Subtitles + "\nDubbed:" + Dubbed +
        //        "\nRelease Date:" + ReleaseDate + "\nRun Time:" + RunTime + "\nMovie ID:" + MovieId;
        //}

        public override string ToString()
        {
            return "Movie:\nMovie Name:" + Title + "\nDirector:" + Director + "\nLanguage:" + Language + "\nSubtitles:" + Subtitles + "\nDubbed:" + Dubbed +
                "\nRelease Date:" + ReleaseDate + "\nRun Time:" + RunTime + "\nMovie ID:" + MovieId;
        }
    }
}
