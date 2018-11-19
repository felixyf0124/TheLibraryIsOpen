using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

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

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _im.FindMovie(movieId);
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

        public async Task<Person> GetPersonByIdAsync(int personId)
        {
            return await _im.FindPerson(personId);
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

        public Task<int> getNoOfAvailableModelCopies(Movie movie)
        {
            return Task.Factory.StartNew(() =>
            {

                int AvailableCopies = _db.CountModelCopiesOfModel(movie.MovieId, (int)TypeEnum.Movie, BorrowType.NotBorrowed);

                return AvailableCopies;

            });

        }

        public async Task<List<ModelCopy>> getModelCopies(Movie movie)
        {
            return await _im.FindModelCopies(movie.MovieId, TypeEnum.Movie);
        }

        public Task<IdentityResult> addModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: manage error if register returns false

                _unitOfWork.RegisterNew(new ModelCopy
                {
                    modelID = Int32.Parse(id),
                    modelType = TypeEnum.Movie
                });
                return IdentityResult.Success;
            });
            
        }
        public Task<IdentityResult> deleteFreeModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: manage error if register returns false
                ModelCopy temp = new ModelCopy
                {
                    modelID = Int32.Parse(id),
                    modelType = TypeEnum.Movie
                };
                _im.DeleteFreeModelCopy(temp, Int32.Parse(id));
                return IdentityResult.Success;
            });
           
        }
    }
}

