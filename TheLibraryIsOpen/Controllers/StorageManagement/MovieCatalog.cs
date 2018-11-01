using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed


namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MovieCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
        private readonly Db _db; // TODO: delete this when db code is removed


        public MovieCatalog(UnitOfWork unitOfWork, IdentityMap im, Db db)
        {
            _unitOfWork = unitOfWork;
            _im = im;
            _db = db; // TODO: delete this when db code is removed

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
                    // TODO: manage errors if register returns false
                    _unitOfWork.RegisterNew(movie);
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
                    _unitOfWork.RegisterDirty(movie);
                    return IdentityResult.Success;
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
                    _unitOfWork.RegisterDeleted(movie);
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
                return _im.FindMovie(movieId);
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
                    _unitOfWork.RegisterNew(person);
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
                    _unitOfWork.RegisterDirty(person);
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
                    _unitOfWork.RegisterDeleted(person);
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
                // TODO: replace with _im
                return _db.GetAllPerson();
            });
        }

        public Task<Person> GetPersonByIdAsync(int personId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _im.FindPerson(personId);
            });
            throw new ArgumentNullException("personId");
        }

        /*
         * The following functions are made for the movie producer table
         */

        public Task<List<Person>> GetAllMovieProducerDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: replace with _im
                return _db.GetAllMovieProducers(movieID);
            });
        }

        /*
         * The following functions are made for the movie actor table
         */

        public Task<List<Person>> GetAllMovieActorDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: replace with _im
                return _db.GetAllMovieActors(movieID);
            });
        }
        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }
    }
}
