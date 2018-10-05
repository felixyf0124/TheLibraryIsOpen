namespace TheLibraryIsOpen.Models.DBModels
{
    public class Movie
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public string genre { get; set; }
        public int year { get; set; }
        // other suggestions?

        // Default constructor
        public Movie() { }


        public Movie(string title, string director, string genre, int year)
        {
            this.title = title;
            this.director = director;
            this.genre = genre;
            this.year = year;
        }

        public Movie(int movieId, string title, string director, string genre, int year) :
        this(title, director, genre, year)
        {
            this.movieId = movieId;
        }

        public override string ToString()
        {
            return "Movie:\nTitle:" + title + "Director:" + director + "\ngenre:" + genre + "\nYear:" + year;
        }
    }
}