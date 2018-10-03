namespace TheLibraryIsOpen.Models.DBModels
{
    public class Movie
    {
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

        public override string ToString()
        {
            return "Movie:\nTitle:" + title + "Director:" + director + "\ngenre:" + genre + "\nYear:" + year;
        }
    }
}