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
                        if (RegisteredNew.ContainsValue(temp) == false)
                            return RegisteredNew.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                        return false;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        if (RegisteredNew.ContainsValue(temp) == false)
                            return RegisteredNew.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o);
                        return false;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        if (RegisteredNew.ContainsValue(temp) == false)
                            return RegisteredNew.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o);
                        return false;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        if (RegisteredNew.ContainsValue(temp) == false)
                            return RegisteredNew.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                        return false;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        if (RegisteredNew.ContainsValue(temp) == false)
                            return RegisteredNew.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public bool RegisterDelete(object o)
        {
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        return RegisteredDeleted.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        return RegisteredDeleted.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o);
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        return RegisteredDeleted.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o);
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        return RegisteredDeleted.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        return RegisteredDeleted.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
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
