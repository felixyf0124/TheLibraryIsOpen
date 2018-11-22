using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MusicCatalog : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
       
        public MusicCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _im = im;

        }

        //Create Music
        public Task<IdentityResult> CreateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage errors if register fails, it returns false
                    _unitOfWork.RegisterNew(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "music was null" });
            });
        }

        //Delete Music
        public Task<IdentityResult> DeleteMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDeleted(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "music was null" });
            });
        }

        //Find methods (by id)
        public async Task<Music> FindMusicByIdAsync(string musicId)
        {
            return await _im.FindMusic((int.Parse(musicId)));
        }

        public Task<IdentityResult> UpdateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDirty(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "music was null" });
            });
        }

        public Task<List<Music>> GetAllMusicDataAsync()
        {

          return _im.GetAllMusic();
         
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }



        public Task<int> GetNoOfAvailableModelCopies(Music music)
        {

           return _im.CountModelCopiesOfModel(music.MusicId, (int)TypeEnum.Music, BorrowType.NotBorrowed);

        }


        public async Task<List<ModelCopy>> GetModelCopies(Music music)
        {
            return await _im.FindModelCopies(music.MusicId, TypeEnum.Music);
        }

        public Task<IdentityResult> AddModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                _unitOfWork.RegisterNew(new ModelCopy
                {
                    modelID = Int32.Parse(id),
                    modelType = TypeEnum.Music
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
                    modelType = TypeEnum.Music
                };
                _im.DeleteFreeModelCopy(temp, Int32.Parse(id));
                return IdentityResult.Success;
            });
        }
    }
}
