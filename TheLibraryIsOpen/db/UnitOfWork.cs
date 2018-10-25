using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.db
{
    /*
     * TODO:
     * - Implement RegisterDeleted
     * - Implement RegisterDirty
     * - ? Implement RegisterClean? should we?
     * - Complete CommitAsync
     */
    public class UnitOfWork
    {
        private readonly IdentityMap _im;

        private readonly Dictionary<string, object> RegisteredNew;
        private readonly Dictionary<string, object> RegisteredDeleted;
        private readonly Dictionary<string, object> RegisteredDirty;
        private readonly Dictionary<string, object> RegisteredClean;

        public UnitOfWork(IdentityMap im)
        {
            _im = im;
            RegisteredNew = new Dictionary<string, object>();
            RegisteredDeleted = new Dictionary<string, object>();
            RegisteredDirty = new Dictionary<string, object>();
            RegisteredClean = new Dictionary<string, object>();
        }

        public bool RegisterNew(object o)
        {
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        return RegisteredNew.TryAdd($"{TypeEnum.Book}-{(temp.BookId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.BookId}")}", o);
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        return RegisteredNew.TryAdd($"{TypeEnum.Magazine}-{(temp.MagazineId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MagazineId}")}", o);
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        return RegisteredNew.TryAdd($"{TypeEnum.Movie}-{(temp.MovieId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MovieId}")}", o);
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        return RegisteredNew.TryAdd($"{TypeEnum.Music}-{(temp.MusicId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MusicId}")}", o);
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        return RegisteredNew.TryAdd($"{TypeEnum.Person}-{(temp.PersonId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.PersonId}")}", o);
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public async Task<bool> CommitAsync()
        {
            return (
                await _im.AddAsync(RegisteredNew.ToArray())
                &&
                await _im.DeleteAsync(RegisteredDeleted.ToArray())
                // TODO: Edit RegisteredDirty
                );
        }
    }
}
