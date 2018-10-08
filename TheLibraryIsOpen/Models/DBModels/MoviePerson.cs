namespace TheLibraryIsOpen.Models.DBModels
{
    public class MoviePerson
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }

        public MoviePerson(int movieId, int personId)
        {
            MovieId = movieId;
            PersonId = personId;
        }

        public override string ToString()
        {
            return "MoviePerson:\nMovie ID: " + MovieId + "\nPerson ID: " + PersonId; 
        }
    }
}
