using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;


namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MovieCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;


        public MovieCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _im = im;

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
                return Task.Factory.StartNew(async () =>
                {
                    var prevMovie = _db.GetMovieById(movie.MovieId);
                    prevMovie.Actors = _db.GetAllMovieActors(movie.MovieId);
                    prevMovie.Producers = _db.GetAllMovieProducers(movie.MovieId);

                    var actorsToAdd = movie.Actors?
                                            .Select(a => a.PersonId)?
                                            .Except(prevMovie.Actors?
                                                .Select(a => a.PersonId) ?? new List<int>())
                                            ?? new List<int>();
                    var actorsToRemove = prevMovie.Actors?
                                            .Select(a => a.PersonId)?
                                            .Except(movie.Actors?
                                                .Select(a => a.PersonId) ?? new List<int>())
                                            ?? new List<int>();

                    foreach (var actor in actorsToRemove)
                    {
                        await DeleteMovieActorAsync(movie.MovieId.ToString(), actor.ToString());
                    }

                    foreach (var actor in actorsToAdd)
                    {
                        await CreateMovieActorAsync(movie.MovieId.ToString(), actor.ToString());
                    }

                    var producersToAdd = movie.Producers?
                                                .Select(a => a.PersonId)?
                                                .Except(prevMovie.Producers?
                                                    .Select(p => p.PersonId) ?? new List<int>())
                                                ?? new List<int>();
                    var producersToRemove = prevMovie.Producers?
                                                .Select(a => a.PersonId)?
                                                .Except(movie.Producers?
                                                    .Select(p => p.PersonId) ?? new List<int>())
                                                ?? new List<int>();

                    foreach (var producer in producersToRemove)
                    {
                        await DeleteMovieProducerAsync(movie.MovieId.ToString(), producer.ToString());
                    }

                    foreach (var producer in producersToAdd)
                    {
                        await CreateMovieProducerAsync(movie.MovieId.ToString(), producer.ToString());
                    }

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
                return _im.GetAllMovies();
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
                return _im.GetAllPerson();
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
                return _im.GetAllMovieProducers(movieID);
            });
        }

        /*
         * The following functions are made for the movie actor table
         */

        public Task<List<Person>> GetAllMovieActorDataAsync(int movieID)
        {
            return Task.Factory.StartNew(() =>
            {
                return _im.GetAllMovieActors(movieID);
            });
        }
        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }
    }
}
