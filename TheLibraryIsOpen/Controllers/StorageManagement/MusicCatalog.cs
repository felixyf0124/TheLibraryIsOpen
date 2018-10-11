using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MusicCatalog
    {
        private readonly Db _db;

        public MusicCatalog(Db db)
        {
            _db = db;
        }

        public Task<IdentityResult> CreateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMusic(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music object was null" });
            });
        }

        public Task UdpateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdateMusic(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task<IdentityResult> DeleteMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteMusic(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music object was null" });
            });
        }

        public Task<List<Music>> GetAllMusicsDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMusic();
            });
        }

        public Task<Music> GetMusicByIdAsync(int musicId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetMusicById(musicId);
            });
            throw new ArgumentNullException("musicId");
        }

        public Task<Music> GetMusicByAsinAsync(string asin)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetMusicByAsin(asin);
            });
            throw new ArgumentNullException("asin");
        }
    }
}
