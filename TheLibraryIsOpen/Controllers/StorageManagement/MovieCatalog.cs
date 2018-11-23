using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db; 
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

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
            return _im.GetAllMovies();
         
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _im.FindMovie(movieId);
        }
        
        public Task<List<Person>> GetAllPersonDataAsync()
        {
            return _im.GetAllPerson();
  
        }
        public Task<List<Person>> GetAllMovieProducerDataAsync(int movieID)
        {
           
                return _im.GetAllMovieProducers(movieID);
 
        }
        public Task<List<Person>> GetAllMovieActorDataAsync(int movieID)
        {
            return _im.GetAllMovieActors(movieID);
           
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<int> GetNoOfAvailableModelCopies(Movie movie)
        {
            return _im.CountModelCopiesOfModel(movie.MovieId, (int)TypeEnum.Movie, BorrowType.NotBorrowed);

        }
        public Task<IdentityResult> AddModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
            {
               _unitOfWork.RegisterNew(new ModelCopy
                {
                    modelID = Int32.Parse(id),
                    modelType = TypeEnum.Movie
                });
                return IdentityResult.Success;
            });
            
        }
        public Task<IdentityResult> DeleteFreeModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
            {
               
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

