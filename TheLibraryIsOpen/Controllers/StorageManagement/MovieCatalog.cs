using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MovieCatalog
    {
        private readonly Db _db;

        public MovieCatalog(Db db)
        {
            _db = db;
        }

        /*
         * The following are made for the Movie table
         */

        public Task<IdentityResult> CreateMovieAsync(Movie movie)
        {
            if (movie != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMovie(movie);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Movie object was null" });
            });
        }

        public Task UpdateMovieAsync(Movie movie)
        {
            if (movie != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdateMovie(movie);
                });
            }
            throw new ArgumentNullException("movie");
        }

        public Task<IdentityResult> DeleteMovieAsync(Movie movie)
        {
            if (movie != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteMovie(movie);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music object was null" });
            });
        }

        public Task<List<Movie>> GetAllMoviesDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMovies();
            });
        }

        public Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetMovieById(movieId);
            });
            throw new ArgumentNullException("movieId");
        }


        /*
         * The following functions are made for the Person table
         */

        public Task<IdentityResult> CreatePersonAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreatePerson(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Person object was null" });
            });
        }

        public Task UpdatePersonAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdatePerson(person);
                });
            }
            throw new ArgumentNullException("person");
        }

        public Task<IdentityResult> DeletePersonAsync(Person person)
        {
            if (person != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeletePerson(person);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Person object was null" });
            });
        }

        public Task<List<Person>> GetAllPersonDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllPerson();
            });
        }

        public Task<Person> GetPersonByIdAsync(int personId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetPersonById(personId);
            });
            throw new ArgumentNullException("personId");
        }

        /*
         * The following functions are made for movie director table
         */

        public Task<IdentityResult> AddMovieDirectorAsync(string mid, string pid)
        {
            if (mid != null && pid != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMovieDirector(mid,pid);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "MovieDirector object was null" });
            });
        }


        public Task<List<Person>> GetAllMovieDirectorDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMovieDirectors(movieID);
            });
        }

        /*
         * The following functions are made for the movie producer table
         */

        public Task<IdentityResult> CreateMovieProducerAsync(string mid, string pid)
        {
            if (mid != null && pid != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMovieProducer(mid,pid);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "MovieProducer object was null" });
            });
        }

        public Task<IdentityResult> DeleteMovieProducerAsync(string mid, string pid)
        {
            if (mid != null && pid != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteMovieProducer(mid, pid);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "MovieProducer object was null" });
            });
        }

        public Task<List<Person>> GetAllMovieProducerDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMovieProducers(movieID);
            });
        }

        /*
         * The following functions are made for the movie actor table
         */

        public Task<IdentityResult> CreateMovieActorAsync(string mid, string pid)
        {
            if (mid != null && pid != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMovieActor(mid, pid);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "MovieActor object was null" });
            });
        }

        public Task<IdentityResult> DeleteMovieActorAsync(string mid, string pid)
        {
            if (mid != null && pid != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteMovieActor(mid, pid);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "MovieActor object was null" });
            });
        }

        public Task<List<Person>> GetAllMovieActorDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMovieActors(movieID);
            });
        }
    }
}
