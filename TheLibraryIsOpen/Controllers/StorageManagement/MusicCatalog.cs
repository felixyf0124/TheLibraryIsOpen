using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;

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
        public Task<Music> FindMusicByIdAsync(string musicId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _im.FindMusic((int.Parse(musicId)));
            });
            throw new ArgumentNullException("musicId");
        }


        //Update Methods (per attribute, general)

        public Task SetMusicTitleAsync(Music music, string title)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.Title = title;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task SetMusicTypeAsync(Music music, string type)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.Type = type;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task SetMusicArtistAsync(Music music, string artist)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.Artist = artist;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task SetMusicLabelAsync(Music music, string label)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.Label = label;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task SetMusicReleaseDateAsync(Music music, string releaseDate)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.ReleaseDate = releaseDate;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task SetMusicASINAsync(Music music, string aSIN)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    music.Asin = aSIN;
                    _unitOfWork.RegisterDirty(music);
                });
            }
            throw new ArgumentNullException("music");
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
                return _im.GetAllMusic();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }
    }
}
