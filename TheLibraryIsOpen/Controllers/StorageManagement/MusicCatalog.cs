using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class MusicCatalog : ControllerBase
    {
        private readonly Db _db;

        public MusicCatalog(Db db)
        {
            _db = db;
        }

        public bool AddNewMusic(string type, string title, string artist, string label, string releaseDate, string asin)
        {
            Music m = new Music();

            bool result = Db.createMusic(m);

            return result;
        }
    }
}
