namespace TheLibraryIsOpen.Models.DBModels
{
    public class MovieProducer
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }

        public MovieProducer(int movieId, int personId)
        {
            MovieId = movieId;
            PersonId = personId;
        }

        public override string ToString()
        {
            return "MovieProducer:\nMovie ID: " + MovieId + "\nPerson ID: " + PersonId;
        }
    }
}
