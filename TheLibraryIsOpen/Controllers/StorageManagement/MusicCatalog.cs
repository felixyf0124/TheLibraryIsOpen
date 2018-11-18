using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MusicCatalog : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
        private readonly Db _db; // TODO: delete this when db code is removed


        public MusicCatalog(UnitOfWork unitOfWork, IdentityMap im, Db db)
        {
            _unitOfWork = unitOfWork;
            _im = im;
            _db = db; // TODO: delete this when db code is removed

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
            return Task.Factory.StartNew(() =>
            {
                //TODO: replace with_im
                return _db.GetAllMusic();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<int> getNoOfAvailableModelCopies(Music music)
        {
            return Task.Factory.StartNew(() =>
            {

                int AvailableCopies = _db.CountModelCopiesOfModel(music.MusicId, (int)TypeEnum.Music, BorrowType.NotBorrowed);

                return AvailableCopies;

            });

        }

        public async Task<List<ModelCopy>> getModelCopies(Music music)
        {
            return await _im.FindModelCopies(music.MusicId, TypeEnum.Music);
        }

        public Task<IdentityResult> addModelCopy(string id, Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Music
                    });
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music was null" });
            });
        }
        public Task<IdentityResult> deleteFreeModelCopy(string id, Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false
                    ModelCopy temp = new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Music
                    };
                    _im.DeleteFreeModelCopy(temp, Int32.Parse(id));
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music was null" });
            });
        }
    }
}
