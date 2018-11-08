using System;
using System.Collections.Generic;

﻿namespace TheLibraryIsOpen.Models.DBModels
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public List<Person> Producers { get; set; }
        public List<Person> Actors { get; set; }
        public string Language { get; set; }
        public string Subtitles { get; set; }
        public string Dubbed { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string RunTime { get; set; }

        // Default constructor
        public Movie() { }

        public Movie(string title,string director, string language, string subtitles,
                    string dubbed, DateTime releaseDate, string runTime)
        {
            Title = title;
            Director = director;
            Language = language;
            Subtitles = subtitles;
            Dubbed = dubbed;
            ReleaseDate = releaseDate;
            RunTime = runTime;
        }

        public Movie(int mvId, string title, string director, string language,
                     string subtitles, string dubbed, DateTime releaseDate, string runTime)
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

        public override string ToString()
        {
            return "Movie:\nMovie Name:" + Title + "\nDirector:" + Director + "\nLanguage:" + Language + "\nSubtitles:" + Subtitles + "\nDubbed:" + Dubbed +
                "\nRelease Date:" + ReleaseDate + "\nRun Time:" + RunTime + "\nMovie ID:" + MovieId;
        }
    }
}
