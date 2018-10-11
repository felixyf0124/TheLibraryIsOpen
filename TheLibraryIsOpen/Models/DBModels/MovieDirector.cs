namespace TheLibraryIsOpen.Models.DBModels
{
    public class MovieDirector
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }

        public MovieDirector(int movieId, int personId)
        {
            MovieId = movieId;
            PersonId = personId;
        }

        public override string ToString()
        {
            return "MovieDirector:\nMovie ID: " + MovieId + "\nPerson ID: " + PersonId; 
        }
    }
}
