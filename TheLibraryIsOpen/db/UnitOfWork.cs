using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly ReaderWriterLockSlim _lock;

        private readonly Dictionary<string, object> RegisteredNew;
        private readonly Dictionary<string, object> RegisteredDeleted;
        private readonly Dictionary<string, object> RegisteredDirty;
        private readonly Dictionary<string, object> RegisteredClean;

        public UnitOfWork(IdentityMap im)
        {
            _im = im;
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            RegisteredNew = new Dictionary<string, object>();
            RegisteredDeleted = new Dictionary<string, object>();
            RegisteredDirty = new Dictionary<string, object>();
            RegisteredClean = new Dictionary<string, object>();
        }

        public bool RegisterNew(object o)
        {
            bool succeeded = false;
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        while (!_lock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                        _lock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_lock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o);
                        _lock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_lock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o);
                        _lock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_lock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                        _lock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_lock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
                        _lock.ExitWriteLock();
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return succeeded;
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
