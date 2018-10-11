namespace TheLibraryIsOpen.Models.DBModels
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }

        public MovieActor(int movieId, int personId)
        {
            MovieId = movieId;
            PersonId = personId;
        }

        public override string ToString()
        {
            return "MovieActor:\nMovie ID: " + MovieId + "\nPerson ID: " + PersonId;
        }
    }
}
